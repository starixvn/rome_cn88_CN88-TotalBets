namespace CN88.TotalBets.Site.Features.Outright
{
    using MediatR;
    using Shared.Requests;
    using TotalBets.Shared.Constants;
    using TotalBets.Shared.Models;

    public class OutrightQuery : FilterQuery, IRequest<OutrightViewModel>
    {
        public OutrightQuery(IUser viewer)
            : base(viewer)
        {
            Type = EventTypeConstants.Outright;
        }
    }
}