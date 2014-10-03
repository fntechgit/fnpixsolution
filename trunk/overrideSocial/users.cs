using System;
using System.Collections.Generic;
using System.Linq;

namespace overrideSocial
{
    public class users
    {
        private FNTech_MediaDataContext db = new FNTech_MediaDataContext();

        public Boolean delete(Int32 id)
        {
            user u = db.users.Single(x => x.id == id);

            db.users.DeleteOnSubmit(u);

            db.SubmitChanges();

            return true;
        }

        public User add(User u)
        {
            user us = new user();

            us.active = u.active;
            us.company = u.company;
            us.created = DateTime.Now;
            us.email = u.email;
            us.first_name = u.first_name;
            us.last_name = u.last_name;
            us.notify_every_minutes = u.notify_every_minutes;
            us.pic = u.picture;
            us.pwd = u.password;
            
            db.users.InsertOnSubmit(us);

            db.SubmitChanges();

            u.id = us.id;

            return u;
        }

        public List<User> get_all()
        {
            List<User> _users = new List<User>();

            var result = from us in db.users
                orderby us.first_name, us.last_name
                select us;

            foreach (var item in result)
            {
                User u = new User();

                u.active = item.active;
                u.company = item.company;
                u.created = item.created;
                u.email = item.email;
                u.first_name = item.first_name;
                u.id = item.id;
                u.last_name = item.last_name;
                u.notify_every_minutes = item.notify_every_minutes;
                u.password = item.pwd;
                u.picture = item.pic;
                
                _users.Add(u);
            }

            return _users;
        }

        public User get_by_id(Int32 id)
        {
            user us = db.users.Single(x => x.id == id);

            User u = new User();

            u.active = us.active;
            u.company = us.company;
            u.created = us.created;
            u.email = us.email;
            u.first_name = us.first_name;
            u.id = us.id;
            u.last_name = us.last_name;
            u.notify_every_minutes = us.notify_every_minutes;
            u.password = us.pwd;
            u.picture = us.pic;

            return u;
        }

        public User update(User us)
        {
            user u = db.users.Single(x => x.id == us.id);

            u.active = us.active;
            u.company = us.company;
            u.created = us.created;
            u.email = us.email;
            u.first_name = us.first_name;
            u.id = us.id;
            u.last_name = us.last_name;
            u.notify_every_minutes = us.notify_every_minutes;
            u.pwd = us.password;
            u.pic = us.picture;

            db.SubmitChanges();

            return us;
        }

        /// <summary>
        /// Logs the User In if the email & Password match
        /// </summary>
        /// <param name="email">The email.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public User login(string email, string password)
        {
            User u = new User();

            u.id = 0;

            var result = from us in db.users
                where us.email == email && us.pwd == password
                select us;

            foreach (var item in result)
            {
                u.active = item.active;
                u.company = item.company;
                u.created = item.created;
                u.email = item.email;
                u.first_name = item.first_name;
                u.id = item.id;
                u.last_name = item.last_name;
                u.notify_every_minutes = item.notify_every_minutes;
                u.password = item.pwd;
                u.picture = item.pic;
            }

            return u;
        }
    }

    public class User
    {
        public Int32 id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string company { get; set; }
        public string password { get; set; }
        public string picture { get; set; }
        public DateTime created { get; set; }
        public Boolean active { get; set; }
        public Int32 notify_every_minutes { get; set; }
    }
}
