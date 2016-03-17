using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using overrideSocial;

namespace fnpix
{
    public partial class add_display : System.Web.UI.Page
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
        private overrideSocial.displays _displays = new overrideSocial.displays();
        private overrideSocial.dropbox _dropbox = new overrideSocial.dropbox();

        protected void Page_Load(object sender, EventArgs e)
        {
            get_totals();

            permissions();

            if (!Page.IsPostBack)
            {
                ddl_view.DataSource = _displays.get_templates();
                ddl_view.DataValueField = "id";
                ddl_view.DataTextField = "title";
                ddl_view.DataBind();

                ListItem i = new ListItem("Select a View", "");

                ddl_view.Items.Insert(0, i);

                if (Page.RouteData.Values["id"] != null)
                {
                    add_edit = "Edit";

                    Display d = _displays.single(Convert.ToInt32(Page.RouteData.Values["id"] as string));

                    ddl_view.SelectedValue = d.view_id.ToString();
                    start_date.Text = Convert.ToDateTime(d.startdate).ToShortDateString();
                    start_time.Text = Convert.ToDateTime(d.startdate).ToShortTimeString();
                    end_date.Text = Convert.ToDateTime(d.enddate).ToShortDateString();
                    end_time.Text = Convert.ToDateTime(d.enddate).ToShortTimeString();
                    listenSlider.Value = d.slide_duration.ToString();
                }
            }
        }

        protected void update(object sender, EventArgs e)
        {
            Boolean err = false;
            List<string> err_text = new List<string>();

            Display d = new Display();

            Boolean is_update = false;

            if (Page.RouteData.Values["id"] != null)
            {
                d = _displays.single(Convert.ToInt32(Page.RouteData.Values["id"] as string));

                is_update = true;
            }

            if (!string.IsNullOrEmpty(start_date.Text.ToString()) && !string.IsNullOrEmpty(start_time.Text.ToString()))
            {
                d.startdate = ((DateTime) Convert.ToDateTime(start_date.Text.ToString())).Date.Add(((DateTime) Convert.ToDateTime(start_time.Text.ToString())).TimeOfDay);
            }


            if (!string.IsNullOrEmpty(end_date.Text.ToString()) && !string.IsNullOrEmpty(end_time.Text.ToString()))
            {
                d.enddate = ((DateTime) Convert.ToDateTime(end_date.Text.ToString())).Date.Add(((DateTime) Convert.ToDateTime(end_time.Text.ToString())).TimeOfDay);
            }


            d.view_id = Convert.ToInt32(ddl_view.SelectedValue.ToString());
            

            d.event_id = Convert.ToInt32(Session["event_id"].ToString());
            d.slide_duration = Convert.ToInt32(listenSlider.Value.ToString());

            if (is_update)
            {
                _displays.update(d);

                pnl_success.Visible = true;
            }
            else
            {
                _displays.insert(d);

                pnl_success.Visible = true;
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
    }
}