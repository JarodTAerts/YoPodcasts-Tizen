using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PodcastApp.Models
{
    public static class Properties
    {
        public static List<Podcast> SubscribedPodcasts { get; set; }

        public static List<Episode> Queue { get; set; }
    }
}
