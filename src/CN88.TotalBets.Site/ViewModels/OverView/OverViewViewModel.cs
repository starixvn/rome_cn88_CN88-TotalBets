using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CN88.TotalBets.Models.OverView;
using CN88.TotalBets.Site.Requests.OverView;

namespace CN88.TotalBets.Site.ViewModels.OverView
{
    public class OverViewViewModel
    {
        public OverViewViewModel(OverViewQuery message, OverViewModel overViewModel)
        {
            this.message = message;
            this.OverViewModel = overViewModel;
        }

        private OverViewQuery message { get; }

        public OverViewModel OverViewModel { get; }
    }
}