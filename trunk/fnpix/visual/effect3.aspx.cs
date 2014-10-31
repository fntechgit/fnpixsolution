using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using overrideSocial;

namespace fnpix.visual
{
    public partial class effect3 : System.Web.UI.Page
    {

        public string max_width = @"300";
        public string max_height = @"240";
        public string delay = @"1000";

        protected void Page_Load(object sender, EventArgs e)
        {
            overrideSocial.mediaManager _media = new overrideSocial.mediaManager();

            foreach (Media m in _media.get_all(Convert.ToInt32(Page.RouteData.Values["id"] as string), true))
            {
                ph_images.Controls.Add(new LiteralControl("<li data-thumb=\"" + m.source + "\"></li>"));
            }
        }
    }
}