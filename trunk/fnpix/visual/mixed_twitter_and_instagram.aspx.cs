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

            if (rnd.Next(0, 1) == 0)
            {
                Response.Redirect("/displays/instagram/" + event_id);
            }
            else
            {
                Response.Redirect("/displays/twitter/" + event_id);
            }
        }
    }
}