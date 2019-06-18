namespace CN88.TotalBets.Site.Features.WagerAccount
{
    using MediatR;
    using TotalBets.Shared.Models;

    public class WagerAccountByOutrightQuery : WagerAccountBaseQuery, IRequest<WagerAccountByOutrightViewModel>
    {
        public WagerAccountByOutrightQuery(IUser viewer)
            : base(viewer)
        {
        }
    }
}