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
                    if (md.Is_Dir)
                    {
                        foreach (DropNet.Models.MetaData md2 in _client.GetMetaData(md.Path).Contents)
                        {
                            if (!md2.Is_Dir)
                            {
                                _dropbox.log_file(md2, ev);
                            }
                        }
                    }
                    else
                    {
                        _dropbox.log_file(md, ev);
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
