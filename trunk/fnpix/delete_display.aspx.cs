using System;

namespace fnpix
{
    public partial class delete_display : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            overrideSocial.displays _displays = new overrideSocial.displays();

            _displays.delete(Convert.ToInt32(Page.RouteData.Values["id"] as string));

            Response.Redirect("/displays");
        }
    }
}