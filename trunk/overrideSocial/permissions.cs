using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace overrideSocial
{
    public class permissions
    {
        private FNTech_MediaDataContext db = new FNTech_MediaDataContext();
        private events _events = new events();
        private users _users = new users();

        public List<Event> select_permitted_events(Int32 user_id)
        {
            User u = _users.get_by_id(user_id);

            List<Event> ev = new List<Event>();

            if (u.security > 1001)
            {
                ev = _events.select_list();
            }
            else
            {
                // determine what events the user has permissions for
                foreach (var item in db.events_get_authorized(user_id))
                {
                    Event eve = new Event();

                    eve.active = true;
                    eve.address = item.address;
                    eve.address2 = item.address2;
                    eve.city = item.city;
                    eve.client = item.client;
                    eve.country = item.country;
                    eve.created_by = item.created_by;
                    eve.created_date = item.create_date;
                    eve.description = item.description;
                    eve.end = item.end_date;
                    eve.id = item.id;
                    eve.interval = item.interval;
                    eve.latitude = item.latitude;
                    eve.longitude = item.longitude;
                    eve.moderate = item.moderate;
                    eve.start = item.start_date;
                    eve.state = item.state;
                    eve.title = item.title;
                    eve.zip = item.zip;

                    ev.Add(eve);
                }
            }

            return ev;
        } 
    }

    public class Permission
    {
        public Int32 id { get; set; }
        public Int32 event_id { get; set; }
        public Int32 user_id { get; set; }
        public Int32 permission_id { get; set; }
        public DateTime assigned_date { get; set; }
        public Int32 assigned_by { get; set; }
    }
}
