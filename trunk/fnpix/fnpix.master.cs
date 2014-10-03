using System;
using System.Collections.Generic;
using overrideSocial;

namespace fnpix
{
    public partial class fnpix : System.Web.UI.MasterPage
    {
        public string unapproved_count = "0";
        public string user_picture = string.Empty;
        public string user_name = string.Empty;
        public string company_name = string.Empty;
        public string user_email = string.Empty;

        private overrideSocial.mediaManager _media = new overrideSocial.mediaManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user_id"] == null)
            {
                Response.Redirect("/login");
            }
            else
            {
                user_picture = "/uploads/" + Session["user_pic"].ToString();
                user_name = Session["user_name"].ToString();
                company_name = Session["company_name"].ToString();
                user_email = Session["user_email"].ToString();
            }

            List<Media> _unapproved = _media.get_unapproved();

            unapproved_count = _unapproved.Count.ToString("0.#");
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["user_id"] == null)
            {
                Response.Redirect("/login");
            }
            else
            {
                user_picture = "/uploads/" + Session["user_pic"].ToString();
                user_name = Session["user_name"].ToString();
                company_name = Session["company_name"].ToString();
                user_email = Session["user_email"].ToString();
            }
        }
    }
}