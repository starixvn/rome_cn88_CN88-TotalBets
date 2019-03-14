namespace CN88.TotalBets.Site.Features.Events.Request
{
    using MediatR;
    using Shared.Requests;
    using TotalBets.Shared.Models;
    using ViewModels;

    public class InPlayQuery : FilterQuery, IRequest<InPlayViewModel>
    {
        public InPlayQuery(IUser viewer)
            : base(viewer)
        {
        }

        public byte IsLive { get; set; }
    }
}
