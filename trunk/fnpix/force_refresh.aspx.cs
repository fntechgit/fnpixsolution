using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using overrideSocial;

namespace fnpix
{
    public partial class force_refresh : System.Web.UI.Page
    {
        private overrideSocial.events _events = new overrideSocial.events();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            _events.queue_force_refresh(Convert.ToInt32(Page.RouteData.Values["id"] as string));

            Response.Redirect(Request.UrlReferrer.AbsoluteUri);
        }
    }
}