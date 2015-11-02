using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using overrideSocial;
using DropNet;
using ImageMagick;

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

                string optimized = "";

                DropNetClient _client = new DropNetClient(_settings.dropbox_api_key(), _settings.dropbox_api_secret(), ev.request_token, ev.access_token);

                var metaDeta = _client.GetMetaData("/Public");

                Console.WriteLine(metaDeta.Contents.Count.ToString() + " Items Found");

                foreach (DropNet.Models.MetaData md in metaDeta.Contents)
                {
                    // download the file
                    string path = @"C:\sites\fnpix.fntech.com\uploads\";

                    WebRequest requestPic = WebRequest.Create(_dropbox.source(md, ev));

                    WebResponse responsePic = requestPic.GetResponse();

                    //using (MagickImage img = new MagickImage(responsePic.GetResponseStream()))
                    //{
                    //    MagickGeometry size = new MagickGeometry(960, 960);

                    //    size.IgnoreAspectRatio = false;

                    //    img.Resize(size);

                    //    img.Write(path + md.Name);

                    //    optimized = path + md.Name;

                    //    // now store the data with the record
                    //}

                    if (md.Is_Dir)
                    {
                        foreach (DropNet.Models.MetaData md2 in _client.GetMetaData(md.Path).Contents)
                        {
                            if (!md2.Is_Dir)
                            {
                                _dropbox.log_file(md2, ev, optimized);
                            }
                        }
                    }
                    else
                    {
                        _dropbox.log_file(md, ev, optimized);
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
