using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using overrideSocial;

namespace fnpix
{
    public partial class events : System.Web.UI.Page
    {

        public string total_media = "0";
        public string facebook_media = "0";
        public string instagram_media = "0";
        public string twitter_media = "0";
        public string unapproved_media = "0";
        public string all_media = "0";

        private overrideSocial.mediaManager _media = new overrideSocial.mediaManager();
        private overrideSocial.events _events = new overrideSocial.events();
        private overrideSocial.dropbox _dropbox = new overrideSocial.dropbox();

        protected void Page_Load(object sender, EventArgs e)
        {
            permissions();

            get_totals();

            

            string moderate = string.Empty;
            string start = "N/A";
            string end = "N/A";

            foreach (Event ev in _events.select_list())
            {
                moderate = ev.moderate ? "MODERATED" : "NON-MODERATED";

                start = ev.start == null ? "N/A" : Convert.ToDateTime(ev.start).ToShortDateString() + " " + Convert.ToDateTime(ev.start).ToShortTimeString();
                end = ev.end == null ? "N/A" : Convert.ToDateTime(ev.end).ToShortDateString() + " " + Convert.ToDateTime(ev.end).ToShortTimeString();

                ph_tags.Controls.Add(new LiteralControl("<tr><td data-title=\"Title\">" + ev.title + "</td><td data-title=\"Client\" class=\"hidden-xs hidden-sm\">" + ev.client + "</td><td data-title=\"Address\" class=\"hidden-xs hidden-sm\">" + ev.address + " " + ev.address2 + "</td><td data-title=\"City, State Zip\" class=\"hidden-xs hidden-sm\">" + ev.city + ", " + ev.state + " " + ev.zip + "</td><td data-title=\"Country\" class=\"hidden-xs hidden-sm\">" + ev.country + "</td><td data-title=\"Moderate\" class=\"text-right\">" + moderate + "</td><td data-title=\"Start\" class=\"text-right\">" + start + "</td><td data-title=\"End\" class=\"text-right\">" + end + "</td><td data-title=\"Actions\"><a href=\"/events/edit/" + ev.id + "\"><i class=\"fa fa-edit\"></i></a> <a href=\"/events/delete/" + ev.id + "\"><i class=\"fa fa-trash-o\"></i></a></td></tr>"));
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