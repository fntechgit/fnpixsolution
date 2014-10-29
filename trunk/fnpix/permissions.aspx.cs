using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using overrideSocial;

namespace fnpix
{
    public partial class permissions : System.Web.UI.Page
    {
        public string total_media = "0";
        public string facebook_media = "0";
        public string instagram_media = "0";
        public string twitter_media = "0";
        public string unapproved_media = "0";
        public string all_media = "0";
        public string user_name = string.Empty;

        private overrideSocial.mediaManager _media = new overrideSocial.mediaManager();
        private overrideSocial.users _users = new overrideSocial.users();
        private overrideSocial.permissions _permissions = new overrideSocial.permissions();

        protected void Page_Load(object sender, EventArgs e)
        {
            get_totals();

            if (Session["user_access"] != null)
            {
                check_levels(Session["user_access"] as string);
            }

            // render permissions for user
            User u = _users.get_by_id(Convert.ToInt32(Page.RouteData.Values["id"] as string));

            user_name = u.first_name + " " + u.last_name;

            add_permission_link.NavigateUrl = "/permissions/add/" + u.id;
            edit_user_link.NavigateUrl = "/users/edit/" + u.id;

            foreach (Permission p in _permissions.get_by_user(u.id))
            {
                ph_tags.Controls.Add(new LiteralControl("<tr><td data-title=\"Event\">" + p.event_name + "</td><td data-title=\"Event Date\" class=\"hidden-xs hidden-sm\">" + p.event_date.ToShortDateString() + "</td><td data-title=\"Assigned Date\" class=\"hidden-xs hidden-sm\">" + p.assigned_date.ToShortDateString() + "</td><td data-title=\"Assigned by\" class=\"hidden-xs hidden-sm\">" + p.assigned_by_name + "</td><td data-title=\"Level\" class=\"text-right\">" + p.security_level + "</td><td data-title=\"Actions\"><a href=\"/permissions/edit/" + p.user_id + "/" + p.id + "\"><i class=\"fa fa-edit\"></i></a> <a href=\"/permissions/delete/" + p.user_id + "/" + p.id + "\"><i class=\"fa fa-trash-o\"></i></a></td></tr>"));
            }
        }

        private void get_totals()
        {
            List<Media> _all = _media.get_all(Convert.ToInt32(Session["event_id"].ToString()));
            List<Media> _twitter = _media.get_twitter(Convert.ToInt32(Session["event_id"].ToString()));
            List<Media> _instagram = _media.get_instagram(Convert.ToInt32(Session["event_id"].ToString()));
            List<Media> _unapproved = _media.get_unapproved(Convert.ToInt32(Session["event_id"].ToString()));

            total_media = _all.Count.ToString("0.#");
            all_media = total_media;
            instagram_media = _instagram.Count.ToString("0.#");
            twitter_media = _twitter.Count.ToString("0.#");
            unapproved_media = _unapproved.Count.ToString("0.#");
        }

        private void check_levels(string user_level)
        {
            switch (user_level)
            {
                case "system":
                    event_link.Visible = true;
                    break;
                case "event":
                    break;
                case "content":
                    break;
            }
        }
    }
}