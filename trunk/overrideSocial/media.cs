using System;
using System.Collections.Generic;
using System.Linq;

namespace overrideSocial
{
    public class mediaManager
    {
        FNTech_MediaDataContext db = new FNTech_MediaDataContext();
        settings _settings = new settings();

        public Media add(Media m)
        {
            media md = new media();

            md.added_to_db_date = DateTime.Now;

            if (!_settings.moderate_event())
            {
                md.approved = true;
                md.approved_by = 1;
                md.approved_date = DateTime.Now;
            }
            else
            {
                md.approved = false;
                md.approved_by = null;
                md.approved_date = null;    
            }

            md.createdate = m.createdate;
            md.description = m.description;
            md.full_name = m.full_name;
            md.latitude = m.latitude;
            md.likes = m.likes;
            md.link = m.link;
            md.location_name = m.location_name;
            md.longitude = m.longitude;
            md.profilepic = m.profilepic;
            md.service = m.service;
            md.username = m.username;
            md.source_id = m.source_id;
            md.width = m.width;
            md.height = m.height;
            md.is_video = m.is_video;
            md.source = m.source;
            md.tags = m.tags;

            db.medias.InsertOnSubmit(md);

            var result = get_recent().Where(x => x.source == md.source).ToList();

            if (!result.Any())
            {
                db.SubmitChanges();    
            }

            m.id = md.id;

            return m;
        }

        public List<Media> get_recent()
        {
            List<Media> _entries = new List<Media>();

            var result = from themedia in db.medias
                orderby themedia.createdate descending
                select themedia;

            foreach (var item in result)
            {
                Media m = new Media();

                m.createdate = Convert.ToDateTime(item.createdate);
                m.description = item.description;
                m.full_name = item.full_name;
                m.id = item.id;
                m.latitude = item.latitude;
                m.likes = item.likes;
                m.link = item.link;
                m.location_name = item.location_name;
                m.longitude = item.longitude;
                m.profilepic = item.profilepic;
                m.service = item.service;
                m.username = item.username;
                m.source_id = item.source_id;
                m.source = item.source;
                m.tags = item.tags;
                m.approved = item.approved;
                m.approved_by = item.approved_by;
                m.approved_date = item.approved_date;
                
                _entries.Add(m);
            }

            return _entries;
        }

        public List<Media> get_reverse()
        {
            List<Media> _entries = new List<Media>();

            var result = from themedia in db.medias
                         orderby themedia.createdate 
                         select themedia;

            foreach (var item in result)
            {
                Media m = new Media();

                m.createdate = Convert.ToDateTime(item.createdate);
                m.description = item.description;
                m.full_name = item.full_name;
                m.id = item.id;
                m.latitude = item.latitude;
                m.likes = item.likes;
                m.link = item.link;
                m.location_name = item.location_name;
                m.longitude = item.longitude;
                m.profilepic = item.profilepic;
                m.service = item.service;
                m.username = item.username;
                m.source_id = item.source_id;
                m.source = item.source;
                m.tags = item.tags;
                m.approved = item.approved;
                m.approved_by = item.approved_by;
                m.approved_date = item.approved_date;

                _entries.Add(m);
            }

            return _entries;
        }

        public List<Media> get_unapproved()
        {
            return get_recent().Where(x => x.approved == false).ToList();
        }

        public List<Media> get_instagram()
        {
            return get_recent().Where(x => x.service == "Instagram").Where(x => x.approved == true).ToList();
        }

        public List<Media> get_instagram_reverse()
        {
            return get_reverse().Where(x => x.service == "Instagram").Where(x => x.approved == true).ToList();
        }

        public List<Media> get_twitter()
        {
            return get_recent().Where(x => x.service == "Twitter").Where(x => x.approved == true).ToList();
        }

        public List<Media> get_twitter_reverse()
        {
            return get_reverse().Where(x => x.service == "Twitter").Where(x => x.approved == true).ToList();
        } 

        public Media approve(Media m)
        {
            media md = db.medias.Single(x => x.id == m.id);

            md.approved = true;
            md.approved_by = m.approved_by;
            md.approved_date = DateTime.Now;

            db.SubmitChanges();

            m.approved_date = DateTime.Now;

            return m;
        }

        public Boolean unapprove(Int32 id)
        {
            media md = db.medias.Single(x => x.id == id);

            md.approved_date = null;
            md.approved = false;
            md.approved_by = null;

            db.SubmitChanges();

            return true;
        }

        public Media get_by_id(Int32 id)
        {
            media m = db.medias.Single(x => x.id == id);

            Media md = new Media();

            md.approved = m.approved;
            md.createdate = Convert.ToDateTime(m.createdate);
            md.description = m.description;
            md.full_name = m.full_name;
            md.latitude = m.latitude;
            md.likes = m.likes;
            md.link = m.link;
            md.location_name = m.location_name;
            md.longitude = m.longitude;
            md.profilepic = m.profilepic;
            md.service = m.service;
            md.username = m.username;
            md.source_id = m.source_id;
            md.width = m.width;
            md.height = m.height;
            md.is_video = m.is_video;
            md.source = m.source;
            md.tags = m.tags;
            md.approved_by = m.approved_by;
            md.approved_date = m.approved_date;
            md.id = m.id;

            return md;
        }
    }

    public class Media
    {
        public Int32 id { get; set; }
        public string service { get; set; }
        public string username { get; set; }
        public string full_name { get; set; }
        public string description { get; set; }
        public DateTime createdate { get; set; }
        public string profilepic { get; set; }
        public string link { get; set; }
        public Int32 likes { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public Boolean approved { get; set; }
        public Int32? approved_by { get; set; }
        public DateTime? approved_date { get; set; }
        public DateTime added_to_db_date { get; set; }
        public string location_name { get; set; }
        public string source_id { get; set; }
        public Int32? width { get; set; }
        public Int32? height { get; set; }
        public Boolean is_video { get; set; }
        public string source { get; set; }
        public string tags { get; set; }
    }
}
