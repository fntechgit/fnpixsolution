using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using overrideSocial;

namespace fnpix.visual
{
    public partial class twitter_and_instagram : System.Web.UI.Page
    {
        private overrideSocial.functions _functions = new overrideSocial.functions();
        private overrideSocial.mediaManager _media = new overrideSocial.mediaManager();
        private overrideSocial.events _events = new overrideSocial.events();

        public string delay = @"12";

        protected void Page_Load(object sender, EventArgs e)
        {
            Int32 event_id = Convert.ToInt32(Page.RouteData.Values["id"] as string);

            overrideSocial.Event ev = _events.@select(event_id);

            if (!string.IsNullOrEmpty(ev.background_1920))
            {
                bdy.Attributes.CssStyle.Add("background-image", "/uploads/" + ev.background_1920);
            }

            if (Page.RouteData.Values["display"] != null)
            {
                hdn_interval.Value = "12";//Page.RouteData.Values["delay"].ToString();
                delay = Page.RouteData.Values["display"] as string;
                delay = delay + @"000";

                hdn_interval.Value = delay;
            }

            List<Media> med = _media.get_twitter_reverse(event_id);

            med.AddRange(_media.get_instagram_reverse(event_id)); 

            hdn_max.Value = (med.Count - 1).ToString();

            Int32 i = 1;

            foreach (Media m in med)
            {
                ph_photos.Controls.Add(new LiteralControl("<div id=\"content" + i + "\" class=\"content\"><div class=\"photo_holder\"><img src=\"" + m.source + "\" style=\"width:100%;max-height:100%;margin: auto auto;\" /></div><div class=\"content_holder\"><div class=\"userinfo\"><span class=\"fullname\">" + m.full_name + "</span><span class=\"username\">" + m.username + "</span><br /><span class=\"postdate\">" + _functions.relative_time(m.createdate) + "</span></div><div class=\"profile-pic\"><img src=\"" + m.profilepic.Replace("_normal", "") + "\" width=\"150\" height=\"150\" /></div><br clear=\"all\"/><br clear=\"all\"/><br clear=\"all\"/><div class=\"description\">" + m.description + "</div><br clear=\"all\"/><br clear=\"all\"/>"));

                if (!string.IsNullOrEmpty(m.latitude) && m.latitude != "0")
                {
                    // render the map
                    ph_photos.Controls.Add(new LiteralControl("<div id=\"map" + i + "\" class=\"map\"><img src=\"http://maps.googleapis.com/maps/api/staticmap?center=" + m.latitude + "," + m.longitude + "&zoom=13&scale=false&size=400x250&maptype=roadmap&sensor=false&format=png&visual_refresh=true&markers=size:mid%7Ccolor:red%7C" + m.latitude + "," + m.longitude + "\" /><br />" + m.location_name + "</div>"));
                }

                ph_photos.Controls.Add(new LiteralControl("</div></div>"));

                i++;
            }
        }
    }
}