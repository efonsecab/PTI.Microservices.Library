# PTI.Microservices.Library

## Documentation for our NuGet Package "PTI.Microservices.Library"

The purpose of this package is to help developers create solutions faster by integrating several Cloud Services in one package,
and enforcing Logging and Exception Handling, while still allowing the developers to have the control over the configuration.
The package is designed to be consumed from microservices, so most items are async and do as little as possible, unless you configure it to do more, for example, in the case of Twitter Services, they support retry configuration, and have out of the box support for waiting for Twitter API Rate Limit, thanks for the great work done by Joe Mayo (https://github.com/JoeMayo) with his LinqToTwitter library.
The TwitterFakeFollowersService currently supports communicating back to the consumer by using Action delegates, and the future version of it will support using SignalR.
"PTI.Microservices.Library" is currently running under .NET Core 3.1.

The package exposes services such as:
* Azure Bing Search
* Azure Computer Vision
* Azure Custom Vision
* Azure Face
* Azure Maps
* Azure Speech
* Azure Video Indexer
* IpStack
* Microsoft Graph
* Paypal
* Twitter
* Youtube

You can download the package at:
https://www.nuget.org/packages/PTI.Microservices.Library/

## About the Configuration Classes
The services configuration classes have an endpoint property, in the case of Azure services, those will usually be the Url for your created resource in Azure, or the base azure service api.
In the case of the specialized services such as Customer Finder, Emotions Analyzer, Books Translation and similar, the property is prefilled with the Rapid API base service, if you use appSettings-based configuration the default value will be overwritten to respect developer-based configuration, this means you will need to set the correct service Url when using appSettings-based configuration.

## Quick Samples

### Sample 1

    [HttpPost("[action]")]
        public async Task<IActionResult> UploadImages([FromBody]UploadImagesModel model)
        {
            Guid projectId = Guid.Parse(model.ProjectId);
            List<Uri> lstImages = model.Items.Where(p=>p.IsSelected==true).Select(p => new Uri(p.ImageUrl)).ToList();
            var uploadImagesResult = await AzureCustomVisionService.UploadImagesAsync(lstImages, projectId);
            List<string> tags = new List<string>() { model.Tag };
            foreach (var singleImage in uploadImagesResult)
            {
                try
                {
                    await this.AzureCustomVisionService.CreateImageTagsAsync(projectId,
                        singleImage.Image.Id,
                        tags);
                    await Task.Delay(TimeSpan.FromSeconds(1));
                }
                catch (Exception ex)
                {
                    this.Logger.LogError(ex, ex.Message);
                }
            }
            return Ok();
        }
        
### Sample 2
    await twitterFakeFollowersService.GetAllPossibleFakeFollowersForUsernameAsync(this.TwitterConfiguration.ScreenName,
                (possibleFakeUser) =>
                {
                   //Your custom logic to execute when a new possible twitter fake user has been detected
                }, cancellationToken: cancellationTokenSource.Token);

### Sample 3
    AudibleWeatherService audibleWeatherService = new AudibleWeatherService(logger, azureMapsService, azureSpeechService);
    await audibleWeatherService.SpeakCurrentWeatherAsync(geoCoordinates);

### Sample 4
    var result = await booksTranslationService.TranslateDocXFileFromUrlAsync(fileUrl, TranslationLanguage.English, TranslationLanguage.Spanish,
    BookTranslationMode.KeepFormatting, emailAddress:"youremail@yourdomain.xyz");
    
### Sample 5
    var allKeywords = await azureVideoIndexerService.GetAllKeywordsAsync(onNewKeywordFound:(keyword)=> 
    {
       //Your custom code to execute when a keyword has been processed.Added so that you do not have to wait for the whole process to finish
    });
    
### Sample 6
    var result =
        await emotionsAnalyzerService.AnalyzeFileFromUrlAsync(fileUrl,
        model: new Microservices.Library.Models.EmotionsAnalyzer.AnalyzeFileModel()
        {
            SourceFileUrl=fileUrl,
            EmailForResults="youremail@yourdomain.xyz",
            AnalysisWorksheet = 
                new Microservices.Library.Models.EmotionsAnalyzer.AnalysisWorksheet()
                {
                   ColumnToAnalyze="PostText",
                   ColumnWithDate="RecordDate",
                   ColumnWithUniqueId="RecordUniqueId",
                   SentimentPlaceholderColumn="SentimentAnalysisPlaceHolder",
                   WorksheetName="FacebookPosts"
                },
                PlaceWorksheet=new Microservices.Library.Models.EmotionsAnalyzer.PlaceWorksheet()
                {
                   SentimentPlaceholderColumn="PlaceSentimentPlaceHolder",
                   WorksheetName="PlaceSentiment"
                }
        });

### Sample 7
    TwitterDataAnalysisService twitterDataAnalysisService = new TwitterDataAnalysisService(
    logger, twitterService, azureTextAnalyticsService);
    var twitterUserTopics = await twitterDataAnalysisService.GetTopicsForUserAsync("twitterusername");

The following are sample applications of things you could do with the package
* Search Images on Bing and feed your Custom Vision Models: https://github.com/efonsecab/BlazorCustomVisionUploader
* Search Images on Bing and feed your Azure Video Indexer Person Models: https://github.com/efonsecab/BlazorVideoIndexerUploader

For inquiries, and business deals, you can write an email to services@pticostarica.com

## Roadmap
* October 2020
  * Integrate Custom Finder APIs into the package. You can check our public APIs here:https://rapidapi.com/organization/pti-costa-rica
* November 2020
  * Add Support for Azure Cognitive Search
  * Integrate PTI Emotions Analyzer APIs into the package.
* December 2020
  * Add support for gRPC
  * Integrate PTI Books Translator APIs into the package
  * Enable package monthly payment using Paypal-based subscriptions.
* January 2021
  * Create a .NET 5 based version of the package
  * Add support for SignalR to TwitterFakeFollowersService (only for .NET 5)
  * Integrate PTI's SitemapScanner into the main package (.NET 3.1 version)
  
  ## Feature Requests
  If there is any functionality you would like to see included in the package, write an email with the subject "Feature Request" to services@pticostarica.com
