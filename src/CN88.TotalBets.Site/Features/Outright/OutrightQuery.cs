namespace CN88.TotalBets.Site.Features.Outright
{
    using MediatR;
    using Shared.Requests;
    using TotalBets.Shared.Models;
    using ViewModels;

    public class OutrightQuery : BaseQuery, IRequest<OutrightViewModel>
    {
        public OutrightQuery(IUser viewer)
            : base(viewer)
        {
        }

        public int SportTypeGroup { get; set; }

        public int LeagueId { get; set; }

        public byte StakeCondition { get; set; }

        public decimal Stake { get; set; }

        public byte PossType { get; set; }
    }
}