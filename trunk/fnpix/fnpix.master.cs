using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fnpix
{
    public partial class fnpix : System.Web.UI.MasterPage
    {
        public string unapproved_count = "0";
        public string user_picture = string.Empty;
        public string user_name = string.Empty;
        public string company_name = string.Empty;
        public string user_email = string.Empty;

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