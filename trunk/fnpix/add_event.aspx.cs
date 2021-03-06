﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using overrideSocial;
using DropNet;
using System.IO;

namespace fnpix
{
    public partial class add_event : System.Web.UI.Page
    {
        public string add_edit = "Add";

        public string total_media = "0";
        public string facebook_media = "0";
        public string instagram_media = "0";
        public string twitter_media = "0";
        public string unapproved_media = "0";
        public string all_media = "0";
        public string interval = "5";

        private overrideSocial.mediaManager _media = new overrideSocial.mediaManager();
        private overrideSocial.events _events = new overrideSocial.events();
        private overrideSocial.settings _settings = new overrideSocial.settings();
        private overrideSocial.dropbox _dropbox = new overrideSocial.dropbox();

        protected void Page_Load(object sender, EventArgs e)
        {
            get_totals();

            permissions();

            // check for edit
            if (Page.RouteData.Values["id"] != null)
            {
                btn_process.Text = "Update";
                add_edit = "Update";
            }
            else
            {
                btn_process.Text = "Add";
            }

            if (!Page.IsPostBack)
            {
                if (Page.RouteData.Values["id"] != null)
                {
                    Event ev = _events.@select(Convert.ToInt32(Page.RouteData.Values["id"] as string));

                    title.Text = ev.title;
                    description.Text = ev.description;
                    client.Text = ev.client;
                    address.Text = ev.address;
                    address2.Text = ev.address2;
                    city.Text = ev.city;
                    state.Text = ev.state;
                    zip.Text = ev.zip;
                    country.Text = ev.country;
                    hdn_latitude.Value = ev.latitude.ToString();
                    hdn_longitude.Value = ev.longitude.ToString();
                    start_date.Text = ev.start.ToShortDateString();
                    start_time.Text = ev.start.ToShortTimeString();
                    end_date.Text = ev.end.ToShortDateString();
                    end_time.Text = ev.end.ToShortTimeString();
                    moderate.Checked = ev.moderate;
                    listenSlider.Value = ev.interval.ToString();
                    interval = ev.interval.ToString();

                    if (!string.IsNullOrEmpty(ev.background_1280))
                    {
                        ph_current_1280.Controls.Add(new LiteralControl("<h6>Current 1280 x 720 Background</h6><p><img src=\"/uploads/" + ev.background_1280 + "\" width=\"100%\" /></p>"));
                    }

                    if (!string.IsNullOrEmpty(ev.background_1920))
                    {
                        ph_current_1920.Controls.Add(new LiteralControl("<h6>Current 1920 x 1280 Background</h6><p><img src=\"/uploads/" + ev.background_1920 + "\" width=\"100%\" /></p>"));
                    }

                    pnl_map.Visible = true;
                    btn_dropbox.Visible = true;
                }
            }

        }

        private void get_totals()
        {
            List<Media> _all = _media.get_all(Convert.ToInt32(Session["event_id"].ToString()));
            List<Media> _twitter = _media.get_twitter(Convert.ToInt32(Session["event_id"].ToString()));
            List<Media> _instagram = _media.get_instagram(Convert.ToInt32(Session["event_id"].ToString()));
            List<Media> _unapproved = _media.get_unapproved(Convert.ToInt32(Session["event_id"].ToString()));
            List<Dropbox> _dropbox_total = _dropbox.select_list(Convert.ToInt32(Session["event_id"].ToString()));

            facebook_media = _dropbox_total.Count.ToString("0.#");
            total_media = _all.Count.ToString("0.#");
            all_media = total_media;
            instagram_media = _instagram.Count.ToString("0.#");
            twitter_media = _twitter.Count.ToString("0.#");
            unapproved_media = _unapproved.Count.ToString("0.#");
        }

        private void permissions()
        {
            if (string.IsNullOrEmpty(Session["event_id"] as string))
            {
                Response.Redirect("/login");
            }

            if (string.IsNullOrEmpty(Session["user_access"] as string))
            {
                Response.Redirect("/login");
            }
            else
            {
                check_levels(Session["user_access"] as string);
            }
        }

        private void check_levels(string user_level)
        {
            switch (user_level)
            {
                case "system":
                    event_link.Visible = true;
                    display_link.Visible = true;
                    user_link.Visible = true;
                    preference_link.Visible = true;
                    break;
                case "event":
                    display_link.Visible = true;
                    preference_link.Visible = true;
                    break;
                case "content":
                    break;
            }
        }

