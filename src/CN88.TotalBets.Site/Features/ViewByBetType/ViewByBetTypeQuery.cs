namespace CN88.TotalBets.Site.Features.ViewByBetType
{
    using MediatR;
    using Shared.Requests;
    using TotalBets.Shared.Constants;
    using TotalBets.Shared.Models;
    using ViewModels;

    public class ViewByBetTypeQuery : FilterQuery, IRequest<ViewByBetTypeViewModel>
    {
        public ViewByBetTypeQuery(IUser viewer)
            : base(viewer)
        {
        }

        public byte Type { get; set; }

        public int MatchId { get; set; }

        public bool IsShowKODate { get; set; }

        public bool IsShowMarketSelection => Type == EventTypeConstants.InPlay || Type == EventTypeConstants.Started;

    }
}