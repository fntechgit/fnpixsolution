using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using overrideSocial;
using DropNet;

namespace fnpix
{
    public partial class dropbox : System.Web.UI.Page
    {
        public string add_edit = "Add";

        public string total_media = "0";
        public string facebook_media = "0";
        public string instagram_media = "0";
        public string twitter_media = "0";
        public string unapproved_media = "0";
        public string all_media = "0";

        private overrideSocial.mediaManager _media = new overrideSocial.mediaManager();
        private overrideSocial.events _events = new overrideSocial.events();
        private overrideSocial.settings _settings = new overrideSocial.settings();

        protected void Page_Load(object sender, EventArgs e)
        {
            get_totals();

            DropNetClient _client = new DropNetClient(_settings.dropbox_api_key(), _settings.dropbox_api_secret());

            check_levels(Session["user_access"] as string);

            if (Session["request_token"] != null)
            {
                Event ev = _events.@select(Convert.ToInt32(Session["dropbox_event_id"] as string));

                _client.UserLogin = Session["request_token"] as DropNet.Models.UserLogin;

                var access_token = _client.GetAccessToken();

                var account_info = _client.AccountInfo();

                ev.request_token = access_token.Token.ToString();
                ev.access_token = access_token.Secret.ToString();
                ev.dropbox_country = account_info.country;
                ev.dropbox_email = account_info.email;
                ev.dropbox_quota = account_info.quota_info.quota.ToString();
                ev.dropbox_referral = account_info.referral_link;
                ev.dropbox_uid = account_info.uid;
                ev.dropbox_username = account_info.display_name;

                ev = _events.update(ev);

                username.Attributes.Add("placeholder", account_info.display_name);
                email.Attributes.Add("placeholder", ev.dropbox_email);
                quota.Attributes.Add("placeholder", ev.dropbox_quota);
                referral.Attributes.Add("placeholder", ev.dropbox_referral);
                uid.Attributes.Add("placeholder", ev.dropbox_uid.ToString());
                country.Attributes.Add("placeholder", ev.dropbox_country);
            }
            else
            {
                Response.Redirect("/events");
            }
        }

        private void get_totals()
        {
            List<Media> _all = _media.get_all(Convert.ToInt32(Session["event_id"].ToString()));
            List<Media> _twitter = _media.get_twitter(Convert.ToInt32(Session["event_id"].ToString()));
            List<Media> _instagram = _media.get_instagram(Convert.ToInt32(Session["event_id"].ToString()));
            List<Media> _unapproved = _media.get_unapproved(Convert.ToInt32(Session["event_id"].ToString()));

            total_media = _all.Count.ToString("0.#");
            all_media = total_media;
            instagram_media = _instagram.Count.ToString("0.#");
            twitter_media = _twitter.Count.ToString("0.#");
            unapproved_media = _unapproved.Count.ToString("0.#");
        }

        private void check_levels(string user_level)
        {
            switch (user_level)
            {
                case "system":
                    event_link.Visible = true;
                    break;
                case "event":
                    Response.Redirect("/dashboard");
                    break;
                case "content":
                    Response.Redirect("/dashboard");
                    break;
            }
        }
    }
}