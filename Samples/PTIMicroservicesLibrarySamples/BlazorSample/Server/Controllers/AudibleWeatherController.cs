using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    public class AudibleWeatherController : ControllerBase
    {
        private AudibleWeatherService AudibleWeatherService { get; }
        public AudibleWeatherController(AudibleWeatherService audibleWeatherService)
        {
            this.AudibleWeatherService = audibleWeatherService;
        }

        [HttpGet("[action]")]
        public async Task<string> GetWeather(double latitude, double longitude)
        {
            MemoryStream outputStream = new MemoryStream();
            await this.AudibleWeatherService.SpeakCurrentWeatherToStreamAsync(new PTI.Microservices.Library.Models.Shared.GeoCoordinates()
            {
                Latitude = latitude,
                Longitude = longitude
            }, outputStream);
            var base64Audio = Convert.ToBase64String(outputStream.ToArray());
            return base64Audio;
        }
    }
}
