using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fnpix
{
    public partial class dropbox_update : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.RouteData.Values["status"] != null)
            {
                overrideSocial.dropbox _dropbox = new overrideSocial.dropbox();

                // process it
                switch (Page.RouteData.Values["status"] as string)
                {
                    case @"approve":
                        _dropbox.approve(Convert.ToInt32(Page.RouteData.Values["id"] as string), Convert.ToInt32(Session["user_id"].ToString()));
                        break;
                    case @"unapprove":
                        _dropbox.unapprove(Convert.ToInt32(Page.RouteData.Values["id"] as string));
                        break;
                }
            }

            Response.Redirect("/dropbox");
        }
    }
}