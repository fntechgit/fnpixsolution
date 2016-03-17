using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using overrideSocial;

namespace fnpix
{
    public partial class add_permission : System.Web.UI.Page
    {
        public string add_edit = "Add";
        public string user_name = string.Empty;

        public string total_media = "0";
        public string facebook_media = "0";
        public string instagram_media = "0";
        public string twitter_media = "0";
        public string unapproved_media = "0";
        public string all_media = "0";

        private overrideSocial.mediaManager _media = new overrideSocial.mediaManager();
        private overrideSocial.users _users = new overrideSocial.users();
        private overrideSocial.events _events = new overrideSocial.events();
        private overrideSocial.permissions _permissions = new overrideSocial.permissions();
        private overrideSocial.dropbox _dropbox = new overrideSocial.dropbox();

        protected void Page_Load(object sender, EventArgs e)
        {
            get_totals();

            permissions();

            if (!Page.IsPostBack)
            {
                ddl_event.DataSource = _events.select_list();
                ddl_event.DataValueField = "id";
                ddl_event.DataTextField = "title";
                ddl_event.DataBind();

                ListItem i = new ListItem("Select an Event", "");

                ddl_event.Items.Insert(0, i);

                btn_permissions.NavigateUrl = "/permissions/" + Page.RouteData.Values["id"];

                if (Page.RouteData.Values["permission_id"] != null)
                {
                    add_edit = "Edit";

                    Permission p =
                        _permissions.@select(Convert.ToInt32(Page.RouteData.Values["permission_id"] as string));

                    ddl_event.SelectedValue = p.event_id.ToString();
                    security.SelectedValue = p.permission_id.ToString();
                }
            }
        }

        protected void update(object sender, EventArgs e)
        {
            Permission p = new Permission();

            Boolean is_update = false;

            if (Page.RouteData.Values["permission_id"] != null)
            {
                p = _permissions.select(Convert.ToInt32(Page.RouteData.Values["permission_id"] as string));

                is_update = true;
            }

            p.assigned_by = Convert.ToInt32(Session["user_id"].ToString());
            p.assigned_date = DateTime.Now;
            p.event_id = Convert.ToInt32(ddl_event.SelectedValue.ToString());
            p.permission_id = Convert.ToInt32(security.SelectedValue.ToString());
            p.user_id = Convert.ToInt32(Page.RouteData.Values["id"] as string);

            if (is_update)
            {
                _permissions.update(p);
            }
            else
            {
                _permissions.add(p);    
            }

            pnl_success.Visible = true;
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
    }
}