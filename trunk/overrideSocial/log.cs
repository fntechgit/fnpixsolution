using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace overrideSocial
{
    public class logger
    {
        private FNTech_MediaDataContext db = new FNTech_MediaDataContext();

        public Log add(Log l)
        {
            log lo = new log();

            lo.description = l.description;
            lo.event_id = l.event_id;
            lo.logdate = DateTime.Now;
            lo.title = l.title;
            lo.user_id = l.user_id;
            
            db.logs.InsertOnSubmit(lo);

            db.SubmitChanges();

            l.id = lo.id;

            return l;
        }

        public List<Log> select(Int32 event_id)
        {
            List<Log> _logs = new List<Log>();

            var result = from l in db.logs
                where l.event_id == event_id
                orderby l.logdate
                select l;

            foreach (var item in result)
            {
                Log lo = new Log();

                lo.description = item.description;
                lo.event_id = event_id;
                lo.id = item.id;
                lo.logdate = item.logdate;
                lo.title = item.title;
                lo.user_id = item.user_id;
                
                _logs.Add(lo);
            }

            return _logs;
        } 
    }

    public class Log
    {
        public Int32 id { get; set; }
        public Int32 event_id { get; set; }
        public Int32 user_id { get; set; }
        public DateTime logdate { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string username { get; set; }
        public string eventname { get; set; }
    }
}
