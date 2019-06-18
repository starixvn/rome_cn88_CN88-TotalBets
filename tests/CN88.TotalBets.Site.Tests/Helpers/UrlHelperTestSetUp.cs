using System;
using System.Collections.Generic;
using CN88.TotalBets.Site.Shared.Helpers;
using NSubstitute;

namespace CN88.TotalBets.Site.Tests.Helpers
{
    public static class UrlHelperTestSetUp
    {
        public static void SetUp()
        {
            UrlHelper.MapTotalBetsPath = Substitute.For<Func<string, string>>();
            UrlHelper.MapTotalBetsPath(Arg.Compat.Any<string>()).Returns("TotalBetsPath");

            UrlHelper.AddQueryStringParamsFunc = Substitute.For<Func<string, IDictionary<string, object>, string>>();
        }
    }
}