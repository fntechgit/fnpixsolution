using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

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

                _tags.Add(ta);
            }

            return _tags;
        } 
    }

    public class Tag
    {
        public Int32 id { get; set; }
        public string value { get; set; }
        public Boolean is_tag { get; set; }
        public Boolean entire_event { get; set; }
        public DateTime start_time { get; set; }
        public DateTime end_time { get; set; }
        public Boolean facebook { get; set; }
        public Boolean instagram { get; set; }
        public Boolean twitter { get; set; }
    }
}
