namespace CN88.TotalBets.Site.Features.WagerAccount
{
    using System.Collections.Generic;
    using MediatR;
    using Shared.Requests;
    using TotalBets.Shared.Models;

    public class WagerAccountBaseQuery : BaseQuery
    {
        public WagerAccountBaseQuery(IUser viewer)
            : base(viewer)
        {            
        }

        public int MatchId { get; set; }

        public byte StakeCondition { get; set; }

        public decimal Stake { get; set; }

        public IEnumerable<CustInfo> BreadcrumbLevel { get; set; }
    }
}