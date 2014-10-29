using System;
using System.Collections.Generic;
using System.Linq;

namespace overrideSocial
{
    public class events
    {
        #region declarations

        private FNTech_MediaDataContext db = new FNTech_MediaDataContext();
        private logger _logger = new logger();

        #endregion

        #region CRUD

        /// <summary>
        /// Adds the specified Event to the database
        /// </summary>
        /// <param name="e">Event Model</param>
        /// <returns>completed Event Model including Event ID Number</returns>
        public Event add(Event e)
        {
            event_master ev = new event_master();

            ev.active = true;
            ev.address = e.address;
            ev.address2 = e.address2;
            ev.city = e.city;
            ev.client = e.client;
            ev.country = e.country;
            ev.description = e.description;
            ev.end_date = e.end;
            ev.interval = e.interval;
            ev.latitude = e.latitude;
            ev.longitude = e.longitude;
            ev.moderate = e.moderate;
            ev.start_date = e.start;
            ev.state = e.state;
            ev.title = e.title;
            ev.zip = e.zip;
            ev.create_date = DateTime.Now;
            ev.created_by = e.created_by;
            ev.last_update = ev.create_date.AddMinutes(e.interval*-1);
            ev.request_token = null;
            ev.access_token = null;

            db.event_masters.InsertOnSubmit(ev);

            db.SubmitChanges();

            e.id = ev.id;

            Log l = new Log();

            l.description = "New Event has been created by " + e.created_by;
            l.event_id = e.id;
            l.eventname = e.title;
            l.logdate = DateTime.Now;
            l.title = "Event Created";
            l.user_id = e.created_by_int;
            l.username = e.created_by;

            _logger.add(l);

            return e;
        }

        /// <summary>
        /// Updates the Event
        /// </summary>
        /// <param name="e">The Event Model</param>
        /// <returns>Updated Event</returns>
        public Event update(Event e)
        {
            event_master ev = db.event_masters.Single(x => x.id == e.id);

            ev.active = e.active;
            ev.address = e.address;
            ev.address2 = e.address2;
            ev.city = e.city;
            ev.client = e.client;
            ev.country = e.country;
            ev.description = e.description;
            ev.end_date = e.end;
            ev.interval = e.interval;
            ev.latitude = e.latitude;
            ev.longitude = e.longitude;
            ev.moderate = e.moderate;
            ev.start_date = e.start;
            ev.state = e.state;
            ev.title = e.title;
            ev.zip = e.zip;
            ev.last_update = e.last_update;

            db.SubmitChanges();

            return e;
        }

        /// <summary>
        /// Deletes the Event
        /// </summary>
        /// <param name="id">The Event ID</param>
        /// <returns>true once deleted</returns>
        public Boolean delete(Int32 id)
        {
            event_master ev = db.event_masters.Single(x => x.id == id);

            db.event_masters.DeleteOnSubmit(ev);

            db.SubmitChanges();

            return true;
        }

        /// <summary>
        /// Gets the Event by ID record
        /// </summary>
        /// <param name="id">ID of the record</param>
        /// <returns>Event Model</returns>
        public Event select(Int32 id)
        {
            Event e = new Event();

            event_master ev = db.event_masters.Single(x => x.id == id);

            e.active = ev.active;
            e.address = ev.address;
            e.address2 = ev.address2;
            e.city = ev.city;
            e.client = ev.client;
            e.country = ev.country;
            e.created_by = ev.created_by;
            e.created_date = ev.create_date;
            e.description = ev.description;
            e.end = ev.end_date;
            e.id = id;
            e.interval = ev.interval;
            e.latitude = ev.latitude;
            e.longitude = ev.longitude;
            e.moderate = ev.moderate;
            e.start = ev.start_date;
            e.state = ev.state;
            e.title = ev.title;
            e.zip = ev.zip;
            e.last_update = ev.last_update;

            return e;
        }

        /// <summary>
        /// Selects All Events in the Database
        /// </summary>
        /// <returns>Enumerable List of Event Models</returns>
        public List<Event> select_list()
        {
            List<Event> _events = new List<Event>();

            var result = from eve in db.event_masters
                orderby eve.title
                select eve;

            foreach (var ev in result)
            {
                Event e = new Event();

                e.active = ev.active;
                e.address = ev.address;
                e.address2 = ev.address2;
                e.city = ev.city;
                e.client = ev.client;
                e.country = ev.country;
                e.created_by = ev.created_by;
                e.created_date = ev.create_date;
                e.description = ev.description;
                e.end = ev.end_date;
                e.id = ev.id;
                e.interval = ev.interval;
                e.latitude = ev.latitude;
                e.longitude = ev.longitude;
                e.moderate = ev.moderate;
                e.start = ev.start_date;
                e.state = ev.state;
                e.title = ev.title;
                e.zip = ev.zip;
                e.last_update = ev.last_update;

                _events.Add(e);
            }

            return _events;
        }

        /// <summary>
        /// Gets All Events that are either Active or Inactive based on the users selection
        /// </summary>
        /// <param name="active">if set to <c>true</c> [active].</param>
        /// <returns>List of Event Models</returns>
        public List<Event> select_list(Boolean active)
        {
            return select_list().Where(x => x.active == active).ToList();
        }

        /// <summary>
        /// Get All Events within parameters
        /// </summary>
        /// <param name="active">if set to <c>true</c> [active].</param>
        /// <param name="start">The start date</param>
        /// <param name="end">The end date</param>
        /// <returns>List of Event Models</returns>
        public List<Event> select_list(Boolean active, DateTime start, DateTime end)
        {
            return select_list(active).Where(x => x.start <= start).Where(x => x.end <= end).ToList();
        }

        public List<Event> ready_for_pull()
        {
            return select_list(true).Where(x => x.last_update.AddMinutes(x.interval) <= DateTime.Now).ToList();
        }

        #endregion
    }

    #region Models

    public class Event
    {
        public Int32 id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public string address { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string country { get; set; }
        public Decimal? latitude { get; set; }
        public Decimal? longitude { get; set; }
        public Boolean active { get; set; }
        public string client { get; set; }
        public Boolean moderate { get; set; }
        public Int32 interval { get; set; }
        public string created_by { get; set; }
        public Int32 created_by_int { get; set; }
        public DateTime created_date { get; set; }
        public DateTime last_update { get; set; }
        public string request_token { get; set; }
        public string access_token { get; set; }
    }

    #endregion
}
