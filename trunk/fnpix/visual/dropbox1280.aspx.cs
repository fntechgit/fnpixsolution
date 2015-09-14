using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using overrideSocial;

namespace fnpix.visual
{
    public partial class dropbox1280 : System.Web.UI.Page
    {
        public string max_width = @"1280";
        public string max_height = @"720";
        public string delay = @"3000";

        protected void Page_Load(object sender, EventArgs e)
        {
            overrideSocial.dropbox _dropbox = new overrideSocial.dropbox();

            if (Page.RouteData.Values["delay"] != null)
            {
                delay = Page.RouteData.Values["delay"].ToString() + "000";
            }

            foreach (Dropbox d in _dropbox.select_list(Convert.ToInt32(Page.RouteData.Values["id"] as string), true))
            {
                ph_images.Controls.Add(new LiteralControl("<li data-thumb=\"" + d.stream + "\"></li>"));
            }
        }
    }
}