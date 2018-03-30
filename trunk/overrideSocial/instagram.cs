using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using RestSharp;
using System.Threading.Tasks;
using System.Runtime;

// test
using System.IO;
using RestSharp.Deserializers;

namespace overrideSocial
{
    public class instagram
    {
        private string access_token = "7392031217.c32dfe9.99d8df280f0b4501a9aa8c09436455ed";
        //"54772586.9bf8632.352fdf6a439d4f27a43948203f11ce87"

        settings _settings = new settings();
        mediaManager _media = new mediaManager();
        private events _events = new events();
        public Int32 page_count = 0;

        //Get media by user name
        public Int32 fetch_by_username(string tag, Int32 event_id, Int32 tag_id)
        {
            tag = get_userid_by_tag(tag);

            if (string.IsNullOrEmpty(tag))
                return 0;

            var client = new RestClient("https://api.instagram.com/v1/");

            var request = new RestRequest("users/" + tag + "/media/recent");

            request.AddParameter("access_token", access_token);

            IRestResponse response = client.Execute(request);

            RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();

            var mySessions = JsonConvert.DeserializeObject<InstagramModel>(response.Content);

            string next_url = mySessions.pagination.next_url;

            Event ev = _events.@select(event_id);

            foreach (var item in mySessions.data)
            {
                Media m = new Media();

                m.added_to_db_date = DateTime.Now;

                if (ev.moderate == false)
                {
                    m.approved = true;
                    m.approved_by = 1;
                    m.approved_date = DateTime.Now;
                }

                m.createdate = DateTime.Now;
                m.description = item.caption.text;
                m.event_id = event_id;
                m.full_name = item.user.full_name;

                if (item.images.standard_image != null)
                {
                    m.height = Convert.ToInt32(item.images.standard_image.height);
                    m.source = item.images.standard_image.url;
                    m.width = Convert.ToInt32(item.images.standard_image.width);
                }
                else
                {
                    m.height = Convert.ToInt32(item.images.low_resolution.height);
                    m.source = item.images.low_resolution.url;
                    m.width = Convert.ToInt32(item.images.low_resolution.width);
                }

                m.is_video = item.type != "image";

                if (item.location != null)
                {
                    m.latitude = item.location.latitude.ToString();
                    m.location_name = item.location.name;
                    m.longitude = item.location.longitude.ToString();
                }

                m.likes = item.likes.count;
                m.link = item.link;
                m.profilepic = item.user.profile_picture;
                m.service = "Instagram";

                m.source_id = item.id;
                m.tag_id = tag_id;
                m.tags = string.Join(", ", item.tags);
                m.username = item.user.username;


                _media.add(m);
            }

            Int32 return_count = 0;

            page_count = 1;

            return mySessions.data.Count();
        }

        private string get_userid_by_tag(string tag)
        {
            var client = new RestClient("https://api.instagram.com/v1/");

            var request = new RestRequest("users/search");

            request.AddParameter("access_token", access_token);
            request.AddParameter("q", tag);

            IRestResponse response = client.Execute(request);

            RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();

            InstagramModel userData = JsonConvert.DeserializeObject<InstagramModel>(response.Content);
            if (userData != null && userData.data.Count() == 1)
                return userData.data[0].id;

            return null;
        }


