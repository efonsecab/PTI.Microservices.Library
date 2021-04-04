
using System;

namespace BlazorSample.Shared.Twitter
{
    public class Tweet
    {
        public DateTime CreatedAt { get; set; }
        public string Text { get; set; }
        public MediaInfo[] Media { get; set; }
        public UrlInfo[] Urls { get; set; }
        public ulong Id { get; set; }
    }

    public class MediaInfo
    {
        public string DisplayUrl { get; set; }
        public string ExpandedUrl { get; set; }
        public string MediaUrl { get; set; }
        public string MediaUrlHttps { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
    }
}
