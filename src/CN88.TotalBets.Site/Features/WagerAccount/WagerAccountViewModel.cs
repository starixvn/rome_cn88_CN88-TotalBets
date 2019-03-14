using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alpha.Web.Helpers;
using CN88.TotalBets.Features.Overview.Models;
using CN88.TotalBets.Features.WagerAccount;
using CN88.TotalBets.Shared.Configurations;
using CN88.TotalBets.Site.Shared.Helpers;

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

        public string GetDownLineUrl(int custId)
            => UrlHelper.AddQueryStringParamsFunc(
                AppPathHelper.GetAppPath(AppSettings.TotalBetsAppPath, "~/WagerAccount/Index"),
                new Dictionary<string, object>
                {
                    ["CustId"] = custId,
                    ["MatchId"] = Message.MatchId,
                    ["StakeCondition"] = Message.StakeCondition,
                    ["Stake"] = Message.Stake,
                    ["CustLevelId"] = Message.CustLevel.GetDirectDownLineLevel().Value
                });
    }
}