using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using overrideSocial;

namespace fnpix.visual
{
    public partial class master : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            overrideSocial.displays _displays = new overrideSocial.displays();

            Display d = _displays.find_my_display(Convert.ToInt32(Page.RouteData.Values["id"]));

            switch (d.view_id)
            {
                case 1000:
                    Response.Redirect("/displays/mixed1/" + d.event_id + "/" + d.slide_duration);
                    break;
                case 1001:
                    Response.Redirect("/displays/instagram/" + d.event_id + "/" + d.slide_duration);
                    break;
                case 1002:
                    Response.Redirect("/displays/magicwall/" + d.event_id + "/" + d.slide_duration);
                    break;
                case 1003:
                    Response.Redirect("/displays/dropbox/" + d.event_id + "/" + d.slide_duration);
                    break;
                case 1004:
                    Response.Redirect("/displays/twitter/" + d.event_id + "/" + d.slide_duration);
                    break;
                default:

                    d.event_id = Convert.ToInt32(Page.RouteData.Values["id"] as string);
                    d.slide_duration = 1;

                    Response.Redirect("/displays/magicwall/" + d.event_id + "/" + d.slide_duration);
                    break;
            }
        }
    }
}