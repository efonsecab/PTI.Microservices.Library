# PTI.Microservices.Library

## Documentation for our NuGet Package "PTI.Microservices.Library"

The purpose of this package is to help developers create solutions faster by integrating several Cloud Services in one package,
and enforcing Logging and Exception Handling, while still allowing the developers to have the control over the configuration.
The package is designed to be consumed from microservices, so most items are async, in the case of Twitter Services, they support retry configuration, and have out of the box support for waiting for Twitter API Rate Limit, thanks for the great work done by Joe Mayo (https://github.com/JoeMayo) with his LinqToTwitter library.
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

