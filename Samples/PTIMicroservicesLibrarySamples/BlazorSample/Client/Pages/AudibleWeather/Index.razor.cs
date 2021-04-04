using BlazorSample.Client.Info;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BlazorSample.Client.Pages.AudibleWeather
{
    public partial class Index
    {
        [Inject]
        public HttpClient Http { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        private double? Latitude { get; set; }
        private double? Longitude { get; set; }
        private string CoordinateAudioBase64 { get; set; }

        // Load the module and keep a reference to it
        // You need to use .AsTask() to convert the ValueTask to Task as it may be awaited multiple times
        private Task<IJSObjectReference> _geoCoordinatesInfoModule;
        private Task<IJSObjectReference> GeoCoordinatesInfoModule => _geoCoordinatesInfoModule ??=
            JSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/geoCoordinatesInfo.js").AsTask();

        private static Action<double, double> UpdateCoordinatesDisplayAction { get; set; }
        private bool IsCoordinatesAudioLoaded { get; set; } = false;

        public bool IsLoading { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                IsLoading = true;
                StateHasChanged();
                await GetCurrentLocation();
                UpdateCoordinatesDisplayAction = UpdateCoordinatesDisplay;
            }
            finally
            {
                IsLoading = false;
                StateHasChanged();
            }
        }

        private async void UpdateCoordinatesDisplay(double latitude, double longitude)
        {
            try
            {
                IsLoading = true;
                StateHasChanged();
                this.Latitude = latitude;
                this.Longitude = longitude;
                this.CoordinateAudioBase64 = await Http.GetStringAsync($"api/AudibleWeather/" +
                    $"GetWeather?latitude={Latitude}&longitude={Longitude}");
            }
            finally
            {
                IsLoading = false;
                StateHasChanged();
            }
        }

        private async Task GetCurrentLocation()
        {
            var module = await GeoCoordinatesInfoModule;
            await module.InvokeVoidAsync("getCurrentLocation");
        }

        [JSInvokable]
        public static void ongetCurrentLocationSuccess(double latitude, double longitude)
        {
            UpdateCoordinatesDisplayAction(latitude, longitude);
        }
    }
}
