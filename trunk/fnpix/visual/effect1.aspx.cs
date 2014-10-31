using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using overrideSocial;

namespace fnpix.visual
{
    public partial class effect1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            overrideSocial.mediaManager _media = new overrideSocial.mediaManager();

            Int32 event_id = Convert.ToInt32(Page.RouteData.Values["id"] as string);

            foreach (Media m in _media.get_instagram(event_id))
            {
                ph_instagram_images_1.Controls.Add(new LiteralControl("<img src=\"" + m.source + "\" width=\"1080\" />"));
            }

            foreach (Media m in _media.get_twitter(event_id))
            {
                ph_twitter_images.Controls.Add(new LiteralControl("<img src=\"" + m.source + "\" width=\"840\" />"));
            }

            foreach (Media m2 in _media.get_twitter_reverse(event_id))
            {
                ph_twitter_reverse.Controls.Add(new LiteralControl("<img src=\"" + m2.source + "\" width=\"840\" />"));
            }
        }
    }
}