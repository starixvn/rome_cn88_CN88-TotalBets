namespace CN88.TotalBets.Site.Features.WagerAccount
{
    using System.Collections.Generic;
    using System.Linq;
    using Alpha.Web.Helpers;
    using CN88.TotalBets.Features.Overview.Models;
    using CN88.TotalBets.Features.WagerAccount;
    using CN88.TotalBets.Shared.Configurations;
    using CN88.TotalBets.Shared.Models;
    using CN88.TotalBets.Site.Shared.Helpers;
    using CN88.TotalBets.Site.Shared.ViewModels;

    public class WagerAccountByOutrightViewModel
    {
        public WagerAccountByOutrightViewModel(
            IEnumerable<WagerAccountModel> wagerAccounts,
            WagerAccountByOutrightQuery message,
            bool isMobile)
        {
            WagerAccounts = wagerAccounts;
            Total = new OverViewItemModel(0, WagerAccounts.Sum(i => i.Wager), WagerAccounts.Sum(i => i.Stake));
            Message = message;
            breadcrumbLevel = GetBreadcrumbLevel();
            IsMobile = isMobile;
        }

        private IEnumerable<CustInfo> breadcrumbLevel { get; }

        public IEnumerable<WagerAccountModel> WagerAccounts { get; }

        public OverViewItemModel Total { get; }

        public WagerAccountByOutrightQuery Message { get; }

        public bool IsMobile { get; }

        public BreadcrumbLevelViewModel BreadcrumbLevelViewModel
            => new BreadcrumbLevelViewModel(breadcrumbLevel);

        public string GetWagerDetailUrl()
        {
            return UrlHelper.AddQueryStringParamsFunc(
                UrlHelper.MapFeaturesPath("~/BetLists/OutrightWagerDetails"),
                new Dictionary<string, object>
                {
                    ["CustId"] = Message.ViewerId,
                    ["CustName"] = Message.ViewerName,
                    ["MatchId"] = Message.MatchId,
                    ["StakeCondition"] = Message.StakeCondition,
                    ["Stake"] = Message.Stake,
                    ["CustLevelId"] = Message.ViewerLevelId
                });
        }

        public string GetDownLineUrl(int custId, string custName)
        {
            var breadscrumbToDonwline = breadcrumbLevel.ToList();
            breadscrumbToDonwline.Add(new CustInfo(custId, custName, Message.CustLevel.GetDirectDownLineLevel().Value, false));
            var param = new Dictionary<string, object>
            {
                ["CustId"] = custId,
                ["CustName"] = custName,
                ["MatchId"] = Message.MatchId,
                ["StakeCondition"] = Message.StakeCondition,
                ["Stake"] = Message.Stake,
                ["CustLevelId"] = Message.CustLevel.GetDirectDownLineLevel().Value,
                ["BreadcrumbLevelJson"] = JsonHelper.ToJsonFunc(breadscrumbToDonwline)
            };
            var url = AppPathHelper.GetAppPath(AppSettings.TotalBetsAppPath, "~/WagerAccount/GetByOutright");
            if (Message.CustLevel.GetDirectDownLineLevel().IsMemberLevel())
            {
                url = AppPathHelper.GetAppPath(AppSettings.FeaturesAppPath, "~/BetLists/OutrightWagerByAccount");
            }

            return UrlHelper.AddQueryStringParamsFunc(url, param);
        }

        private IEnumerable<CustInfo> GetBreadcrumbLevel()
        {
            var breadcrumbLevels = new List<CustInfo>();

            if (Message.BreadcrumbLevel != null)
            {
                foreach (var custInfo in Message.BreadcrumbLevel)
                {
                    if (custInfo.CustLevelId == Message.CustLevelId)
                    {
                        break;
                    }
                    custInfo.IsLink = true;
                    breadcrumbLevels.Add(custInfo);
                }
            }

            breadcrumbLevels.Add(new CustInfo(Message.CustId, Message.CustName, Message.CustLevelId, false));

            return breadcrumbLevels;
        }

        public BreadcrumbItem BuildBackLink()
        {
            var breadcrumbLinks = BreadcrumbLevelViewModel.BuildBreadcrumbLinks(Message.CurrentUrl);
            var totalItem = breadcrumbLinks.Count();

            if (totalItem == 0)
            {
                return new BreadcrumbItem(string.Empty, string.Empty);
            }

            var firstItem = breadcrumbLinks.First();

            return totalItem > 1
                    ? breadcrumbLinks.ElementAt(breadcrumbLinks.Count() - 2)
                    : new BreadcrumbItem(firstItem.Text, string.Empty);
        }
    }
}