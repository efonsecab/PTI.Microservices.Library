using BlazorSample.Shared.AudibleComputerVision;
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
    public class AudibleComputerVisionController : ControllerBase
    {
        private AudibleComputerVisionService AudibleComputerVisionService { get; }

        public AudibleComputerVisionController(AudibleComputerVisionService audibleComputerVisionService)
        {
            this.AudibleComputerVisionService = audibleComputerVisionService;
        }

        [HttpPost("[action]")]
        public async Task<string> DescribeImage(DescribeImageRequestModel model)
        {
            var imageBytes = Convert.FromBase64String(model.ImageBase64);
            MemoryStream imageStream = new MemoryStream(imageBytes);
            MemoryStream outputStream = new MemoryStream();
            await this.AudibleComputerVisionService.DescribeImageToStreamAsync(
                imageStream, outputStream, "camera");
            var result = Convert.ToBase64String(outputStream.ToArray());
            return result;
        }
    }
}
