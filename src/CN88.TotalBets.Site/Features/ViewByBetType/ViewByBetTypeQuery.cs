namespace CN88.TotalBets.Site.Features.ViewByBetType
{
    using MediatR;
    using Shared.Requests;
    using TotalBets.Shared.Models;
    using ViewModels;

    public class ViewByBetTypeQuery : FilterQuery, IRequest<ViewByBetTypeViewModel>
    {
        public ViewByBetTypeQuery(IUser viewer)
            : base(viewer)
        {
        }

        public int MatchId { get; set; }
    }
}