namespace CN88.TotalBets.Site.Features.Overview
{
    using MediatR;
    using Shared.Requests;
    using TotalBets.Shared.Models;
    using ViewModels;

    public class OverViewQuery : BaseQuery, IRequest<HomeViewModel>
    {
        public OverViewQuery(IUser viewer) : base(viewer)
        {
        }

        public int PossType { get; set; }
    }
}