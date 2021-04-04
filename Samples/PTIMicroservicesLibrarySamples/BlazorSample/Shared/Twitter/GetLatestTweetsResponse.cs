using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSample.Shared.Twitter
{
    public class GetLatestTweetsResponse
    {
        public Tweet[] Tweets { get; set; }
        public string TweetsAudioBase64 { get; set; }
    }
}
