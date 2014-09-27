﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InstaSharp.Model.Responses;
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
        public Boolean fetch(string tag, Int32 count)
        {
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
            }

            return true;
        }

        // pull by Instagram username
        public Boolean fetch(string username, Int32 count, Boolean is_user)
        {
            if (!is_user)
            {
                // get by hashtag
                return fetch(username, count);
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
                }

                return true;
            }
        }
    }
}
