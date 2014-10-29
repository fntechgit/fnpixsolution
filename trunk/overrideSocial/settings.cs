using System;
using System.Linq;
using RestSharp.Extensions;

namespace overrideSocial
{
    public class settings
    {
        FNTech_MediaDataContext db = new FNTech_MediaDataContext();

        #region facebook

        public string facebook_app_id()
        {
            return @"281783528685773";
        }

        public string facebook_app_secret()
        {
            return @"b6b95663dd5748e6ee020a4ece7488b8";
        }

        public string facebook_app_token()
        {
            return @"281783528685773|rbrOy3LPSXQ7yUwzmferw8A3r2c";
        }

        #endregion

        #region instagram

        public string instagram_client_id()
        {
            return @"9bf8632ed4c64925bace180803a89b05";
        }

        public string instagram_client_secret()
        {
            return @"700cfd3373344a338763d78eba3abd22";
        }

        #endregion

        #region twitter

        public string twitter_api_key()
        {
            return @"qzWV5F02vuWwyqyHkW1YQ1Tiy";
        }

        public string twitter_api_secret()
        {
            return @"PxI7UKtG7mxvt2RixH2PUvOaw1MOor5aBdSmMWjpiPVyu5aqf3";
        }

        public string twitter_access_token()
        {
            return @"22405550-1FoDgNozlwgrkFjCaY1zWgTtTOZ3kN8XmQfq3vif8";
        }

        public string twitter_access_token_secret()
        {
            return @"Zljk61eXQnsTnGu6PMIeRupmMdKYm8dzykEx3YbB79Zu5";
        }

        #endregion

        #region dropbox

        public string dropbox_api_key()
        {
            return get_by_id(2);
        }

        public string dropbox_api_secret()
        {
            return get_by_id(3);
        }

        public string dropbox_return_url()
        {
            return get_by_id(4);
        }

        public Boolean dropbox_moderate()
        {
            if (get_by_id(5) == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion

        public Boolean moderate_event()
        {
            return true;
        }

        public Int32 refresh_count()
        {
            return Convert.ToInt32(get_by_id(1));
        }

        #region dynamic settings 

        public string get_by_id(Int32 id)
        {
            setting s = db.settings.Single(x => x.id == id);

            return s.value;
        }

        #endregion
    }

    public class Setting
    {
        public Int32 id { get; set; }
        public string name { get; set; }
        public string value { get; set; }
    }
}
