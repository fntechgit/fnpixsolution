using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using overrideSocial;

namespace fnpix.visual
{
    public partial class effect4 : System.Web.UI.Page
    {
        public string event_name = @"OpenStack 2014";

        protected void Page_Load(object sender, EventArgs e)
        {
            overrideSocial.mediaManager _media = new overrideSocial.mediaManager();

            foreach(Media m in _media.get_all(Convert.ToInt32(Page.RouteData.Values["id"] as string), true))
            {
                ph_photos.Controls.Add(new LiteralControl("<div class=\"crystal-photo\" title=\"" + m.full_name + "\"><div class=\"crystal-image\" title=\"" + m.source + "\"></div><div class=\"crystal-thumb\" title=\"" + m.source + "\"></div><div class=\"crystal-desc crystal-align-left-center crystal-desc-width-30%\"><h2>" + m.full_name + "</h2><p>" + m.description + "</p></div></div>"));
            }
        }
    }
}