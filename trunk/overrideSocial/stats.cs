using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace overrideSocial
{
    public class stats
    {
        private FNTech_MediaDataContext db = new FNTech_MediaDataContext();

        public Statistic add(Statistic s)
        {
            stat st = new stat();

            st.facebook = s.facebook;
            st.id = s.id;
            st.instagram = s.instagram;
            st.pulldate = s.pulldate;
            st.total = s.total;
            st.twitter = s.twitter;
            
            db.stats.InsertOnSubmit(st);

            db.SubmitChanges();

            s.id = st.id;

            return s;
        }

        public List<Statistic> get_all()
        {
            List<Statistic> _stats = new List<Statistic>();

            var result = from sts in db.stats
                select sts;

            foreach (var item in result)
            {
                Statistic s = new Statistic();

                s.facebook = item.facebook;
                s.id = item.id;
                s.instagram = item.instagram;
                s.pulldate = item.pulldate;
                s.total = item.total;
                s.twitter = item.twitter;

                _stats.Add(s);
            }

            return _stats;
        }
    }

    public class Statistic
    {
        public Int32 id { get; set; }
        public DateTime pulldate { get; set; }
        public Int32 instagram { get; set; }
        public Int32 twitter { get; set; }
        public Int32 facebook { get; set; }
        public Int32 total { get; set; }
    }
}
