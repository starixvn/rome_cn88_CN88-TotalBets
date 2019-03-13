namespace CN88.TotalBets.Site.Controllers
{
    using System.Diagnostics.CodeAnalysis;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using MediatR;
    using Requests;
    using Web.Controllers;

    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1304:NonPrivateReadonlyFieldsMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
    public abstract class SiteBaseController : CN88BaseController
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

        protected async Task<ActionResult> ViewAsync<TResponse>(IRequest<TResponse> message, string viewPath)
             => (message as BaseQuery).IsValid() ?
                View(viewPath, await mediator.Send(message).ConfigureAwait(false)) : View(NoInformationPageViewPath);

    }
}