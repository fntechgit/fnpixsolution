using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using overrideSocial;

namespace fnpix
{
    public partial class preferences : System.Web.UI.Page
    {
        public string total_media = "0";
        public string facebook_media = "0";
        public string instagram_media = "0";
        public string twitter_media = "0";
        public string unapproved_media = "0";
        public string all_media = "0";

        private overrideSocial.mediaManager _media = new overrideSocial.mediaManager();
        private overrideSocial.tags _tags = new overrideSocial.tags();
        private overrideSocial.dropbox _dropbox = new overrideSocial.dropbox();

        protected void Page_Load(object sender, EventArgs e)
        {
            permissions();

            get_totals();

            string facebook = "IGNORE";
            string twitter = "IGNORE";
            string instagram = "IGNORE";
            string entire_event = "TRUE";
            string is_tag = "HASHTAG";
            string start = "N/A";
            string end = "N/A";

            foreach (Tag t in _tags.select(Convert.ToInt32(Session["event_id"].ToString())))
            {
                entire_event = t.entire_event ? "TRUE" : "FALSE";
                facebook = t.facebook ? "QUERY" : "IGNORE";
                twitter = t.twitter ? "QUERY" : "IGNORE";
                instagram = t.instagram ? "QUERY" : "IGNORE";
                is_tag = t.is_tag ? "HASHTAG" : "USERNAME";

                start = t.start_time == null ? "N/A" : Convert.ToDateTime(t.start_time).ToShortDateString() + " " + Convert.ToDateTime(t.start_time).ToShortTimeString();
                end = t.end_time == null ? "N/A" : Convert.ToDateTime(t.end_time).ToShortDateString() + " " + Convert.ToDateTime(t.end_time).ToShortTimeString();

                ph_tags.Controls.Add(new LiteralControl("<tr><td data-title=\"Tag\">" + t.value.ToUpper() + "</td><td data-title=\"Facebook\" class=\"hidden-xs hidden-sm\">" + facebook + "</td><td data-title=\"Twitter\" class=\"hidden-xs hidden-sm\">" + twitter + "</td><td data-title=\"Instagram\" class=\"hidden-xs hidden-sm\">" + instagram + "</td><td data-title=\"Type\" class=\"hidden-xs hidden-sm\">" + is_tag + "</td><td data-title=\"Entire Event\" class=\"text-right\">" + entire_event + "</td><td data-title=\"Start\" class=\"text-right\">" + start + "</td><td data-title=\"End\" class=\"text-right\">" + end + "</td><td data-title=\"Actions\"><a href=\"/preferences/edit/" + t.id + "\"><i class=\"fa fa-edit\"></i></a> <a href=\"/preferences/delete/" + t.id + "\"><i class=\"fa fa-trash-o\"></i></a></td></tr>"));
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