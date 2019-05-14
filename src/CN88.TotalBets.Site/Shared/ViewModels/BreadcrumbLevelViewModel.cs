namespace CN88.TotalBets.Site.Shared.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using CN88.TotalBets.Shared.Models;
    using CN88.TotalBets.Site.Shared.Helpers;

    public class BreadcrumbLevelViewModel
    {
        public BreadcrumbLevelViewModel(
            IEnumerable<CustInfo> breadcrumbLevel,
            string breadcrumbLevelJson = null)
        {
            BreadcrumbLevel = breadcrumbLevel;
            BreadcrumbLevelJson = breadcrumbLevelJson ?? JsonHelper.ToJsonFunc(breadcrumbLevel);
        }

        public IEnumerable<CustInfo> BreadcrumbLevel { get; }

        public string BreadcrumbLevelJson { get; }

        public string BuildBreadcrumbLink(CustInfo model, string currentLink, IEnumerable<string> breadcrumbParamByLevel, int breadcrumbIndex)
        {
            var url = breadcrumbParamByLevel == null
                ? currentLink
                : UrlHelper.GetSiteUrl(breadcrumbParamByLevel.ElementAt(breadcrumbIndex), currentLink);

            return UrlHelper.ReplaceOrAddQueryStringParamsFunc(
                url,
                new Dictionary<string, object>
                {
                    ["CustId"] = model.CustId,
                    ["CustName"] = model.CustName,
                    ["CustLevelId"] = model.CustLevelId,
                    ["BreadcrumbLevelJson"] = BreadcrumbLevelJson,
                });
        }

        public IEnumerable<BreadcrumbItem> BuildBreadcrumbLinks(string currentLink, IEnumerable<string> breadcrumbParamByLevel = null)
        {
            if (BreadcrumbLevel.Any())
            {
                var linkIndex = 0;
                return BreadcrumbLevel.Select(custInfo =>
                    new BreadcrumbItem(custInfo.CustName, BuildBreadcrumbLink(custInfo, currentLink, breadcrumbParamByLevel, linkIndex++)));
            }

            return Enumerable.Empty<BreadcrumbItem>();
        }
    }

    public class BreadcrumbItem
    {
        public BreadcrumbItem(string text, string link)
        {
            Text = text;
            Link = link;
        }

        public string Text { get; }

        public string Link { get; }
    }
}