using System;
using System.Collections.Generic;
using System.Web.UI;
using overrideSocial;

namespace fnpix
{
    public partial class media : System.Web.UI.Page
    {
        public string total_media = "0";
        public string facebook_media = "0";
        public string instagram_media = "0";
        public string twitter_media = "0";
        public string unapproved_media = "0";
        public string all_media = "0";

        private overrideSocial.mediaManager _media = new overrideSocial.mediaManager();
        private overrideSocial.dropbox _dropbox = new overrideSocial.dropbox();

        protected void Page_Load(object sender, EventArgs e)
        {
            permissions();

            get_totals();

            if (Page.RouteData.Values["unapproved"] != null)
            {
                string status = Page.RouteData.Values["unapproved"].ToString();

                if (status == "unapproved")
                {
                    render_media(_media.get_unapproved(Convert.ToInt32(Session["event_id"].ToString())));
                }
                else
                {
                    render_media(_media.get_all(Convert.ToInt32(Session["event_id"].ToString()), true));   
                }
            }
            else
            {
                render_media(_media.get_all(Convert.ToInt32(Session["event_id"].ToString())));
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

        private void render_media(List<Media> result)
        {
            foreach (Media m in result)
            {
                pn_photos.Controls.Add(new LiteralControl("<div class=\"isotope-item " + m.service.ToLower() + " col-sm-6 col-md-4 col-lg-3\"><div class=\"thumbnail\"><div class=\"thumb-preview\"><a class=\"thumb-image\" href=\"" + m.source + "\"><img src=\"" + m.source + "\" class=\"img-responsive\" alt=\"" + m.description + "\"></a><div class=\"mg-thumb-options\"><div class=\"mg-zoom\"><i class=\"fa fa-search\"></i></div><div class=\"mg-toolbar\"><div class=\"mg-option checkbox-custom checkbox-inline\"><input type=\"checkbox\" id=\"file_" + m.id.ToString() + "\" value=\"" + m.id.ToString() + "\"><label for=\"file_" + m.id.ToString() + "\">SELECT</label></div><div class=\"mg-group pull-right\"><a href=\"/media/approve/" + m.id + "\">APPROVE</a><button class=\"dropdown-toggle mg-toggle\" type=\"button\" data-toggle=\"dropdown\"><i class=\"fa fa-caret-up\"></i></button><ul class=\"dropdown-menu mg-menu\" role=\"menu\"><li><a href=\"" + m.source + "\"><i class=\"fa fa-download\"></i> Download</a></li></ul></div></div></div></div><h5 class=\"mg-title text-semibold\">" + m.full_name + "<small> <a href=\"" + m.link + "\" target=\"_blank\">Detail</a></small></h5><div class=\"mg-description\"><small class=\"pull-left text-muted\">" + m.tags + "</small><small class=\"pull-right text-muted\">" + m.createdate.ToShortDateString() + "</small></div></div></div>"));
            }
        }
    }
}