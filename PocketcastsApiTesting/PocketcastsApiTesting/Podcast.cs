using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PodcastApp.Models
{
    public class Podcast
    {
        public string Uuid { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public DateTime LastEpisodePublished { get; set; }

        public bool Unplayed { get; set; }

        public string LastEpisodeUuid { get; set; }
    }
}
