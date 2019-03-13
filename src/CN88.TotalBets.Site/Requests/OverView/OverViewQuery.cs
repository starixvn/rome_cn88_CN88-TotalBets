using System;
using CN88.TotalBets.Models;
using CN88.TotalBets.Site.ViewModels;
using MediatR;

namespace CN88.TotalBets.Site.Requests.OverView
{
    public class OverViewQuery : BaseQuery, IRequest<HomeViewModel>
    {
        public OverViewQuery(IUser viewer) : base(viewer)
        {
        }

        public int PossType { get; set; }

        public Func<int, string> GetSportDisplayName { get; set; }
    }
}