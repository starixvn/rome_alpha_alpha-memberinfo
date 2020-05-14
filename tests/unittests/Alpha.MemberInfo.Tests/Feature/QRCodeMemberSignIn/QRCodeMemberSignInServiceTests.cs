namespace Alpha.MemberInfo.Tests.Services.MemberRequest
{
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn;
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Criteria;
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Models;
    using Alpha.MemberInfo.Tests.Extensions;
    using Fanex.Data.Repository;
    using Fixtures;
    using KellermanSoftware.CompareNetObjects;
    using NSubstitute;
    using System.Collections.Generic;
    using Xunit;

    [Trait("Category", "QRCodeMemberSignIn")]
    [Collection("AnyValueCreatorCollection")]
    public class QRCodeMemberSignInServiceTests
    {
        private readonly QRCodeMemberSignInServiceImpl serviceImpl;
        private readonly CompareLogic compareLogic;
        private readonly AnyValueCreatorFixture anyValueCreatorFixture;
        private readonly IDynamicRepository repository;

        public QRCodeMemberSignInServiceTests(AnyValueCreatorFixture anyValueCreatorFixture)
        {
            this.anyValueCreatorFixture = anyValueCreatorFixture;

            repository = Substitute.For<IDynamicRepository>();
            compareLogic = new CompareLogic();

            serviceImpl = new QRCodeMemberSignInServiceImpl(repository);
        }

        [Fact]
        public void GetUsernamesSuggestion_Always_ReturnsCorrectModel()
        {
            var model = new List<UsernameSuggestion>
            {
                new UsernameSuggestion("UserName", "FirstName", "LastName", "UserName2")
            };

            repository
                .Fetch<UsernameSuggestion>(Arg.Any<SelectAutoSuggestionCriteria>())
                .Returns(model as IEnumerable<UsernameSuggestion>);

            var actualModel = serviceImpl.GetUsernamesSuggestion(1, "test", false);

            AssertExtension.Equal(actualModel, model);
        }

        [Fact]
        public void GetCreditRange_Always_ReturnsCorrectModel()
        {
            var model = new CreditRangeModel(100, 100, 1000, "open");

            repository
                .Get<CreditRangeModel>(Arg.Any<SelectCreditRangeCriteria>())
                .Returns(model);

            var actualModel = serviceImpl.GetCreditRange("user Test");

            AssertExtension.Equal(actualModel, model);
        }

        [Fact]
        public void GetExistedMemberSetting_Always_ReturnsCorrectModel()
        {
            var model = anyValueCreatorFixture.Create<SportBookSettingModel>();

            repository
                .Get<SportBookSettingModel>(Arg.Any<SelectExistedMemberSettingCriteria>())
                .Returns(model);

            var actualModel = serviceImpl.GetExistedMemberSetting(1);

            AssertExtension.Equal(actualModel, model);
        }

        [Fact]
        public void CreateMemberByQRCode_Always_ReturnsCorrecResult()
        {
            var result = new ResultCreateMemberQRCode
            {
                CustId = 1,
                CustIdCopyFrom = 2,
                Success = true,
                ErrCode = 0,
                ErrMsg = string.Empty
            };

            repository
               .When(repo => repo.Execute(Arg.Any<InsertMemberCommand>()))
               .Do((callInfo) =>
               {
                   var command = callInfo[0] as InsertMemberCommand;
                   command.CustId = result.CustId;
                   command.Success = result.Success;
                   command.ErrCode = result.ErrCode;
                   command.ErrMsg = result.ErrMsg;
               });

            var actualResult = serviceImpl.CreateMemberByQRCode(
                anyValueCreatorFixture.Create<InsertMemberCommand>());

            Assert.Equal(actualResult.CustId, result.CustId);
            Assert.Equal(actualResult.ErrCode, result.ErrCode);
            Assert.Equal(actualResult.ErrMsg, result.ErrMsg);
            Assert.Equal(actualResult.Success, result.Success);
        }

        [Fact]
        public void CheckBelongingByUsername_Always_ReturnsCorrectModel()
        {
            var result = new CheckBelongingByUsernameModel(true, 1, false, 2);

            repository
                 .When(repo => repo.Execute(Arg.Any<CheckBelongingByUsernameCritieria>()))
                 .Do((callInfo) =>
                 {
                     var criteria = callInfo[0] as CheckBelongingByUsernameCritieria;
                     criteria.IsActive = result.IsActive;
                     criteria.CustId = result.CustId;
                     criteria.IsSubAcc = result.IsSubAcc;
                     criteria.RoleId = result.RoleId;
                 });

            var actualResult = serviceImpl.CheckBelongingByUsername(1, "test", 0);

            AssertExtension.Equal(actualResult, result);
        }

        [Theory]
        [InlineData(0, true)]
        [InlineData(1, false)]
        public void IsEnableCustomer_Always_ReturnsCorrectModel(int errorCode, bool expectedResult)
        {
            repository
                 .When(repo => repo.Execute(Arg.Any<CheckEnableCustomerCritieria>()))
                 .Do((callInfo) =>
                 {
                     var criteria = callInfo[0] as CheckEnableCustomerCritieria;
                     criteria.ErrCode = errorCode;
                 });

            var actualResult = serviceImpl.IsEnableCustomer(1);

            Assert.Equal(actualResult, expectedResult);
        }

        [Fact]
        public void GetSuggestUsername_Always_ReturnsCorrectModel()
        {
            var result = "AAA";

            repository
                 .When(repo => repo.Execute(Arg.Any<SelectSuggestUsernameCriteria>()))
                 .Do((callInfo) =>
                 {
                     var criteria = callInfo[0] as SelectSuggestUsernameCriteria;
                     criteria.SeqName = result;
                 });

            var actualResult = serviceImpl.GetSuggestUsername("AA", 1);

            AssertExtension.Equal(actualResult, result);
        }

        [Fact]
        public void GetRnGStatus_Always_ReturnsCorrectModel()
        {
            var model = anyValueCreatorFixture.Create<SelectRnGStatusModel>();

            repository
                .Get<SelectRnGStatusModel>(Arg.Any<SelectRnGStatusCriteria>())
                .Returns(model);

            var actualModel = serviceImpl.GetRnGStatus("Test");

            AssertExtension.Equal(actualModel, model);
        }
    }
}
