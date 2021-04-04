using BlazorSample.Shared.Twitter;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorSample.Client.Pages.Twitter
{
    public partial class Index
    {
        [Inject]
        public HttpClient Http { get; set; }
        public GetLatestTweetsResponse LatestTweets { get; set; }
        private string Username { get; set; }
        private bool IsLoading { get; set; }

        public async Task LoadUserTweets()
        {
            try
            {
                IsLoading = true;
                this.LatestTweets = await Http.GetFromJsonAsync<GetLatestTweetsResponse>($"api/Twitter/GetLatestTweets?username={Username}");
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
