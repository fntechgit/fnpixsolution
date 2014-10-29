using System;
using Newtonsoft.Json;
using InstaSharp;

// test
using System.IO;

namespace overrideSocial
{
    public class instagram
    {
        settings _settings = new settings();
        mediaManager _media = new mediaManager();

        // pull by Instagram Hashtag
        public Int32 fetch(string tag, Int32 count, Int32 event_id, Int32 tag_id)
        {
            Int32 total_count = 0;

            var config = new InstagramConfig("https://api.instagram.com/v1", "https://api.instagram.com/oauth", _settings.instagram_client_id(), _settings.instagram_client_secret(), "http://overridepro.com/portfolio");

            var tag_photos = new InstaSharp.Endpoints.Tags.Unauthenticated(config).Recent(tag, count);

            Media m = new Media();

            foreach (var item in tag_photos.Data)
            {
                m.full_name = item.User.FullName;
                m.username = item.User.Username;
                m.profilepic = item.User.ProfilePicture;
                m.source_id = item.Id;
                m.service = "Instagram";
                m.source = item.Images.StandardResolution.Url;
                m.width = item.Images.StandardResolution.Width;
                m.height = item.Images.StandardResolution.Height;
                m.link = item.Link;
                m.event_id = event_id;
                m.tag_id = tag_id;

                dynamic dyn = JsonConvert.DeserializeObject(item.Caption);

                if (dyn != null)
                {
                    m.description = dyn.text;    
                }
                
                m.createdate = item.CreatedTime;
                m.likes = item.Likes.Count;

                if (item.Location != null)
                {
                    m.location_name = item.Location.Name;
                    m.latitude = item.Location.Latitude.ToString();
                    m.longitude = item.Location.Longitude.ToString();
                }

                m.tags = "#" + string.Join(" #", item.Tags);

                _media.add(m);

                total_count++;
            }

            return total_count;
        }

        // pull by Instagram username
        public Int32 fetch(string username, Int32 count, Boolean is_user, Int32 event_id, Int32 tag_id)
        {
            Int32 total_count = 0;

            if (!is_user)
            {
                // get by hashtag
                return fetch(username, count, event_id, tag_id);
            }
            else
            {
                // get by user
                var config = new InstagramConfig("https://api.instagram.com/v1", "https://api.instagram.com/oauth", _settings.instagram_client_id(), _settings.instagram_client_secret(), "http://overridepro.com/portfolio");

                var theusers = new InstaSharp.Endpoints.Users.Unauthenticated(config).Search(username, 1);

                Int32 user_id = 0;

                foreach (var r in theusers.Data)
                {
                    user_id = Convert.ToInt32(r.Id);
                }

                var tag_photos = new InstaSharp.Endpoints.Tags.Unauthenticated(config).RecentByUser(user_id.ToString());

                Media m = new Media();

                foreach (var item in tag_photos.Data)
                {
                    m.full_name = item.User.FullName;
                    m.username = item.User.Username;
                    m.profilepic = item.User.ProfilePicture;
                    m.source_id = item.Id;
                    m.service = "Instagram";
                    m.source = item.Images.StandardResolution.Url;
                    m.width = item.Images.StandardResolution.Width;
                    m.height = item.Images.StandardResolution.Height;
                    m.link = item.Link;
                    m.event_id = event_id;
                    m.tag_id = tag_id;

                    dynamic dyn = JsonConvert.DeserializeObject(item.Caption);

                    m.description = dyn.text;

                    m.createdate = item.CreatedTime;
                    m.likes = item.Likes.Count;

                    if (item.Location != null)
                    {
                        m.location_name = item.Location.Name;
                        m.latitude = item.Location.Latitude.ToString();
                        m.longitude = item.Location.Longitude.ToString();
                    }

                    m.tags = "#" + string.Join(" #", item.Tags);

                    _media.add(m);

                    total_count++;
                }

                return total_count;
            }
        }
    }
}
