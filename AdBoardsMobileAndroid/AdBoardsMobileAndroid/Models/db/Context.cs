using AdBoards.ApiClient;
using AdBoards.ApiClient.Contracts.Responses;
using System;
using System.Collections.Generic;

#nullable enable
namespace AdBoardsMobileAndroid.Models.db
{
    public partial class Context
    {
        public static AdBoardsApiClient Api = new("https://adboards.site/api/");
        public static AuthorizedModel? UserNow { get; set; }
        public static Ad? AdNow { get; set; }
        public static AdListViewModel? AdList { get; set; }
    }
}
#nullable restore