namespace CN88.TotalBets.Site.Features.Overview
{
    using System;
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

        public Func<int, string> GetSportDisplayName { get; set; }
    }
}