using System;
using System.Collections.Generic;
using System.Web.UI;
using overrideSocial;

namespace fnpix
{
    public partial class users : System.Web.UI.Page
    {
        public string total_media = "0";
        public string facebook_media = "0";
        public string instagram_media = "0";
        public string twitter_media = "0";
        public string unapproved_media = "0";
        public string all_media = "0";

        private overrideSocial.mediaManager _media = new overrideSocial.mediaManager();
        private overrideSocial.users _users = new overrideSocial.users();
        private overrideSocial.dropbox _dropbox = new overrideSocial.dropbox();

        protected void Page_Load(object sender, EventArgs e)
        {
            permissions();

            get_totals();

            string is_active = "ACTIVE";

            foreach (User u in _users.get_all())
            {
                is_active = u.active ? "ACTIVE" : "INACTIVE";

                ph_users.Controls.Add(new LiteralControl("<tr><td data-title=\"Avatar\"><figure class=\"profile-picture\"><img src=\"/uploads/" + u.picture + "\" alt=\"" + u.first_name + " " + u.last_name + "\" class=\"img-circle\" width=\"35\" /></figure></td><td data-title=\"Full Name\">" + u.first_name + " " + u.last_name + "</td><td data-title=\"Email\">" + u.email + "</td><td data-title=\"Company\">" + u.company + "</td><td data-title=\"Active\" class=\"hidden-xs hidden-sm\">" + is_active + "</td><td data-title=\"Created\" class=\"hidden-xs hidden-sm\">" + u.created.ToShortDateString() + " @ " + u.created.ToShortTimeString() + "</td><td data-title=\"Notify\" class=\"hidden-xs hidden-sm\">" + u.notify_every_minutes.ToString("0.#") + " mins</td><td data-title=\"Actions\"><a href=\"/users/edit/" + u.id + "\"><i class=\"fa fa-edit\"></i></a> <a href=\"/users/delete/" + u.id + "\"><i class=\"fa fa-trash-o\"></i></a> <a href=\"/permissions/" + u.id + "\"><i class=\"fa fa-key\"></i></a></td></tr>"));
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