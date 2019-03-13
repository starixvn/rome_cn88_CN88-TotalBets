namespace CN88.TotalBets.Site.Shared.Helpers
{
    using System;
    using System.Web.Mvc;
    using Requests;
    using TotalBets.Shared.Models;

    public class QueryEntityModelBinder : DefaultModelBinder
    {
        private static readonly Type[] IUserType = { typeof(IUser) };
        private static readonly Type BaseQueryType = typeof(BaseQuery);

        protected override object CreateModel(
            ControllerContext controllerContext,
            ModelBindingContext bindingContext,
            Type modelType)
        {
            if (modelType != null
                && BaseQueryType.IsAssignableFrom(modelType)
                && modelType.GetConstructor(IUserType) != null)
            {
                return CreateInstanceOfAssignableBaseQuery(modelType);
            }

            return CallBaseCreateModel(controllerContext, bindingContext, modelType);
        }

        protected virtual object CreateInstanceOfAssignableBaseQuery(Type modelType)
            => Activator.CreateInstance(modelType, DependencyResolver.Current.GetService<IUser>());

        protected virtual object CallBaseCreateModel(
           ControllerContext controllerContext,
           ModelBindingContext bindingContext,
           Type modelType) => base.CreateModel(controllerContext, bindingContext, modelType);

        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var boundModel = CallBaseBindModel(controllerContext, bindingContext);

            var baseQuery = boundModel as BaseQuery;
            if (baseQuery != null)
            {
                var breadcrumbLevelJson = GetBreadcrumbLevelJson(controllerContext);

                if (!string.IsNullOrWhiteSpace(breadcrumbLevelJson)
                   && breadcrumbLevelJson.LastIndexOf(",[", StringComparison.OrdinalIgnoreCase) < 0)
                {
                    //baseQuery.BreadcrumbLevel = JsonConvert.DeserializeObject<IEnumerable<CustInfo>>(
                    //    breadcrumbLevelJson,
                    //    new JsonSerializerSettings
                    //    {
                    //        Error = (sender, errorArgs) => { errorArgs.ErrorContext.Handled = true; }
                    //    });
                }

                return baseQuery;
            }

            return boundModel;
        }

        protected virtual object CallBaseBindModel(
           ControllerContext controllerContext,
           ModelBindingContext bindingContext)
            => base.BindModel(controllerContext, bindingContext);

        protected virtual string GetBreadcrumbLevelJson(ControllerContext controllerContext)
            => controllerContext.RequestContext.HttpContext.Request["BreadcrumbLevelJson"];
    }
}