using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alpha.Web.Helpers;
using CN88.TotalBets.Features.Overview.Models;
using CN88.TotalBets.Features.WagerAccount;
using CN88.TotalBets.Shared.Configurations;
using CN88.TotalBets.Shared.Models;
using CN88.TotalBets.Site.Shared.Helpers;
using CN88.TotalBets.Site.Shared.ViewModels;

namespace CN88.TotalBets.Site.Features.WagerAccount
{
    public class WagerAccountViewModel
    {
        public WagerAccountViewModel(
            IEnumerable<WagerAccountModel> wagerAccounts,
            WagerAccountQuery message)
        {
            WagerAccounts = wagerAccounts;
            Total = new OverViewItemModel(WagerAccounts.Sum(i => i.Wager), WagerAccounts.Sum(i => i.Stake));
            Message = message;
        }

        public IEnumerable<WagerAccountModel> WagerAccounts { get; }

        public OverViewItemModel Total { get; }

        public WagerAccountQuery Message { get; }

        public BreadcrumbLevelViewModel BreadcrumbLevelViewModel
            => new BreadcrumbLevelViewModel(GetBreadcrumbLevel());

        public string GetWagerDetailUrl()
        {
            return UrlHelper.AddQueryStringParamsFunc(
                AppPathHelper.GetAppPath(AppSettings.FeaturesAppPath, "~/BetLists/WagerDetails"),
                new Dictionary<string, object>
                {
                    ["CustId"] = Message.CustId,
                    ["CustName"] = Message.CustName,
                    ["MatchId"] = Message.MatchId,
                    ["StakeCondition"] = Message.StakeCondition,
                    ["Stake"] = Message.Stake,
                    ["CustLevelId"] = Message.CustLevel.Value
                });
        }

        public string GetDownLineUrl(int custId, string custName)
        {
            var url = AppPathHelper.GetAppPath(AppSettings.TotalBetsAppPath, "~/WagerAccount/Index");
            if (Message.CustLevel.GetDirectDownLineLevel().IsMemberLevel())
            {
                url = AppPathHelper.GetAppPath(AppSettings.FeaturesAppPath, "~/BetLists/BetListByAccount");
            }

            return UrlHelper.AddQueryStringParamsFunc(
                url,
                new Dictionary<string, object>
                {
                    ["CustId"] = custId,
                    ["CustName"] = custName,
                    ["MatchId"] = Message.MatchId,
                    ["StakeCondition"] = Message.StakeCondition,
                    ["Stake"] = Message.Stake,
                    ["CustLevelId"] = Message.CustLevel.GetDirectDownLineLevel().Value,
                    ["BreadcrumbLevelJson"] = JsonHelper.ToJsonFunc(GetBreadcrumbLevel())
                });
        }

        private IEnumerable<CustInfo> GetBreadcrumbLevel()
        {
            var breadcrumbLevel = new List<CustInfo>();

            if (Message.BreadcrumbLevel != null
                && Message.BreadcrumbLevel.Any())
            {
                foreach (var custInfo in Message.BreadcrumbLevel)
                {
                    if (custInfo.CustLevelId == Message.CustLevelId)
                    {
                        break;
                    }

                    breadcrumbLevel.Add(custInfo);
                }
            }

            breadcrumbLevel.Add(new CustInfo(Message.CustId, Message.CustName, Message.CustLevelId));

            return breadcrumbLevel;            
        }
    }
}