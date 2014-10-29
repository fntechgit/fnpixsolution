using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using overrideSocial;

namespace fnpix.visual
{
    public partial class dropbox : System.Web.UI.Page
    {
        private overrideSocial.dropbox _dropbox = new overrideSocial.dropbox();

        protected void Page_Load(object sender, EventArgs e)
        {
            Int32 event_id = Convert.ToInt32(Page.RouteData.Values["id"] as string);

            foreach (Dropbox d in _dropbox.select_list(event_id, true))
            {
                ph_images.Controls.Add(new LiteralControl("<img src=\"" + d.stream + "\" />"));
            }
        }
    }
}