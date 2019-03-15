namespace CN88.TotalBets.Site.Features.WagerAccount
{
    using System.Collections.Generic;
    using System.Linq;
    using MediatR;
    using Shared.Requests;
    using TotalBets.Shared.Constants;
    using TotalBets.Shared.Models;

    public class WagerAccountQuery : BaseQuery, IRequest<WagerAccountViewModel>
    {
        public WagerAccountQuery(IUser viewer)
            : base(viewer)
        {            
        }

        public int MatchId { get; set; }

        public byte StakeCondition { get; set; }

        public decimal Stake { get; set; }

        public IEnumerable<CustInfo> BreadcrumbLevel { get; set; }
    }
}