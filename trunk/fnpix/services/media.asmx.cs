using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Web;
using System.Web.Services;
using overrideSocial;

namespace fnpix.services
{
    /// <summary>
    /// Summary description for media
    /// </summary>
    [WebService(Namespace = "http://fnpix.fntech.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    
    [System.Web.Script.Services.ScriptService]
    public class media : System.Web.Services.WebService
    {
        private overrideSocial.mediaManager _media = new overrideSocial.mediaManager();
        private overrideSocial.events _events = new overrideSocial.events();
        private overrideSocial.displays _displays = new overrideSocial.displays();

        [WebMethod(Description = "Approve Items Async", EnableSession = true)]
        public Boolean approve_list(string[] images)
        {
                //foreach (string s in images)
                //{
                //    Media m = _media.get_by_id(Convert.ToInt32(s));

                //    _media.approve(m);
                //}

                return true;
        }

        [WebMethod(Description = "Approve Single Item", EnableSession = true)]
        public Boolean approve(Int32 image)
        {
            if (Context.Session["user_id"] != null)
            {
                Media m = _media.get_by_id(image);

                _media.approve(m);

                return true;
            }
            else
            {
                return false;
            }
        }

        [WebMethod(Description = "Check for Force Refresh", EnableSession = true)]
        public Boolean force_refresh(Int32 event_id)
        {
            return _events.check_for_refresh(event_id);
        }

        [WebMethod(Description = "Get Current Display", EnableSession = true)]
        public Display find(Int32 event_id)
        {
            List<Display> displays = _displays.@select(event_id, DateTime.Now);

            return displays[0];
        }

    }
}
