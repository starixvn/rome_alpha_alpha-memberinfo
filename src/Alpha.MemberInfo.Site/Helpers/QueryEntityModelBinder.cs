namespace Alpha.MemberInfo.Site.Helpers
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Web.Mvc;
    using MemberInfo.Models;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;
    using Requests;
    using Sunplus.Agent.Model.Entities;
    using Sunplus.Agent.Utilities;

    public class QueryEntityModelBinder : DefaultModelBinder
    {
        private static readonly Type[] IUserType = { typeof(IUser) };
        private static readonly Type MemberInfoBaseQueryType = typeof(MemberInfoBaseQuery);

        protected override object CreateModel(
            ControllerContext controllerContext,
            ModelBindingContext bindingContext,
            Type modelType)
        {
            if (modelType != null
                && MemberInfoBaseQueryType.IsAssignableFrom(modelType)
                && modelType.GetConstructor(IUserType) != null)
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

            return boundModel;
        }

        protected virtual object CallBaseBindModel(
           ControllerContext controllerContext,
           ModelBindingContext bindingContext)
            => base.BindModel(controllerContext, bindingContext);

        protected virtual void LogWarning(JsonException ex)
        {
            Logs.WriteWarn(ex, UserSession.Current.CustId);
        }

        public void HandleDeserializationError(object sender, ErrorEventArgs errorArgs)
        {
            if (errorArgs.ErrorContext.Error.Message.Contains(".CustLevelId'"))
            {
                errorArgs.ErrorContext.Handled = true;
            }
        }
    }
}