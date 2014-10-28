using System;
using System.Collections.Generic;
using overrideSocial;

namespace fnpix
{
    public partial class add_preference : System.Web.UI.Page
    {
        public string add_edit = "Add";

        public string total_media = "0";
        public string facebook_media = "0";
        public string instagram_media = "0";
        public string twitter_media = "0";
        public string unapproved_media = "0";
        public string all_media = "0";

        private overrideSocial.mediaManager _media = new overrideSocial.mediaManager();
        private overrideSocial.tags _tags = new overrideSocial.tags();

        protected void Page_Load(object sender, EventArgs e)
        {
            get_totals();

            if (Page.RouteData.Values["id"] != null)
            {
                add_edit = "Update";
                btn_process.Text = "Update";
            }
            else
            {
                btn_process.Text = "Add";
            }

            if (!Page.IsPostBack)
            {
                if (Page.RouteData.Values["id"] != null)
                {
                    // this is an update
                    Tag t = _tags.get_by_id(Convert.ToInt32(Page.RouteData.Values["id"] as string));

                    search_text.Text = t.value;

                    is_tag.SelectedValue = t.is_tag ? "true" : "false";
                    entire_event.Checked = t.entire_event;

                    if (t.start_time != null)
                    {
                        DateTime dt_start_date = Convert.ToDateTime(t.start_time);

                        start_date.Text = dt_start_date.ToShortDateString();
                        start_time.Text = dt_start_date.ToShortTimeString();
                    }

                    if (t.end_time != null)
                    {
                        DateTime dt_end_time = Convert.ToDateTime(t.end_time);

                        end_date.Text = dt_end_time.ToShortDateString();
                        end_time.Text = dt_end_time.ToShortTimeString();
                    }

                    facebook.Checked = t.facebook;
                    instagram.Checked = t.instagram;
                    twitter.Checked = t.twitter;
                }
            }
        }

        protected void update(object sender, EventArgs e)
        {
            if (Session["event_id"] == null)
            {
                Response.Redirect("/dashboard");
            }

            Tag t = new Tag();

            Boolean is_update = false;

            if (Page.RouteData.Values["id"] != null)
            {
                t = _tags.get_by_id(Convert.ToInt32(Page.RouteData.Values["id"] as string));

                is_update = true;
            }

            t.value = search_text.Text.ToString();

            t.is_tag = is_tag.SelectedValue.ToString() == "true";
            t.entire_event = entire_event.Checked;
            t.facebook = facebook.Checked;
            t.instagram = instagram.Checked;
            t.twitter = twitter.Checked;

            if (!t.entire_event)
            {
                // we need to make a datetime
                if (!string.IsNullOrEmpty(start_date.Text.ToString()) && !string.IsNullOrEmpty(start_time.Text.ToString()))
                {
                    t.start_time = ((DateTime) Convert.ToDateTime(start_date.Text.ToString())).Date.Add(((DateTime) Convert.ToDateTime(start_time.Text.ToString())).TimeOfDay);
                }

                if (!string.IsNullOrEmpty(end_date.Text.ToString()) && !string.IsNullOrEmpty(end_time.Text.ToString()))
                {
                    t.end_time = ((DateTime) Convert.ToDateTime(end_date.Text.ToString())).Date.Add(((DateTime) Convert.ToDateTime(end_time.Text.ToString())).TimeOfDay);
                }
            }

            if (is_update)
            {
                _tags.update(t);
            }
            else
            {
                t.event_id = Convert.ToInt32(Session["event_id"].ToString());

                _tags.add(t);
            }

            pnl_success.Visible = true;
        }

        private void get_totals()
        {
            List<Media> _all = _media.get_recent();
            List<Media> _twitter = _media.get_twitter();
            List<Media> _instagram = _media.get_instagram();
            List<Media> _unapproved = _media.get_unapproved();

            total_media = _all.Count.ToString("0.#");
            all_media = total_media;
            instagram_media = _instagram.Count.ToString("0.#");
            twitter_media = _twitter.Count.ToString("0.#");
            unapproved_media = _unapproved.Count.ToString("0.#");
        }
    }
}