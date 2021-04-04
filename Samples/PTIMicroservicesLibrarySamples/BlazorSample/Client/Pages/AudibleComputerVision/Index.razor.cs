using BlazorSample.Shared.AudibleComputerVision;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BlazorSample.Client.Pages.AudibleComputerVision
{
    public partial class Index
    {
        [Inject]
        public HttpClient Http { get; set; }
        public string PhotoData { get; set; }
        public ElementReference CanvasElement { get; set; }
        public ElementReference VideoElement { get; set; }
        public ElementReference ImageElement { get; set; }
        public ElementReference AudioElement { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        public string AudioBase64 { get; private set; }

        public string FileData { get; set; }
        public string FileName { get; private set; }
        public string ResponseMessage { get; private set; }

        private bool IsLoading { get; set; } = false;
        public string ImageBase64 { get; private set; }

        private async Task OnInputFileChange(InputFileChangeEventArgs e)
        {
            IsLoading = true;
            this.ResponseMessage = $"Invoked: {e.FileCount} files";
            this.FileName = e.File.Name;
            if (e.File != null)
            {
                int maxSizeInMB = (1024 * 1000) * 10; // 10MB
                var fileStream = e.File.OpenReadStream(maxAllowedSize: maxSizeInMB);
                MemoryStream memoryStream = new MemoryStream();
                await fileStream.CopyToAsync(memoryStream);
                var imageBase64 = Convert.ToBase64String(memoryStream.ToArray());
                this.ImageBase64 = imageBase64;
                StateHasChanged();
                DescribeImageRequestModel model = new DescribeImageRequestModel()
                {
                    ImageBase64 = imageBase64
                };
                string requestUrl = "api/AudibleComputerVision/DescribeImage";
                var response = await Http.PostAsJsonAsync<DescribeImageRequestModel>(requestUrl, model);
                if (response.IsSuccessStatusCode)
                {
                    this.AudioBase64 = await response.Content.ReadAsStringAsync();
                    StateHasChanged();
                }
                this.ResponseMessage = await response.Content.ReadAsStringAsync();
            }
            IsLoading = false;
        }
        public async Task DescribeImage()
        {

            var fileBytes = await System.IO.File.ReadAllBytesAsync(this.FileData);
            var imageBase64 = Convert.ToBase64String(fileBytes);
            DescribeImageRequestModel model = new DescribeImageRequestModel()
            {
                ImageBase64 = imageBase64
            };
            string requestUrl = "api/AudibleComputerVision/DescribeImage";
            var response = await Http.PostAsJsonAsync<DescribeImageRequestModel>(requestUrl, model);
            if (response.IsSuccessStatusCode)
            {
                this.AudioBase64 = await response.Content.ReadAsStringAsync();
            }
        }

    }
}
