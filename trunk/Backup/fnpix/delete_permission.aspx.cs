using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using overrideSocial;

namespace fnpix
{
    public partial class delete_permission : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            overrideSocial.permissions _permissions = new overrideSocial.permissions();

            _permissions.delete(Convert.ToInt32(Page.RouteData.Values["id"] as string));

            Response.Redirect("/permissions/" + Page.RouteData.Values["user_id"] as string);
        }
    }
}