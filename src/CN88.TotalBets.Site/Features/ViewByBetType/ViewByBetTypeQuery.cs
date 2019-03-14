namespace CN88.TotalBets.Site.Features.ViewByBetType
{
    using CN88.TotalBets.Shared.Models;
    using CN88.TotalBets.Site.Features.ViewByBetType.ViewModels;
    using CN88.TotalBets.Site.Shared.Requests;
    using MediatR;

    public class ViewByBetTypeQuery : FilterQuery, IRequest<ViewByBetTypeViewModel>
    {
        public ViewByBetTypeQuery(IUser viewer)
            : base(viewer)
        {
        }

        public byte MarketType { get; set; }

        public byte Type { get; set; }

        public int MatchId { get; set; }
    }
}