using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facebook;

namespace overrideSocial
{
    public class facebook
    {
        settings _settings = new settings();

        public string get_access_token()
        {
            FacebookClient _client = new FacebookClient();

            string access_token = string.Empty;

            dynamic result = _client.Get("oauth/access_token", new
            {
                client_id = _settings.facebook_app_id(),
                client_secret = _settings.facebook_app_secret(),
                redirect_uri = "http://overridepro.com/test.html",
                code = access_token
            });

            return access_token;
        }
    }
}
