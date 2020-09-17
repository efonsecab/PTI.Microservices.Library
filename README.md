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