        public Int32 fetch2(string tag, Int32 event_id, Int32 tag_id)
        {
            var client = new RestClient("https://api.instagram.com/v1/");

            var request = new RestRequest("tags/" + tag + "/media/recent");

            request.AddParameter("access_token", access_token);
            

            IRestResponse response = client.Execute(request);

            RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();

            //var mySessions = deserial.Deserialize<InstagramModel>(response);

            var mySessions = JsonConvert.DeserializeObject<InstagramModel>(response.Content);

            string next_url = mySessions.pagination.next_url;

            Event ev = _events.@select(event_id);

            foreach (var item in mySessions.data)
            {
                Media m = new Media();

                m.added_to_db_date = DateTime.Now;

                if (ev.moderate == false)
                {
                    m.approved = true;
                    m.approved_by = 1;
                    m.approved_date = DateTime.Now;
                }

                m.createdate = DateTime.Now;
                m.description = item.caption.text;
                m.event_id = event_id;
                m.full_name = item.user.full_name;

                if (item.images.standard_image != null)
                {
                    m.height = Convert.ToInt32(item.images.standard_image.height);
                    m.source = item.images.standard_image.url;
                    m.width = Convert.ToInt32(item.images.standard_image.width);
                }
                else
                {
                    m.height = Convert.ToInt32(item.images.low_resolution.height);
                    m.source = item.images.low_resolution.url;
                    m.width = Convert.ToInt32(item.images.low_resolution.width);
                }
                
                m.is_video = item.type != "image";

                if (item.location != null)
                {
                    m.latitude = item.location.latitude.ToString();
                    m.location_name = item.location.name;
                    m.longitude = item.location.longitude.ToString();
                }
                
                m.likes = item.likes.count;
                m.link = item.link;
                m.profilepic = item.user.profile_picture;
                m.service = "Instagram";
                
                m.source_id = item.id;
                m.tag_id = tag_id;
                m.tags = string.Join(", ", item.tags);
                m.username = item.user.username;
                

                _media.add(m);
            }

            Int32 return_count = 0;

            page_count = 1;

            if (!string.IsNullOrEmpty(next_url))
            {
                fetch_next(next_url, ev, tag_id, mySessions.data.Count());
            }
            else
            {
                return mySessions.data.Count();   
            }

            return return_count;
        }

        public Boolean fetch_next(string url, Event ev, Int32 tag_id, Int32 count)
        {
            var client = new RestClient("https://api.instagram.com/v1/");

            var request = new RestRequest(url.Replace("https://api.instagram.com/v1/", ""));

            IRestResponse response = client.Execute(request);

            RestSharp.Deserializers.JsonDeserializer deserial = new JsonDeserializer();

            //var mySessions = deserial.Deserialize<InstagramModel>(response);

            var mySessions = JsonConvert.DeserializeObject<InstagramModel>(response.Content);

            string next_url = mySessions.pagination.next_url;

            foreach (var item in mySessions.data)
            {
                Media m = new Media();

                m.added_to_db_date = DateTime.Now;

                if (ev.moderate == false)
                {
                    m.approved = true;
                    m.approved_by = 1;
                    m.approved_date = DateTime.Now;
                }

                m.createdate = DateTime.Now;
                m.description = item.caption.text;
                m.event_id = ev.id;
                m.full_name = item.user.full_name;

                if (item.images.standard_image != null)
                {
                    m.height = Convert.ToInt32(item.images.standard_image.height);
                    m.source = item.images.standard_image.url;
                    m.width = Convert.ToInt32(item.images.standard_image.width);
                }
                else
                {
                    m.height = Convert.ToInt32(item.images.low_resolution.height);
                    m.source = item.images.low_resolution.url;
                    m.width = Convert.ToInt32(item.images.low_resolution.width);
                }

                m.is_video = item.type != "image";

                if (item.location != null)
                {
                    m.latitude = item.location.latitude.ToString();
                    m.location_name = item.location.name;
                    m.longitude = item.location.longitude.ToString();
                }

                m.likes = item.likes.count;
                m.link = item.link;
                m.profilepic = item.user.profile_picture;
                m.service = "Instagram";
                m.source_id = m.source;
                m.tag_id = tag_id;
                m.tags = string.Join(", ", item.tags);
                m.username = item.user.username;

                _media.add(m);
            }

            Int32 return_count = 0;

            page_count++;

            if (page_count >= 10)
            {
                return true;
            }
            else
            {

                if (!string.IsNullOrEmpty(next_url))
                {
                    fetch_next(next_url, ev, tag_id, mySessions.data.Count());
                }
                else
                {
                    return true;
                }
            }

            return true;
        }

        

        //// pull by Instagram Hashtag
        //public Int32 fetch(string tag, Int32 event_id, Int32 tag_id)
        //{
        //    Int32 total_count = 0;

        //    var config = new InstagramConfig(_settings.instagram_client_id(), _settings.instagram_client_secret(), "http://overridepro.com/portfolio");

