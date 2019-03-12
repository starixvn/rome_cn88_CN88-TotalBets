namespace CN88.TotalBets.Site.ViewModels.Outright
{
    using System.Collections.Generic;
    using TotalBets.Models;
    using TotalBets.Models.Outright;

    public class OutrightViewModel
    {
        public OutrightViewModel(
            int custId,
            UserLevel userLevel,
            IEnumerable<OutrightLeague> outrights)
        {
            CustId = custId;
            UserLevel = userLevel;
            Outrights = outrights;
        }

        public int CustId { get; }

        public UserLevel UserLevel { get; }

        public IEnumerable<OutrightLeague> Outrights { get; }
    }
}