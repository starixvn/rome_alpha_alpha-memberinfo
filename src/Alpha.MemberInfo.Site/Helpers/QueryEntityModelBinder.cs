namespace Alpha.MemberInfo.Site.Helpers
{
    using Alpha.MemberInfo.Features.Shared.Models;
    using Alpha.MemberInfo.Site.Requests;
    using MemberInfo.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Web.Mvc;

    public class QueryEntityModelBinder : DefaultModelBinder
    {
        private static readonly Type[] IUserType = { typeof(IUser) };

        protected override object CreateModel(
            ControllerContext controllerContext,
            ModelBindingContext bindingContext,
            Type modelType)
        {
            if (modelType != null && modelType.GetConstructor(IUserType) != null)
            {
                return CreateInstanceOfAssignableReportBaseQuery(modelType);
            }

            return CallBaseCreateModel(controllerContext, bindingContext, modelType);
        }

        protected virtual object CreateInstanceOfAssignableReportBaseQuery(Type modelType)
            => Activator.CreateInstance(modelType, DependencyResolver.Current.GetService<IUser>());

        protected virtual object CallBaseCreateModel(
           ControllerContext controllerContext,
           ModelBindingContext bindingContext,
           Type modelType) => base.CreateModel(controllerContext, bindingContext, modelType);

        [SuppressMessage("Microsoft.Performance", "CA1804:RemoveUnusedLocals", MessageId = "ex", Justification = "Reviewed")]
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var boundModel = CallBaseBindModel(controllerContext, bindingContext);

            var reportBaseQuery = boundModel as MemberInfoBaseQuery;
            if (reportBaseQuery != null)
            {
                var breadcrumbLevelJson = GetBreadcrumbLevelJson(controllerContext);

                if (!string.IsNullOrWhiteSpace(breadcrumbLevelJson)
                   && breadcrumbLevelJson.LastIndexOf(",[", StringComparison.OrdinalIgnoreCase) < 0)
                {
                    reportBaseQuery.BreadcrumbLevel = JsonConvert.DeserializeObject<IEnumerable<CustInfo>>(
                        breadcrumbLevelJson,
                        new JsonSerializerSettings
                        {
                            Error = (sender, errorArgs) => { errorArgs.ErrorContext.Handled = true; }
                        });
                }

                return reportBaseQuery;
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