using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PodcastApp.Models
{
    public class Episode
    {
        public string Uuid { get; set; }

        public string Url { get; set; }

        public string Title { get; set; }

        public string Published { get; set; }

        public int Duration { get; set; }

        public string FileType { get; set; }

        public string Size { get; set; }

        public int PlayedUpTo { get; set; }

        public bool Starred { get; set; }

        public string PodcastUuid { get; set; }

        public string PodcastTitle { get; set; }
    }
}
