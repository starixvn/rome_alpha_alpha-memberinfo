using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Alpha.MemberInfo.Models.Transfer;
using Alpha.MemberInfo.Site.Helpers;
using Alpha.MemberInfo.Site.Models;
using Alpha.MemberInfo.Site.Requests.MemberRequest;
using Xunit;

namespace Alpha.MemberInfo.Site.Tests.Helpers
{
    [Trait("Category", "Site.Helpers")]
    public class QueryEntityModelBinderTests : QueryEntityModelBinder
    {
        protected override object CreateInstanceOfAssignableReportBaseQuery(Type modelType)
        {
            return "CreateInstanceOfAssignableReportBaseQuery";
        }

        protected override object CallBaseCreateModel(ControllerContext controllerContext, ModelBindingContext bindingContext, Type modelType)
        {
            return "CallBaseCreateModel";
        }

        protected override object CallBaseBindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return "CallBaseBindModel";
        }

        [Fact]
        public void CreateModel_ConstructorParamIsNotAssignableFromIUser_CallBaseCreateModel()
        {
            // Arrange
            var userType = typeof(CashBalanceTransfer);

            // Act
            var createdInstance = CreateModel(null, null, userType);

            // Assert
            Assert.Equal("CallBaseCreateModel", createdInstance);
        }

        [Fact]
        public void CreateModel_ConstructorParamIsAssignableFromIUser_CreateInstanceOfAssignableReportBaseQuery()
        {
            // Arrange
            var userType = typeof(ProductQuery);

            // Act
            var createdInstance = CreateModel(null, null, userType);

            // Assert
            Assert.Equal("CreateInstanceOfAssignableReportBaseQuery", createdInstance);
        }

        [Fact]
        public void BindModel_Always_CallBaseBindModel()
        {
            //// Arrange

            //// Act
            var createdInstance = BindModel(null, null);

            //// Assert
            Assert.Equal("CallBaseBindModel", createdInstance);
        }
    }
}
