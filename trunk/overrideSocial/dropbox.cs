using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DropNet;

namespace overrideSocial
{
    public class dropbox
    {
        private FNTech_MediaDataContext db = new FNTech_MediaDataContext();
        private settings _settings = new settings();
        private events _events = new events();

        /// <summary>
        /// Inserts a Dropbox Record
        /// </summary>
        /// <param name="d">The dropbox model</param>
        /// <returns>a completed one with an ID</returns>
        public Dropbox insert(Dropbox d)
        {
            dropbox_media dm = new dropbox_media();

            dm.approved = d.approved;
            dm.approved_by = d.approved_by;
            dm.approved_date = d.approved_date;
            dm.create_date = DateTime.Now;
            dm.email = d.email;
            dm.event_id = d.event_id;
            dm.extension = d.extension;
            dm.filename = d.filename;
            dm.is_video = d.is_video;
            dm.modified_date = d.modified_date;
            dm.path = d.path;
            dm.size = d.size;
            dm.stream = d.stream;
            dm.thumbnail = d.thumbnail;
            dm.uid = d.uid;
            dm.username = d.username;
            
            db.dropbox_medias.InsertOnSubmit(dm);

            db.SubmitChanges();

            d.id = dm.id;

            return d;
        }

        public Dropbox update(Dropbox d)
        {
            dropbox_media dm = db.dropbox_medias.Single(x => x.id == d.id);

            dm.approved = d.approved;
            dm.approved_by = d.approved_by;
            dm.approved_date = d.approved_date;
            dm.email = d.email;
            dm.event_id = d.event_id;
            dm.extension = d.extension;
            dm.filename = d.filename;
            dm.is_video = d.is_video;
            dm.modified_date = d.modified_date;
            dm.path = d.path;
            dm.size = d.size;
            dm.stream = d.stream;
            dm.thumbnail = d.thumbnail;
            dm.uid = d.uid;
            dm.username = d.username;

            db.SubmitChanges();

            return d;
        }

        public Dropbox select(Int32 id)
        {
            Dropbox dm = new Dropbox();

            dropbox_media d = db.dropbox_medias.Single(x => x.id == id);

            dm.approved = d.approved;
            dm.approved_by = d.approved_by;
            dm.approved_date = d.approved_date;
            dm.email = d.email;
            dm.event_id = d.event_id;
            dm.extension = d.extension;
            dm.filename = d.filename;
            dm.is_video = d.is_video;
            dm.modified_date = d.modified_date;
            dm.path = d.path;
            dm.size = d.size;
            dm.stream = d.stream;
            dm.thumbnail = d.thumbnail;
            dm.uid = d.uid;
            dm.username = d.username;
            dm.id = d.id;

            return dm;
        }

        public Dropbox select(string path)
        {
            Dropbox dm = new Dropbox();

            var result = from dbx in db.dropbox_medias
                where dbx.path == path
                select dbx;

            foreach (var d in result)
            {
                dm.approved = d.approved;
                dm.approved_by = d.approved_by;
                dm.approved_date = d.approved_date;
                dm.email = d.email;
                dm.event_id = d.event_id;
                dm.extension = d.extension;
                dm.filename = d.filename;
                dm.is_video = d.is_video;
                dm.modified_date = d.modified_date;
                dm.path = d.path;
                dm.size = d.size;
                dm.stream = d.stream;
                dm.thumbnail = d.thumbnail;
                dm.uid = d.uid;
                dm.username = d.username;
                dm.id = d.id;
            }

            return dm;
        }

        public List<Dropbox> select_list(Int32 event_id)
        {
            List<Dropbox> _dropboxes = new List<Dropbox>();

            var result = from dropboxes in db.dropbox_medias
                where dropboxes.event_id == event_id
                select dropboxes;

            foreach (var d in result)
            {
                Dropbox dm = new Dropbox();

                dm.approved = d.approved;
                dm.approved_by = d.approved_by;
                dm.approved_date = d.approved_date;
                dm.email = d.email;
                dm.event_id = d.event_id;
                dm.extension = d.extension;
                dm.filename = d.filename;
                dm.is_video = d.is_video;
                dm.modified_date = d.modified_date;
                dm.path = d.path;
                dm.size = d.size;
                dm.stream = d.stream;
                dm.thumbnail = d.thumbnail;
                dm.uid = d.uid;
                dm.username = d.username;
                dm.id = d.id;

                _dropboxes.Add(dm);
            }

            return _dropboxes;
        }

        public List<Dropbox> select_list(Int32 event_id, Boolean approved)
        {
            return select_list(event_id).Where(x => x.approved == approved).ToList();
        }

        public Boolean approve(Int32 id, Int32 approver_id)
        {
            Dropbox d = select(id);

            d.approved = true;
            d.approved_by = approver_id;
            d.approved_date = DateTime.Now;

            update(d);

            return true;
        }

        public Boolean unapprove(Int32 id)
        {
            Dropbox d = select(id);

            d.approved = false;
            d.approved_by = null;
            d.approved_date = null;

            update(d);

            return true;
        }

        public Boolean video_check(string extension)
        {
            Boolean is_video = false;

            switch (extension)
            {
                case ".mp4":
                    is_video = true;
                    break;
                case ".mov":
                    is_video = true;
                    break;
                case ".flv":
                    is_video = true;
                    break;
                case ".ogv":
                    is_video = true;
                    break;
                case ".m4v":
                    is_video = true;
                    break;
                case ".f4v":
                    is_video = true;
                    break;
                case ".wmv":
                    is_video = true;
                    break;
                case ".webm":
                    is_video = true;
                    break;
            }

            return is_video;
        }
    }

    public class Dropbox
    {
        public Int32 id { get; set; }
        public string extension { get; set; }
        public string filename { get; set; }
        public string path { get; set; }
        public string size { get; set; }
        public string uid { get; set; }
        public string thumbnail { get; set; }
        public string stream { get; set; }
        public DateTime? modified_date { get; set; }
        public Boolean approved { get; set; }
        public DateTime create_date { get; set; }
        public Int32? approved_by { get; set; }
        public DateTime? approved_date { get; set; }
        public Boolean is_video { get; set; }
        public Int32 event_id { get; set; }
        public string username { get; set; }
        public string email { get; set; }
    }
}
