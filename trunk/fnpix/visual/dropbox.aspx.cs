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

        public string timeout = "2000";

        protected void Page_Load(object sender, EventArgs e)
        {
            Int32 event_id = Convert.ToInt32(Page.RouteData.Values["id"] as string);

            string fx1 = @"tileSlide";
            string fx1_vert = @"false";

            string fx2 = @"tileBlind";
            string fx2_vert = @"true";

            string fx3 = @"tileSlide";
            string fx3_vert = @"true";

            string fx4 = @"tileBlind";
            string fx4_vert = @"false";

            Int32 i = 1;

            string fx = string.Empty, vert = string.Empty, tiles = string.Empty;

            Random rnd = new Random();

            foreach (Dropbox d in _dropbox.select_list(event_id, true))
            {
                switch (i)
                {
                    case 1:
                        fx = fx1;
                        vert = fx1_vert;
                        tiles = rnd.Next(4, 30).ToString();
                        break;
                    case 2:
                        fx = fx2;
                        vert = fx2_vert;
                        tiles = rnd.Next(4, 30).ToString();
                        break;
                    case 3:
                        fx = fx3;
                        vert = fx3_vert;
                        tiles = rnd.Next(4, 30).ToString();
                        break;
                    case 4:
                        fx = fx4;
                        vert = fx4_vert;
                        tiles = rnd.Next(4, 30).ToString();
                        i = 0;
                        break;
                    default:
                        fx = fx1;
                        vert = fx1_vert;
                        tiles = rnd.Next(4, 30).ToString();
                        i = 0;
                        break;
                }


                ph_images.Controls.Add(new LiteralControl("<img src=\"" + d.stream + "\" data-cycle-fx=\"" + fx + "\" data-cycle-tile-vertical=" + vert + " data-cycle-tile-count=" + tiles + " />"));

                i++;
            }
        }
    }
}