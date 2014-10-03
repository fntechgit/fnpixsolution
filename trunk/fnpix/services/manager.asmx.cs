using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Services;
using overrideSocial;

namespace fnpix.services
{
    /// <summary>
    /// Summary description for manager
    /// </summary>
    [WebService(Namespace = "http://fnpix.fntech.com")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class manager : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        private overrideSocial.mediaManager _media = new overrideSocial.mediaManager();

        [WebMethod(Description = "Approve Items Async", EnableSession = true)]
        public Boolean approve_list(string images)
        {
            if (Context.Session["user_id"] != null)
            {
                List<string> _images = images.Split(',').ToList<string>();

                foreach (string s in _images)
                {
                    Media m = _media.get_by_id(Convert.ToInt32(s));

                    m.approved_by = Convert.ToInt32(Context.Session["user_id"] as string);

                    _media.approve(m);
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        [WebMethod(Description = "Unapprove Items Async", EnableSession = true)]
        public Boolean unapprove_list(string images)
        {
            if (Context.Session["user_id"] != null)
            {
                List<string> _images = images.Split(',').ToList<string>();

                foreach (string s in _images)
                {
                    _media.unapprove(Convert.ToInt32(s));
                }

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
