using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using overrideSocial;

namespace api_test
{
    class Program
    {
        static void Main(string[] args)
        {
            facebook _facebook = new facebook();
            instagram _instagram = new instagram();
            twitter _twitter = new twitter();
            tags _tags = new tags();
            settings _settings = new settings();

            Int32 fetch_count = _settings.refresh_count();

            Console.WriteLine("######################## BEGINNING UPDATE SEQUENCE ########################");
            Console.WriteLine("");

            Console.WriteLine("Checking for Active Tags & Usernames to Import...");
            Console.WriteLine("");

            foreach (Tag t in _tags.get_active())
            {
                Console.WriteLine("Beginning Evaluation for Term: " + t.value);
                Console.WriteLine("");

                if (t.instagram)
                {
                    Console.WriteLine("###################### INSTAGRAM PULL #########################");
                    Console.WriteLine("");

                    if (t.is_tag)
                    {
                        Console.WriteLine("Importing Records from Instagram for Hashtag #" + t.value);

                        _instagram.fetch(t.value, fetch_count);
                    }
                    else
                    {
                        Console.WriteLine("Importing Records from Instagram for Username @" + t.value);

                        _instagram.fetch(t.value, fetch_count, true);
                    }
                }

                if (t.twitter)
                {
                    Console.WriteLine("");
                    Console.WriteLine("###################### TWITTER PULL #########################");
                    Console.WriteLine("");

                    if (t.is_tag)
                    {
                        Console.WriteLine("Importing Records from Twitter for Hashtag #" + t.value);

                        _twitter.fetch(t.value, fetch_count);
                    }
                    else
                    {
                        Console.WriteLine("Importing Records from Twitter for Username @" + t.value);

                        _twitter.fetch(t.value, fetch_count, true);
                    }
                }
            }

            Console.WriteLine("");
            Console.WriteLine("######################## ENDING UPDATE SEQUENCE ########################");
        }
    }
}
