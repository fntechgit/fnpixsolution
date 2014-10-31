using System;
using System.Collections.Generic;
using System.IO;
using overrideSocial;

namespace fnpix
{
    public partial class add_user : System.Web.UI.Page
    {
        public string add_edit = "Add";

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
            get_totals();

            permissions();

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
                    User u = _users.get_by_id(Convert.ToInt32(Page.RouteData.Values["id"] as string));

                    first_name.Text = u.first_name;
                    last_name.Text = u.last_name;
                    company.Text = u.company;
                    email.Text = u.email;
                    password.Text = u.password;
                    current_image.ImageUrl = "/uploads/" + u.picture;
                    listenSlider.Value = u.notify_every_minutes.ToString();
                    active.Checked = u.active;
                    security.SelectedValue = u.security.ToString();

                    pnl_current_image.Visible = true;

                    btn_add_permission.NavigateUrl = "/permissions/add/" + u.id;
                    btn_add_permission.Visible = true;
                }
            }
        }

        protected void update(object sender, EventArgs e)
        {
            User u = new User();

            Boolean is_update = false;

            if (Page.RouteData.Values["id"] != null)
            {
                u = _users.get_by_id(Convert.ToInt32(Page.RouteData.Values["id"] as string));

                is_update = true;
            }

            u.first_name = first_name.Text.ToString();
            u.last_name = last_name.Text.ToString();
            u.email = email.Text.ToString();
            u.password = password.Text.ToString();
            u.company = company.Text.ToString();
            u.active = active.Checked;
            u.notify_every_minutes = Convert.ToInt32(listenSlider.Value.ToString());
            u.security = Convert.ToInt32(security.SelectedValue.ToString());

            if (image.HasFile)
            {
                string path = Server.MapPath("~/uploads/");
                string extension = Path.GetExtension(image.FileName.ToString());
                string unique = Guid.NewGuid().ToString();

                image.SaveAs(path + unique + extension);

                u.picture = unique + extension;
            }
            else
            {
                if (!is_update)
                {
                    u.picture = "user.jpg";
                }
            }

            if (is_update)
            {
                _users.update(u);
            }
            else
            {
                u = _users.add(u);
            }

            pnl_success.Visible = true;
            current_image.ImageUrl = "/uploads/" + u.picture;
            pnl_current_image.Visible = true;
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