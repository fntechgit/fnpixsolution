using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace fnpix.visual
{
    public partial class schedule : System.Web.UI.Page
    {
        private overrideSocial.events _events = new overrideSocial.events();

        protected void Page_Load(object sender, EventArgs e)
        {
            Int32 event_id = Convert.ToInt32(Page.RouteData.Values["id"] as string);

            _events.force_refresh(event_id);

            hdn_event_id.Value = event_id.ToString();

            overrideSocial.Event ev = _events.@select(event_id);

            if (!string.IsNullOrEmpty(ev.background_1920))
            {
                bdy.Attributes.CssStyle.Add("background-image", "/uploads/" + ev.background_1920);
            }
        }
    }
}