using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using overrideSocial;
using DropNet;

namespace fnpix_dropbox_updater
{
    class Program
    {
        static void Main(string[] args)
        {
            overrideSocial.events _events = new overrideSocial.events();
            overrideSocial.settings _settings = new overrideSocial.settings();
            overrideSocial.dropbox _dropbox = new overrideSocial.dropbox();
            
            Console.WriteLine("##################### BEGIN DROPBOX UPDATE SEQUENCE ########################");
            Console.WriteLine("");
            Console.WriteLine("");

            Console.WriteLine("---- BEGIN LOOPING THROUGH EVENTS THAT ARE DUE AN UPDATE ----");

            Boolean moderate = _settings.dropbox_moderate();

            foreach (Event ev in _events.dropbox_authorized())
            {
                Console.WriteLine("*************** BEGINNING UPDATE FOR " + ev.title.ToUpper() + " ***********************");
                Console.WriteLine("");
                Console.WriteLine("");

                DropNetClient _client = new DropNetClient(_settings.dropbox_api_key(), _settings.dropbox_api_secret(), ev.request_token, ev.access_token);

                var metaDeta = _client.GetMetaData("/Public");

                Console.WriteLine(metaDeta.Contents.Count.ToString() + " Items Found");

                foreach (DropNet.Models.MetaData md in metaDeta.Contents)
                {
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

                    if (_dropbox.video_check(d.extension))
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

                    Dropbox db_test = _dropbox.@select(d.path);

                    if (db_test.id > 0)
                    {
                        d.id = db_test.id;

                        _dropbox.update(d);

                        Console.WriteLine(d.filename + " Updated");
                    }
                    else
                    {
                        _dropbox.insert(d);

                        Console.WriteLine(d.filename + " Inserted");
                    }
                }

                Console.WriteLine("################################## " + ev.title.ToUpper() + " COMPLETED #############################");
                Console.WriteLine("");
                Console.WriteLine("");
            }

            Console.WriteLine("################################# PROCESS COMPLETED ###############################");
        }
    }
}
