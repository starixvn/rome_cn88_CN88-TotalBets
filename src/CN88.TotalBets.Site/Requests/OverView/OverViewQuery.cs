using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CN88.TotalBets.Models;
using CN88.TotalBets.Site.ViewModels.OverView;
using MediatR;

namespace CN88.TotalBets.Site.Requests.OverView
{
    public class OverViewQuery : BaseQuery, IRequest<OverViewViewModel>
    {
        public OverViewQuery(IUser viewer) : base(viewer)
        {
        }

        public int ViewMode { get; set; }
    }
}