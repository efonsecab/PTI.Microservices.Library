### Sample 8 - Organize people on Twitter Lists
    var result = await customerFinderService.GetTwitterProfilesByKeywordAsync(keyword);
    var totalPaes = Math.Ceiling((double)result.Count() / 100);
    for (int iPage = 0; iPage < totalPaes; iPage++)
    {
       var pageItems = result.Skip(iPage * 100).Take(100).Select(p => p.username).ToList();
       await twitterService.AddUsersToListAsync(pageItems, azurePeopleList.ListIDResponse);
    }