        //    //var tag_photos2 = new InstaSharp.Endpoints.Tags.Unauthenticated(config).Recent(tag, count);

        //    var tag_photos = new InstaSharp.Endpoints.Tags(config).Recent(tag);

        //    Event ev = _events.@select(event_id);

        //    Media m = new Media();

        //        try
        //        {
        //            foreach (var item in tag_photos.Result.Data)
        //            {
        //                m.full_name = item.User.FullName;
        //                m.username = item.User.Username;
        //                m.profilepic = item.User.ProfilePicture;
        //                m.source_id = item.Id;
        //                m.service = "Instagram";
        //                m.source = item.Images.StandardResolution.Url;
        //                m.width = item.Images.StandardResolution.Width;
        //                m.height = item.Images.StandardResolution.Height;
        //                m.link = item.Link;
        //                m.event_id = event_id;
        //                m.tag_id = tag_id;

                        
        //                    m.description = item.Caption.Text;
                        

        //                m.createdate = item.CreatedTime;
        //                m.likes = item.Likes.Count;

        //                if (item.Location != null)
        //                {
        //                    m.location_name = item.Location.Name;
        //                    m.latitude = item.Location.Latitude.ToString();
        //                    m.longitude = item.Location.Longitude.ToString();
        //                }

        //                m.tags = "#" + string.Join(" #", item.Tags);

        //                if (ev.moderate == false)
        //                {
        //                    m.approved = true;
        //                    m.approved_by = 1;
        //                    m.approved_date = DateTime.Now;
        //                }

        //                _media.add(m);

        //                total_count++;
        //            }
        //        }
        //        catch (NullReferenceException ex)
        //        {
        //            Console.WriteLine("No Data Available for #" + tag);    
        //        }
           

        //    return total_count;
        //}

        //// pull by Instagram username
        //public Int32 fetch(string username, Boolean is_user, Int32 event_id, Int32 tag_id)
        //{
        //    Int32 total_count = 0;

        //    if (!is_user)
        //    {
        //        // get by hashtag
        //        return fetch(username, event_id, tag_id);
        //    }
        //    else
        //    {
        //        // get by user
        //        Regex rgx = new Regex("[^A-Za-z0-9]");

        //        username = rgx.Replace(username, "");

        //        var config = new InstagramConfig(_settings.instagram_client_id(), _settings.instagram_client_secret(), "http://overridepro.com/portfolio");

        //        try
        //        {
        //            var theusers = new InstaSharp.Endpoints.Users(config).Search(username, 1);

        //            Int32 user_id = 0;

        //            foreach (var r in theusers.Result.Data)
        //            {
        //                user_id = Convert.ToInt32(r.Id);
        //            }

        //            var tag_photos = new InstaSharp.Endpoints.Users(config).Recent(username);

        //            Media m = new Media();

        //            foreach (var item in tag_photos.Result.Data)
        //            {
        //                m.full_name = item.User.FullName;
        //                m.username = item.User.Username;
        //                m.profilepic = item.User.ProfilePicture;
        //                m.source_id = item.Id;
        //                m.service = "Instagram";
        //                m.source = item.Images.StandardResolution.Url;
        //                m.width = item.Images.StandardResolution.Width;
        //                m.height = item.Images.StandardResolution.Height;
        //                m.link = item.Link;
        //                m.event_id = event_id;
        //                m.tag_id = tag_id;

        //                m.description = item.Caption.Text;

        //                m.createdate = item.CreatedTime;
        //                m.likes = item.Likes.Count;

        //                if (item.Location != null)
        //                {
        //                    m.location_name = item.Location.Name;
        //                    m.latitude = item.Location.Latitude.ToString();
        //                    m.longitude = item.Location.Longitude.ToString();
        //                }

        //                m.tags = "#" + string.Join(" #", item.Tags);

        //                    _media.add(m);

        //                    total_count++;
        //            }
        //        }
        //        catch (JsonReaderException ex)
        //        {
        //            Console.WriteLine(ex.InnerException);
        //        }

        //        return total_count;
        //    }
        //}
    }
}
