namespace CN88.TotalBets.Site.Features.Overview.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using TotalBets.Features.Overview.Models;

    public abstract class OverViewByBaseViewModel
    {
        public IList<OverViewItemModel> SportsColumn { get; protected set; } = new List<OverViewItemModel>();

        public abstract string Text { get; }

        public abstract string SubMenuId { get; }

        public abstract string Url(int possType, int sportTypeGroup);

        public OverViewItemModel Total
        {
            get
            {
                var totalWager = SportsColumn.Sum(i => i.Wager);
                var totalStake = SportsColumn.Sum(i => i.Stake);

                return new OverViewItemModel(0, totalWager, totalStake);
            }
        }
    }
}