        protected void dropbox(object sender, EventArgs e)
        {
            DropNetClient _client = new DropNetClient(_settings.dropbox_api_key(), _settings.dropbox_api_secret());

            Session["request_token"] = _client.GetToken();
            Session["dropbox_event_id"] = Page.RouteData.Values["id"] as string;

            var url = _client.BuildAuthorizeUrl(_settings.dropbox_return_url());

            Response.Redirect(url);
        }

        protected void update(object sender, EventArgs e)
        {
            Event ev = new Event();

            Boolean is_update = false;

            if (Page.RouteData.Values["id"] != null)
            {
                is_update = true;

                ev = _events.@select(Convert.ToInt32(Page.RouteData.Values["id"] as string));
            }

            ev.title = title.Text.ToString();
            ev.client = client.Text.ToString();
            ev.description = description.Text.ToString();
            ev.address = address.Text.ToString();
            ev.address2 = address2.Text.ToString();
            ev.city = city.Text.ToString();
            ev.state = state.Text.ToString();
            ev.zip = zip.Text.ToString();
            ev.country = country.SelectedValue.ToString();
            ev.interval = Convert.ToInt32(listenSlider.Value);
            ev.moderate = moderate.Checked;

            interval = ev.interval.ToString();

            if (background_1920.HasFile)
            {
                string path = Server.MapPath("~/uploads/");
                string extension = Path.GetExtension(background_1920.FileName.ToString());
                string unique = Guid.NewGuid().ToString();

                background_1920.SaveAs(path + unique + extension);

                ev.background_1920 = unique + extension;
            }

            if (background_1280.HasFile)
            {
                string path = Server.MapPath("~/uploads/");
                string extension = Path.GetExtension(background_1280.FileName.ToString());
                string unique = Guid.NewGuid().ToString();

                background_1280.SaveAs(path + unique + extension);

                ev.background_1280 = unique + extension;
            }

            if (!string.IsNullOrEmpty(start_date.Text.ToString()) && !string.IsNullOrEmpty(start_time.Text.ToString()))
            {
                ev.start = ((DateTime)Convert.ToDateTime(start_date.Text.ToString())).Date.Add(((DateTime)Convert.ToDateTime(start_time.Text.ToString())).TimeOfDay);
            }

            if (!string.IsNullOrEmpty(end_date.Text.ToString()) && !string.IsNullOrEmpty(end_time.Text.ToString()))
            {
                ev.end = ((DateTime)Convert.ToDateTime(end_date.Text.ToString())).Date.Add(((DateTime)Convert.ToDateTime(end_time.Text.ToString())).TimeOfDay);
            }

            var theaddress = ev.address + " " + ev.city + " " + ev.state + " " + ev.country;
            var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false", Uri.EscapeDataString(theaddress));

            var request = WebRequest.Create(requestUri);
            var response = request.GetResponse();
            var xdoc = XDocument.Load(response.GetResponseStream());

            var result = xdoc.Element("GeocodeResponse").Element("result");

            try
            {
                var locationElement = result.Element("geometry").Element("location");
                
                ev.latitude = Convert.ToDecimal(locationElement.Element("lat").Value);
                ev.longitude = Convert.ToDecimal(locationElement.Element("lng").Value);
            }
            catch (NullReferenceException ex)
            {
                ev.latitude = null;
                ev.longitude = null;
            }

            if (is_update)
            {
                ev = _events.update(ev);
            }
            else
            {
                ev.created_by = Session["user_name"].ToString();
                ev.created_date = DateTime.Now;
                ev.created_by_int = Convert.ToInt32(Session["user_id"].ToString());

                ev = _events.add(ev);
            }

            ph_current_1280.Controls.Clear();
            ph_current_1920.Controls.Clear();

            if (!string.IsNullOrEmpty(ev.background_1280))
            {
                ph_current_1280.Controls.Add(new LiteralControl("<h6>Current 1280 x 720 Background</h6><p><img src=\"/uploads/" + ev.background_1280 + "\" width=\"100%\" /></p>"));
            }

            if (!string.IsNullOrEmpty(ev.background_1920))
            {
                ph_current_1920.Controls.Add(new LiteralControl("<h6>Current 1920 x 1280 Background</h6><p><img src=\"/uploads/" + ev.background_1920 + "\" width=\"100%\" /></p>"));
            }

            pnl_success.Visible = true;
        }
    }
}