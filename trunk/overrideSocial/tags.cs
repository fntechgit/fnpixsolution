using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace overrideSocial
{
    public class tags
    {
        FNTech_MediaDataContext db = new FNTech_MediaDataContext();

        public List<Tag> get_active()
        {
            List<Tag> _tags = new List<Tag>();

            var result = from t in db.tags
                where t.entire_event == true || (t.start_time <= DateTime.Now && t.end_time >= DateTime.Now)
                select t;

            foreach (var item in result)
            {
                Tag ta = new Tag();

                ta.facebook = item.facebook;
                ta.id = item.id;
                ta.instagram = item.instagram;
                ta.is_tag = item.is_tag;
                ta.twitter = item.twitter;
                ta.value = item.value;
                ta.event_id = item.event_id;

                _tags.Add(ta);
            }

            return _tags;
        }

        public List<Tag> get_all()
        {
            List<Tag> _tags = new List<Tag>();

            var result = from t in db.tags
                         orderby t.value
                         select t;

            foreach (var item in result)
            {
                Tag ta = new Tag();

                ta.facebook = item.facebook;
                ta.id = item.id;
                ta.instagram = item.instagram;
                ta.is_tag = item.is_tag;
                ta.twitter = item.twitter;
                ta.value = item.value;
                ta.start_time = item.start_time;
                ta.end_time = item.end_time;
                ta.entire_event = item.entire_event;
                ta.event_id = item.event_id;

                _tags.Add(ta);
            }

            return _tags;
        }

        public Tag get_by_id(Int32 id)
        {
            tag t = db.tags.Single(x => x.id == id);

            Tag ta = new Tag();

            ta.end_time = t.end_time;
            ta.entire_event = t.entire_event;
            ta.facebook = t.facebook;
            ta.id = t.id;
            ta.instagram = t.instagram;
            ta.is_tag = t.is_tag;
            ta.start_time = t.start_time;
            ta.twitter = t.twitter;
            ta.value = t.value;
            ta.event_id = t.event_id;

            return ta;
        }

        public List<Tag> select(Int32 event_id, Boolean active)
        {
            List<Tag> _tags = new List<Tag>();

            var result = from t in db.tags
                         where t.event_id==event_id && t.entire_event == true || (t.start_time <= DateTime.Now && t.end_time >= DateTime.Now)
                         select t;

            foreach (var item in result)
            {
                Tag ta = new Tag();

                ta.facebook = item.facebook;
                ta.id = item.id;
                ta.instagram = item.instagram;
                ta.is_tag = item.is_tag;
                ta.twitter = item.twitter;
                ta.value = item.value;
                ta.event_id = item.event_id;

                _tags.Add(ta);
            }

            return _tags;
        }

        public List<Tag> select(Int32 event_id)
        {
            List<Tag> _tags = new List<Tag>();

            var result = from t in db.tags
                         where t.event_id == event_id
                         orderby t.value
                         select t;

            foreach (var item in result)
            {
                Tag ta = new Tag();

                ta.facebook = item.facebook;
                ta.id = item.id;
                ta.instagram = item.instagram;
                ta.is_tag = item.is_tag;
                ta.twitter = item.twitter;
                ta.value = item.value;
                ta.event_id = item.event_id;

                _tags.Add(ta);
            }

            return _tags;
        } 

        public Tag add(Tag t)
        {
            tag ta = new tag();

            ta.end_time = t.end_time;
            ta.entire_event = t.entire_event;
            ta.facebook = t.facebook;
            ta.instagram = t.instagram;
            ta.is_tag = t.is_tag;
            ta.start_time = t.start_time;
            ta.twitter = t.twitter;
            ta.value = t.value;
            ta.event_id = t.event_id;

            db.tags.InsertOnSubmit(ta);

            db.SubmitChanges();

            t.id = ta.id;

            return t;
        }

        public Boolean delete(Int32 id)
        {
            tag t = db.tags.Single(x => x.id == id);

            db.tags.DeleteOnSubmit(t);

            db.SubmitChanges();

            // now we need to delete all media that was created using that tag
            var result = from md in db.medias
                where md.tag_id == id
                select md;

            db.medias.DeleteAllOnSubmit(result);

            db.SubmitChanges();

            return true;
        }

        public Tag update(Tag t)
        {
            tag ta = db.tags.Single(x => x.id == t.id);

            ta.end_time = t.end_time;
            ta.entire_event = t.entire_event;
            ta.facebook = t.facebook;
            ta.instagram = t.instagram;
            ta.is_tag = t.is_tag;
            ta.start_time = t.start_time;
            ta.twitter = t.twitter;
            ta.value = t.value;
            ta.event_id = t.event_id;

            db.SubmitChanges();

            return t;
        }
    }

    public class Tag
    {
        public Int32 id { get; set; }
        public string value { get; set; }
        public Boolean is_tag { get; set; }
        public Boolean entire_event { get; set; }
        public DateTime? start_time { get; set; }
        public DateTime? end_time { get; set; }
        public Boolean facebook { get; set; }
        public Boolean instagram { get; set; }
        public Boolean twitter { get; set; }
        public Int32 event_id { get; set; }
    }
}
