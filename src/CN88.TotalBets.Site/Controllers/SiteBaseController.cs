namespace CN88.TotalBets.Site.Controllers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Alpha.Web;
    using MediatR;
    using Requests;

    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1304:NonPrivateReadonlyFieldsMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
    public abstract class SiteBaseController : BaseController
    {
        private const string NoInformationPageViewPath = "~/Shared/Views/NoInformation.cshtml";
        private const string InvalidExcelFileName = "Invalid";

        protected readonly IMediator mediator;

        protected SiteBaseController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        protected async Task<ActionResult> ViewAsync<TResponse>(IRequest<TResponse> message)
             => (message as BaseQuery).IsValid() ?
                View(await mediator.Send(message).ConfigureAwait(false)) : View(NoInformationPageViewPath);

    }
}