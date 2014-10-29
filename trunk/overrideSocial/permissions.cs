using System;
using System.Collections.Generic;
using System.Linq;

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

        public Permission add(Permission p)
        {
            permission pe = new permission();

            pe.assigned_by = p.assigned_by;
            pe.assigned_date = DateTime.Now;
            pe.event_id = p.event_id;
            pe.permission_id = p.permission_id;
            pe.user_id = p.user_id;
            
            db.permissions.InsertOnSubmit(pe);

            db.SubmitChanges();

            p.id = pe.id;

            return p;
        }

        public Permission update(Permission p)
        {
            permission pe = db.permissions.Single(x => x.id == p.id);

            pe.event_id = p.event_id;
            pe.permission_id = p.permission_id;
            pe.assigned_by = p.assigned_by;
            pe.assigned_date = DateTime.Now;

            db.SubmitChanges();

            return p;
        }

        public Permission select(Int32 id)
        {
            permission pe = db.permissions.Single(x => x.id == id);

            Permission p = new Permission();

            p.event_id = pe.event_id;
            p.permission_id = pe.permission_id;
            p.id = pe.id;

            return p;
        }

        public Boolean delete(Int32 id)
        {
            permission p = db.permissions.Single(x => x.id == id);

            db.permissions.DeleteOnSubmit(p);

            db.SubmitChanges();

            return true;
        }

        /// <summary>
        /// Gets a List of Permissions for the User
        /// </summary>
        /// <param name="user_id">The user_id.</param>
        /// <returns>List of Permission Models</returns>
        public List<Permission> get_by_user(Int32 user_id)
        {
            List<Permission> _permissions = new List<Permission>();

            foreach (var item in db.permissions_get_by_user(user_id))
            {
                Permission p = new Permission();

                p.assigned_by = item.assigned_by;
                p.assigned_by_name = item.full_name;
                p.assigned_date = item.assigned_date;
                p.event_date = item.start_date;
                p.event_id = item.event_id;
                p.event_name = item.title;
                p.id = item.id;
                p.permission_id = item.permission_id;
                p.security_level = item.security_level;
                p.user_id = item.user_id;
                
                _permissions.Add(p);
            }

            return _permissions;
        } 
    }

    public class Permission
    {
        public Int32 id { get; set; }
        public Int32 event_id { get; set; }
        public string event_name { get; set; }
        public DateTime event_date { get; set; }
        public Int32 user_id { get; set; }
        public Int32 permission_id { get; set; }
        public string security_level { get; set; }
        public DateTime assigned_date { get; set; }
        public Int32 assigned_by { get; set; }
        public string assigned_by_name { get; set; }
    }
}
