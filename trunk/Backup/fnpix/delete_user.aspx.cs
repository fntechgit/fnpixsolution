using System;

namespace fnpix
{
    public partial class delete_user : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            overrideSocial.users _users = new overrideSocial.users();

            _users.delete(Convert.ToInt32(Page.RouteData.Values["id"] as string));

            Response.Redirect("/users");
        }
    }
}