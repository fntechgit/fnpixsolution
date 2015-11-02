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

        public string source(DropNet.Models.MetaData md, Event ev)
        {
            DropNetClient _client = new DropNetClient(_settings.dropbox_api_key(), _settings.dropbox_api_secret(), ev.request_token, ev.access_token);

            return _client.GetMedia(md.Path).Url;
        }

        public Boolean log_file(DropNet.Models.MetaData md, Event ev, string optimized)
        {
            DropNetClient _client = new DropNetClient(_settings.dropbox_api_key(), _settings.dropbox_api_secret(), ev.request_token, ev.access_token);

            Console.WriteLine("Extension: " + md.Extension);
            Console.WriteLine("Name: " + md.Name);
            Console.WriteLine("Path: " + md.Path);
            Console.WriteLine("Root: " + md.Root);
            Console.WriteLine("Size: " + md.Size);
            Console.WriteLine("Hash: " + md.Hash);
            Console.WriteLine("Icon: " + md.Icon);
            Console.WriteLine("REV: " + md.Rev);
            Console.WriteLine("REVISION: " + md.Revision.ToString());
            Console.WriteLine("Modified: " + md.Modified);
            Console.WriteLine("Modified Date: " + md.ModifiedDate.ToShortDateString());

            var mediaLink = _client.GetMedia(md.Path);

            Console.WriteLine("Media Link: " + mediaLink.Url);
            Console.WriteLine("Expires: " + mediaLink.Expires);

            Console.WriteLine("");

            Dropbox d = new Dropbox();

            Boolean moderate = _settings.dropbox_moderate();

            if (!moderate)
            {
                d.approved = true;
                d.approved_by = 1;
                d.approved_date = DateTime.Now;
            }

            d.create_date = DateTime.Now;
            d.email = ev.dropbox_email;
            d.event_id = ev.id;
            d.extension = md.Extension;
            d.filename = md.Name;

            if (video_check(d.extension))
            {
                d.is_video = true;
            }
            else
            {
                d.is_video = false;
            }

            d.modified_date = md.ModifiedDate;
            d.path = md.Path;
            d.size = md.Size;
            d.stream = mediaLink.Url;
            d.uid = ev.dropbox_uid.ToString();
            d.username = ev.dropbox_username;
            d.optimized = optimized;

            Dropbox db_test = select(d.path);

            if (db_test.id > 0)
            {
                d.id = db_test.id;

                d.approved = db_test.approved;
                d.approved_by = db_test.approved_by;
                d.approved_date = db_test.approved_date;

                update(d);

                Console.WriteLine(d.filename + " Updated");
            }
            else
            {
                insert(d);

                Console.WriteLine(d.filename + " Inserted");
            }

            return true;
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
        public string optimized { get; set; }
    }
}
