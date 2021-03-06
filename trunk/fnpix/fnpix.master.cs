﻿using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using overrideSocial;
using DropNet;

namespace fnpix
{
    public partial class fnpix : System.Web.UI.MasterPage
    {
        public string unapproved_count = "0";
        public string user_picture = string.Empty;
        public string user_name = string.Empty;
        public string company_name = string.Empty;
        public string user_email = string.Empty;

        private overrideSocial.mediaManager _media = new overrideSocial.mediaManager();
        private overrideSocial.permissions _permissions = new overrideSocial.permissions();
        private overrideSocial.settings _settings = new overrideSocial.settings();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_id"] == null)
            {
                Response.Redirect("/login");
            }
            else
            {
                user_picture = "/uploads/" + Session["user_pic"].ToString();
                user_name = Session["user_name"].ToString();
                company_name = Session["company_name"].ToString();
                user_email = Session["user_email"].ToString();

                if (!Page.IsPostBack)
                {
                    List<Event> myevents = _permissions.select_permitted_events(Convert.ToInt32(Session["user_id"].ToString()));

                    if (myevents.Count > 0)
                    {
                        ddl_event.DataSource = myevents;
                        ddl_event.DataValueField = "id";
                        ddl_event.DataTextField = "title";
                        ddl_event.DataBind();
                        ddl_event.SelectedIndex = 0;

                        if (Session["event_id"] != null)
                        {
                            ddl_event.SelectedValue = Session["event_id"].ToString();
                        }
                        else
                        {
                            Session["event_id"] = myevents[0].id.ToString();
                        }
                    }
                    else
                    {
                        if (Session["user_access"] == "system")
                        {
                            ListItem item = new ListItem("No Events", "0");

                            ddl_event.Items.Insert(0, item);
                        }
                        else
                        {
                            Response.Redirect("/login/no-events-allowed");
                        }
                    }
                }
            }

            List<Media> _unapproved = _media.get_unapproved(Convert.ToInt32(Session["event_id"].ToString()));

            unapproved_count = _unapproved.Count.ToString("0.#");
        }

        protected void change_event(object sender, EventArgs e)
        {
            Session["event_id"] = ddl_event.SelectedValue.ToString();

            Response.Redirect(Request.Url.ToString());
        }

        protected void auth_dropbox(object sender, EventArgs e)
        {
            DropNetClient _client = new DropNetClient(_settings.dropbox_api_key(), _settings.dropbox_api_secret());

            Session["request_token"] = _client.GetToken();
            Session["dropbox_event_id"] = Session["event_id"].ToString();

            var url = _client.BuildAuthorizeUrl(_settings.dropbox_return_url());

            Response.Redirect(url);
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["user_id"] == null)
            {
                Response.Redirect("/login");
            }
            else
            {
                if (Session["event_id"] == null)
                {
                    Session["event_id"] = _permissions.select_permitted_events(Convert.ToInt32(Session["user_id"].ToString()))[0].id.ToString();
                }

                user_picture = "/uploads/" + Session["user_pic"].ToString();
                user_name = Session["user_name"].ToString();
                company_name = Session["company_name"].ToString();
                user_email = Session["user_email"].ToString();
            }
        }
    }
}