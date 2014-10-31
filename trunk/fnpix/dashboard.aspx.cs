using System;
using System.Collections.Generic;
using System.Web.UI;
using overrideSocial;

namespace fnpix
{
    public partial class dashboard : System.Web.UI.Page
    {

        public string total_media = "0";
        public string facebook_media = "0";
        public string instagram_media = "0";
        public string twitter_media = "0";
        public string unapproved_media = "0";
        public string all_media = "0";

        private overrideSocial.mediaManager _media = new overrideSocial.mediaManager();
        private overrideSocial.stats _stats = new overrideSocial.stats();
        private overrideSocial.dropbox _dropbox = new overrideSocial.dropbox();
        private overrideSocial.permissions _permissions = new overrideSocial.permissions();

        protected void Page_Load(object sender, EventArgs e)
        {
            permissions();;

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

            render_stats(10);
        }

        private void render_stats(Int32 cnt)
        {
            foreach (Statistic s in _stats.get_top(cnt, Convert.ToInt32(Session["event_id"].ToString())))
            {
                ph_imports.Controls.Add(new LiteralControl("<tr><td>" + s.id.ToString() + "</td><td>" + s.pulldate.ToShortDateString() + " " + s.pulldate.ToShortTimeString() + "</td><td><span class=\"label label-success\">Success</span></td><td>" + s.instagram.ToString() + "</td><td>" + s.twitter.ToString() + "</td><td>" + s.facebook.ToString() + "</td><td>" + s.total.ToString() + "</td></tr>"));
            }
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