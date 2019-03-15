namespace CN88.TotalBets.Site.Features.WagerAccount
{
    using System.Collections.Generic;
    using MediatR;
    using Shared.Requests;
    using TotalBets.Shared.Models;

    public class WagerAccountQuery : WagerAccountBaseQuery, IRequest<WagerAccountViewModel>
    {
        public WagerAccountQuery(IUser viewer)
            : base(viewer)
        {            
        }       
    }
}