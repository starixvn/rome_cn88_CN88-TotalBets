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

    public class WagerAccountByEventViewModel
    {
        public WagerAccountByEventViewModel(
            IEnumerable<WagerAccountModel> wagerAccounts,
            WagerAccountByEventQuery message)
        {
            WagerAccounts = wagerAccounts;
            Total = new OverViewItemModel(0, WagerAccounts.Sum(i => i.Wager), WagerAccounts.Sum(i => i.Stake));
            Message = message;
            breadcrumbLevel = GetBreadcrumbLevel();
        }

        private IEnumerable<CustInfo> breadcrumbLevel { get; }

        public IEnumerable<WagerAccountModel> WagerAccounts { get; }

        public OverViewItemModel Total { get; }

        public WagerAccountByEventQuery Message { get; }

        public BreadcrumbLevelViewModel BreadcrumbLevelViewModel
            => new BreadcrumbLevelViewModel(breadcrumbLevel);

        public string GetWagerDetailUrl()
        {
            return UrlHelper.AddQueryStringParamsFunc(
                AppPathHelper.GetAppPath(AppSettings.FeaturesAppPath, "~/BetLists/EventWagerDetails"),
                new Dictionary<string, object>
                {
                    ["CustId"] = Message.ViewerId,
                    ["CustName"] = Message.ViewerName,
                    ["MatchId"] = Message.MatchId,
                    ["StakeCondition"] = Message.StakeCondition,
                    ["Stake"] = Message.Stake,
                    ["CustLevelId"] = Message.ViewerLevelId,
                    ["BetType"] = Message.BetTypeGroup,
                    ["LiveIndicator"] = Message.LiveIndicator,
                    ["BetTeam"] = Message.BetTeam
                });
        }

        public string GetDownLineUrl(int custId, string custName)
        {
            var breadscrumbToDonwline =  breadcrumbLevel.ToList();
            breadscrumbToDonwline.Add(new CustInfo(custId, custName, Message.CustLevel.GetDirectDownLineLevel().Value, false));
            var param = new Dictionary<string, object>
            {
                ["CustId"] = custId,
                ["CustName"] = custName,
                ["MatchId"] = Message.MatchId,
                ["StakeCondition"] = Message.StakeCondition,
                ["Stake"] = Message.Stake,
                ["CustLevelId"] = Message.CustLevel.GetDirectDownLineLevel().Value,
                ["BetType"] = Message.BetTypeGroup,
                ["LiveIndicator"] = Message.LiveIndicator,
                ["BetTeam"] = Message.BetTeam,
                ["BreadcrumbLevelJson"] = JsonHelper.ToJsonFunc(breadscrumbToDonwline)
            };
            var url = AppPathHelper.GetAppPath(AppSettings.TotalBetsAppPath, "~/WagerAccount/GetByEvent");
            if (Message.CustLevel.GetDirectDownLineLevel().IsMemberLevel())
            {
                url = AppPathHelper.GetAppPath(AppSettings.FeaturesAppPath, "~/BetLists/EventWagerByAccount");
            }

            return UrlHelper.AddQueryStringParamsFunc(url, param);
        }

        private IEnumerable<CustInfo> GetBreadcrumbLevel()
        {
            var breadcrumbLevel = new List<CustInfo>();

            if (Message.BreadcrumbLevel != null)
            {                
                foreach (var custInfo in Message.BreadcrumbLevel)
                {
                    if (custInfo.CustLevelId == Message.CustLevelId)
                    {
                        break;
                    }
                    custInfo.IsLink = true;
                    breadcrumbLevel.Add(custInfo);
                }
            }

            breadcrumbLevel.Add(new CustInfo(Message.CustId, Message.CustName, Message.CustLevelId, false));

            return breadcrumbLevel;
        }
    }
}