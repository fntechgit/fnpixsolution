using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using overrideSocial;


namespace fnpix
{
    public partial class dropbox_media : System.Web.UI.Page
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
            get_totals();

            if (Page.RouteData.Values["unapproved"] != null)
            {
                render_media(_dropbox.select_list(Convert.ToInt32(Session["event_id"].ToString()), false));
            }
            else
            {
                render_media(_dropbox.select_list(Convert.ToInt32(Session["event_id"].ToString())));
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

        private void render_media(List<Dropbox> result)
        {
            string approve = "approved";

            foreach (Dropbox m in result)
            {
                if (m.approved)
                {
                    approve = "approved";
                }
                else
                {
                    approve = "unapproved";
                }

                pn_photos.Controls.Add(new LiteralControl("<div class=\"isotope-item " + approve + " col-sm-6 col-md-4 col-lg-3\"><div class=\"thumbnail\"><div class=\"thumb-preview\"><a class=\"thumb-image\" href=\"" + m.stream + "\"><img src=\"" + m.stream + "\" class=\"img-responsive\" alt=\"" + m.filename + "\"></a><div class=\"mg-thumb-options\"><div class=\"mg-zoom\"><i class=\"fa fa-search\"></i></div><div class=\"mg-toolbar\"><div class=\"mg-option checkbox-custom checkbox-inline\"><input type=\"checkbox\" id=\"file_" + m.id.ToString() + "\" value=\"" + m.id.ToString() + "\"><label for=\"file_" + m.id.ToString() + "\">SELECT</label></div><div class=\"mg-group pull-right\"><a href=\"/dropbox/approve/" + m.id + "\">APPROVE</a><button class=\"dropdown-toggle mg-toggle\" type=\"button\" data-toggle=\"dropdown\"><i class=\"fa fa-caret-up\"></i></button><ul class=\"dropdown-menu mg-menu\" role=\"menu\"><li><a href=\"" + m.stream + "\"><i class=\"fa fa-download\"></i> Download</a></li></ul></div></div></div></div><h5 class=\"mg-title text-semibold\">" + m.username + "<small> <a href=\"" + m.stream + "\" target=\"_blank\">Detail</a></small></h5><div class=\"mg-description\"><small class=\"pull-left text-muted\">" + m.size + "</small><small class=\"pull-right text-muted\">" + Convert.ToDateTime(m.modified_date).ToShortDateString() + "</small></div></div></div>"));
            }
        }
    }
}