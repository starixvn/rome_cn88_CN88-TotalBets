namespace CN88.TotalBets.Site.Features.Events
{
    using MediatR;
    using Shared.Requests;
    using TotalBets.Shared.Constants;
    using TotalBets.Shared.Models;
    using ViewModels;

    public class EventQuery : FilterQuery, IRequest<EventViewModel>
    {
        public EventQuery(IUser viewer)
            : base(viewer)
        {
            Market = MarketConstants.AllMarkets;
        }

        public byte Type { get; set; }

        public byte Market { get; set; }
    }
}