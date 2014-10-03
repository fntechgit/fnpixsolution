using System;
using overrideSocial;

namespace fnpix
{
    public partial class approve : System.Web.UI.Page
    {
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["user_id"] != null)
            {
                overrideSocial.mediaManager _media = new overrideSocial.mediaManager();

                Media m = _media.get_by_id(Convert.ToInt32(Page.RouteData.Values["id"] as string));

                m.approved_by = Convert.ToInt32(Session["user_id"].ToString());
                m.approved_date = DateTime.Now;

                _media.approve(m);

                Response.Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                Response.Redirect("/login");
            }
        }
    }
}