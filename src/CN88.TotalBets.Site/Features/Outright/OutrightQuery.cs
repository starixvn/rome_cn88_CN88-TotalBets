namespace CN88.TotalBets.Site.Features.Outright
{
    using MediatR;
    using Shared.Requests;
    using TotalBets.Shared.Constants;
    using TotalBets.Shared.Models;

    public class OutrightQuery : BaseQuery, IRequest<OutrightViewModel>
    {
        public OutrightQuery(IUser viewer)
            : base(viewer)
        {
            SportTypeGroup = SportTypeConstants.Soccer;
        }

        public int SportTypeGroup { get; set; }

        public int LeagueId { get; set; }

        public byte StakeCondition { get; set; }

        public decimal Stake { get; set; }

        public byte PossType { get; set; }
    }
}