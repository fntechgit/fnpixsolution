using System;
using TweetSharp;

namespace overrideSocial
{
    public class twitter
    {
        settings _settings = new settings();
        mediaManager _media = new mediaManager();

        // get by twitter hashtag
        public Int32 fetch(string tag, Int32 count)
        {
            Int32 total = 0;

            var service = new TwitterService(_settings.twitter_api_key(), _settings.twitter_api_secret());
            service.AuthenticateWith(_settings.twitter_access_token(), _settings.twitter_access_token_secret());

            var options = new SearchOptions();

            options.Q = "%23" + tag;
            options.Count = count;
            options.IncludeEntities = true;
            options.Lang = "en";
            options.Resulttype = TwitterSearchResultType.Recent;

            TwitterSearchResult tweets = new TwitterSearchResult();

            try
            {
                tweets = service.Search(options);
            }
            catch (OverflowException ex)
            {
                Console.WriteLine(ex.InnerException);
            }

            foreach (var item in tweets.Statuses)
                {
                    Media m = new Media();

                    m.added_to_db_date = DateTime.Now;
                    m.createdate = item.CreatedDate;
                    m.description = item.Text;
                    m.full_name = item.User.Name;
                    //m.source_id = item.Id.ToString();

                    if (item.Location != null)
                    {
                        m.latitude = item.Location.Coordinates.Latitude.ToString();
                        m.longitude = item.Location.Coordinates.Longitude.ToString();
                    }

                    if (item.Place != null)
                    {
                        m.location_name = item.Place.FullName;
                    }

                    m.likes = item.RetweetCount;
                    m.link = "https://twitter.com/" + item.User.ScreenName + "/status/" + item.Id.ToString();
                    m.profilepic = item.User.ProfileImageUrl;
                    m.service = "Twitter";

                    foreach (var hashtag in item.Entities.HashTags)
                    {
                        m.tags += "#" + hashtag.Text + " ";
                    }

                    m.username = item.User.ScreenName;

                    // ################## NOW THE IMAGE STUFF 
                    foreach (TwitterMedia photo in item.Entities.Media)
                    {
                        m.source_id = photo.IdAsString;
                        m.source = photo.MediaUrl + ":large";
                        m.width = photo.Sizes.Large.Width;
                        m.height = photo.Sizes.Large.Height;

                        if (photo.MediaType.ToString() != "Photo")
                        {
                            m.is_video = true;
                        }

                        _media.add(m);

                        total++;
                    }
            }

            return total;
        }

        // get by twitter username
        public Int32 fetch(string tag, Int32 count, Boolean is_username)
        {
            Int32 total = 0;

            var service = new TwitterService(_settings.twitter_api_key(), _settings.twitter_api_secret());
            service.AuthenticateWith(_settings.twitter_access_token(), _settings.twitter_access_token_secret());

            var options = new ListTweetsOnUserTimelineOptions();

            options.ScreenName = tag;
            options.Count = count;

            var tweets = service.ListTweetsOnUserTimeline(options);

            foreach (TwitterStatus item in tweets)
            {
                Media m = new Media();

                m.added_to_db_date = DateTime.Now;
                m.createdate = item.CreatedDate;
                m.description = item.Text;

                if (item.User != null)
                {
                    m.full_name = item.User.Name;
                    m.link = "https://twitter.com/" + item.User.ScreenName + "/status/" + item.Id.ToString();
                    m.profilepic = item.User.ProfileImageUrl;
                }

                if (item.Location != null)
                {
                    m.latitude = item.Location.Coordinates.Latitude.ToString();
                    m.longitude = item.Location.Coordinates.Longitude.ToString();
                }

                if (item.Place != null)
                {
                    m.location_name = item.Place.FullName;
                }

                m.likes = item.RetweetCount;

                m.service = "Twitter";

                foreach (var hashtag in item.Entities.HashTags)
                {
                    m.tags += "#" + hashtag.Text + " ";
                }

                m.username = tag;

                // ################## NOW THE IMAGE STUFF 
                foreach (TwitterMedia photo in item.Entities.Media)
                {
                    m.source_id = photo.IdAsString;
                    m.source = photo.MediaUrl + ":large";
                    m.width = photo.Sizes.Large.Width;
                    m.height = photo.Sizes.Large.Height;

                    if (photo.MediaType.ToString() != "Photo")
                    {
                        m.is_video = true;
                    }

                    _media.add(m);

                    total++;
                }
            }

            return total;
        }


    }
}
