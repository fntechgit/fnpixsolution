using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fnpix.visual
{
    public partial class mixed_twitter_and_instagram : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            Random rnd = new Random();

            Int32 event_id = Convert.ToInt32(Page.RouteData.Values["id"] as string);

            string resolution = string.Empty;

            if (Page.RouteData.Values["resolution"] != null)
            {
                if (Page.RouteData.Values["resolution"].ToString() == "1280")
                {
                    resolution = "-" + Page.RouteData.Values["resolution"] as string;
                }
            }

            string timing = string.Empty;

            if (Page.RouteData.Values["display"] != null)
            {
                timing = "/" + Page.RouteData.Values["display"] as string;
            }

            if (rnd.Next(0, 2) == 0)
            {
                Response.Redirect("/displays/instagram" + resolution + "/" + event_id + timing);
            }
            else
            {
                Response.Redirect("/displays/twitter" + resolution + "/" + event_id + timing);
            }
        }
    }
}