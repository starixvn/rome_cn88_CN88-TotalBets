namespace CN88.TotalBets.Site.Features.WagerAccount
{
    using System.Collections.Generic;
    using MediatR;
    using Shared.Requests;
    using TotalBets.Shared.Models;

    public class WagerAccountByOutrightQuery : WagerAccountBaseQuery, IRequest<WagerAccountByOutrightViewModel>
    {
        public WagerAccountByOutrightQuery(IUser viewer)
            : base(viewer)
        {            
        }       
    }
}