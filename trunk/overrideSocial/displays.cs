using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace overrideSocial
{
    public class displays
    {
        private FNTech_MediaDataContext db = new FNTech_MediaDataContext();

        public List<Template> get_templates()
        {
            List<Template> _templates = new List<Template>();

            var result = from tmp in db.templates
                where tmp.active == true
                orderby tmp.title
                select tmp;

            foreach (var item in result)
            {
                Template t = new Template();

                t.active = item.active;
                t.description = item.description;
                t.id = item.id;
                t.title = item.title;
                t.video = item.video;

                _templates.Add(t);
            }

            return _templates;
        }

        public Display insert(Display d)
        {
            display di = new display();

            di.enddate = d.enddate;
            di.event_id = d.event_id;
            di.randomize = false;
            di.slide_duration = d.slide_duration;
            di.startdate = d.startdate;
            di.view_id = d.view_id;

            db.displays.InsertOnSubmit(di);

            db.SubmitChanges();

            d.id = di.id;

            return d;
        }

        public Display update(Display d)
        {
            display di = db.displays.Single(x => x.id == d.id);

            di.enddate = d.enddate;
            di.event_id = d.event_id;
            di.randomize = false;
            di.slide_duration = d.slide_duration;
            di.startdate = d.startdate;
            di.view_id = d.view_id;

            db.SubmitChanges();

            return d;
        }

        public Boolean delete(Int32 id)
        {
            var result = from dis in db.displays
                where dis.id == id
                select dis;

            db.displays.DeleteAllOnSubmit(result);

            db.SubmitChanges();

            return true;
        }

        public List<Display> select(Int32 event_id)
        {
            List<Display> _displays = new List<Display>();

            foreach (var item in db.displays_get_by_event(event_id))
            {
                Display d = new Display();

                d.enddate = item.enddate;
                d.event_id = item.event_id;
                d.id = item.id;
                d.randomize = false;
                d.slide_duration = item.slide_duration;
                d.startdate = item.startdate;
                d.view_id = item.view_id;
                d.view_name = item.title;

                _displays.Add(d);
            }

            return _displays;
        }

        public List<Display> select(Int32 event_id, DateTime whatTime)
        {
            return select(event_id).Where(x => x.startdate <= whatTime).Where(x => x.enddate <= whatTime).ToList();
        }

        public Display single(Int32 id)
        {
            Display d = new Display();

            display di = db.displays.Single(x => x.id == id);

            d.enddate = di.enddate;
            d.event_id = di.event_id;
            d.id = di.id;
            d.randomize = false;
            d.slide_duration = di.slide_duration;
            d.startdate = di.startdate;
            d.view_id = di.view_id;

            return d;
        }

        public Display find_my_display(Int32 event_id)
        {
            Random rnd = new Random();

            var records = @select(event_id, DateTime.Now);

            Display myview = new Display();

            if (records.Count > 1)
            {
                int toSkip = rnd.Next(0, records.Count);

                myview = records.Skip(toSkip).Take(1).First();
            }
            else
            {
                foreach (Display d in records)
                {
                    myview = d;
                }
            }

            return myview;
        }
    }

    public class Display
    {
        public Int32 id { get; set; }
        public Int32 view_id { get; set; }
        public Boolean randomize { get; set; }
        public DateTime? startdate { get; set; }
        public DateTime? enddate { get; set; }
        public Int32 slide_duration { get; set; }
        public Int32 event_id { get; set; }
        public string view_name { get; set; }
    }

    public class Template
    {
        public Int32 id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public Boolean active { get; set; }
        public string video { get; set; }
    }
}
