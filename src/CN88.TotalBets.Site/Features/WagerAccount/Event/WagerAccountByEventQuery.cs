namespace CN88.TotalBets.Site.Features.WagerAccount
{
    using System.Collections.Generic;
    using MediatR;
    using Shared.Requests;
    using TotalBets.Shared.Models;

    public class WagerAccountByEventQuery : WagerAccountBaseQuery, IRequest<WagerAccountByEventViewModel>
    {
        public WagerAccountByEventQuery(IUser viewer)
            : base(viewer)
        {            
        }

        public int BetType { get; set; }

        public string BetTeam { get; set; }

        public int LiveIndicator { get; set; }   
        
        public byte Type { get; set; }     
    }
}