using System;

namespace fnpix
{
    public partial class delete_tag : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            overrideSocial.tags _tags = new overrideSocial.tags();

            _tags.delete(Convert.ToInt32(Page.RouteData.Values["id"] as string));

            Response.Redirect("/preferences");
        }
    }
}