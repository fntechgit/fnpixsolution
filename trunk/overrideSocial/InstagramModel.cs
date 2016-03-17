using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace overrideSocial
{
    public class InstagramModel
    {
        public Pagination pagination { get; set; }
        public Meta meta { get; set; }
        public List<Data> data { get; set; }
    }

    public class Pagination
    {
        public string next_max_tag_id { get; set; }
        public string depreciation_warning { get; set; }
        public string next_max_id { get; set; }
        public string next_min_id { get; set; }
        public string min_tag_id { get; set; }
        public string next_url { get; set; }
    }

    public class Meta
    {
        public Int32 code { get; set; }
    }

    public class Attribution
    {
        /// <summary>
        /// Gets or sets the website.
        /// </summary>
        /// <value>
        /// The website.
        /// </value>
        public string Website { get; set; }
        /// <summary>
        /// Gets or sets the itunes url.
        /// </summary>
        /// <value>
        /// The itunes url.
        /// </value>
        public string itunes_url { get; set; }
        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        public string Name { get; set; }
    }

    public class Data
    {
        public Attribution attribution { get; set; }
        public string[] tags { get; set; }
        public string type { get; set; }
        public Location location { get; set; }
        public Comments comments { get; set; }
        public string filter { get; set; }
        public string created_time { get; set; }
        public string link { get; set; }
        public Likes likes { get; set; }
        public InstagramImage images { get; set; }
        public Caption caption { get; set; }
        public Boolean? user_has_liked { get; set; }
        public string id { get; set; }
        public InstagramUser user { get; set; }
    }

    public class UserInPhoto
    {
        public Position Position { get; set; }
        public InstagramUser User { get; set; }
    }

    public class Position
    {
        public float Y { get; set; }
        public float X { get; set; }
    }

    public class Location
    {
        public float latitude { get; set; }
        public float longitude { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public Likes likes { get; set; }

    }

    public class Comments
    {
        public Int32 count { get; set; }
        public List<Comment> data { get; set; }
    }

    public class Comment
    {
        public string created_time { get; set; }
        public string text { get; set; }
        public InstagramUser from { get; set; }
        public string id { get; set; }
    }

    public class InstagramUser
    {
        public string id { get; set; }
        public string username { get; set; }
        public string profile_picture { get; set; }
        public string full_name { get; set; }
    }

    public class Likes
    {
        public Int32 count { get; set; }
        public List<InstagramUser> data { get; set; }
    }

    public class InstagramImage
    {
        public iImage low_resolution { get; set; }      
        public iImage thumbnail { get; set; }
        public iImage standard_image { get; set; }
    }

    public class iImage
    {
        public string url { get; set; }   
        public Int32 width { get; set; }
        public Int32 height { get; set; }
    }

    public class Caption
    {
        public string created_time { get; set; }
        public string text { get; set; }
        public InstagramUser from { get; set; }
        public string id { get; set; }
    }
}
