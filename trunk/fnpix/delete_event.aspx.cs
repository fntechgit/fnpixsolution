using System;

namespace fnpix
{
    public partial class delete_event : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            overrideSocial.events _events = new overrideSocial.events();

            _events.delete(Convert.ToInt32(Page.RouteData.Values["id"] as string));

            Response.Redirect("/events");
        }
    }
}