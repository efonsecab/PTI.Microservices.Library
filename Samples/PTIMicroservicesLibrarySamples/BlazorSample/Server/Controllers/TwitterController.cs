using BlazorSample.Shared.Twitter;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PTI.Microservices.Library.Services;
using PTI.Microservices.Library.Services.Specialized;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorSample.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TwitterController : ControllerBase
    {
        private TwitterService TwitterService { get; }
        private AudibleTwitterService AudibleTwitterService { get; }

        public TwitterController(TwitterService twitterService,
            AudibleTwitterService audibleTwitterService)
        {
            this.TwitterService = twitterService;
            this.AudibleTwitterService = audibleTwitterService;
        }

        [HttpGet("[action]")]
        public async Task<GetLatestTweetsResponse> GetLatestTweets(string username)
        {
            try
            {
                var response = await this.TwitterService.GetTweetsByUsernameAsync(username, excludeReplies: true,
                    includeRetweets: true,
                    maxTweets: 10);
                MemoryStream memoryStream = new MemoryStream();
                await this.AudibleTwitterService.GenerateAudioFromTweets(memoryStream, response);
                var result = response
                 .OrderByDescending(p => p.StatusID)
                 .Select(p => new Tweet()
                 {
                     Id = p.StatusID,
                     CreatedAt = p.CreatedAt,
                     Text = GetCompleteTweetText(p),
                     Urls = p.Entities.UrlEntities.Select(u => new UrlInfo()
                     {
                         DisplayUrl = u.DisplayUrl,
                         ExpandedUrl = u.ExpandedUrl,
                         Url = u.Url
                     }).ToArray(),
                     Media = p.Entities.MediaEntities.Select(m => new MediaInfo
                     {
                         DisplayUrl = m.DisplayUrl,
                         ExpandedUrl = m.ExpandedUrl,
                         MediaUrl = m.MediaUrl,
                         MediaUrlHttps = m.MediaUrlHttps,
                         Type = m.Type,
                         Url = m.Url
                     })
                     .ToArray()
                 }).ToArray();
                return new GetLatestTweetsResponse()
                {
                    Tweets = result,
                    TweetsAudioBase64 = Convert.ToBase64String(memoryStream.ToArray())
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static string GetCompleteTweetText(LinqToTwitter.Status p)
        {
            string result = p.FullText;
            if (string.IsNullOrWhiteSpace(result))
                if (p.Retweeted)
                {
                    if (p.RetweetedStatus != null && p.RetweetedStatus.ExtendedTweet != null)
                        result = p.RetweetedStatus.ExtendedTweet.FullText ?? p.RetweetedStatus.ExtendedTweet.Text;
                    else
                        result = p.RetweetedStatus.FullText ?? p.RetweetedStatus.Text;
                }
            if (string.IsNullOrWhiteSpace(result))
                result = p.Text;

            return result;
        }
    }
}
