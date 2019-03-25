namespace CN88.TotalBets.Site.Features.Started
{
    using MediatR;
    using Shared.Requests;
    using TotalBets.Shared.Models;
    using ViewModels;

    public class StartedQuery : FilterQuery, IRequest<StartedViewModel>
    {
        public StartedQuery(IUser viewer) : base(viewer)
        {
        }

        public byte DateType { get; set; }
    }
}