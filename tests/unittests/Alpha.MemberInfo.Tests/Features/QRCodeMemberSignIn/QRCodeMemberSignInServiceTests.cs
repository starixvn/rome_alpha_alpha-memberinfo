namespace Alpha.MemberInfo.Tests.Services.MemberRequest
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.CopyAccount.Models;
    using Alpha.CopyAccount.Params;
    using Alpha.CopyAccount.Services;
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn;
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Criteria;
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Models;
    using Alpha.MemberInfo.Tests.Extensions;
    using Fanex.Data.Repository;
    using Fixtures;
    using KellermanSoftware.CompareNetObjects;
    using NSubstitute;
    using Xunit;

    [Trait("Category", "QRCodeMemberSignIn")]
    [Collection("AnyValueCreatorCollection")]
    public class QRCodeMemberSignInServiceTests
    {
        private readonly QRCodeMemberSignInServiceImpl serviceImpl;
        private readonly CompareLogic compareLogic;
        private readonly AnyValueCreatorFixture anyValueCreatorFixture;
        private readonly IDynamicRepository repository;
        private readonly ICopyAccountService copyAccountService;

        public QRCodeMemberSignInServiceTests(AnyValueCreatorFixture anyValueCreatorFixture)
        {
            this.anyValueCreatorFixture = anyValueCreatorFixture;

            repository = Substitute.For<IDynamicRepository>();
            copyAccountService = Substitute.For<ICopyAccountService>();
            compareLogic = new CompareLogic();

            serviceImpl = new QRCodeMemberSignInServiceImpl(repository, copyAccountService);
        }

        [Fact]
        public void GetCreditRange_Always_ReturnsCorrectModel()
        {
            var model = new CreditAndStatusModel(100, 100, 1000, "open");

            repository
                .Get<CreditAndStatusModel>(Arg.Any<SelectCreditAndStatusCriteria>())
                .Returns(model);

            var actualModel = serviceImpl.GetCreditAndStatus("user Test");

            AssertExtension.Equal(actualModel, model);
        }

        [Fact]
        public void CreateMemberByQRCode_Always_ReturnsCorrecResult()
        {
            var result = new ResultCreateMemberQRCode();

            copyAccountService.CopyAsync(Arg.Any<CopyAccountParams>()).Returns(Task.FromResult(new CopyAccountResult() { CustId = result.CustId }));

            var actualResult = serviceImpl.CreateMemberByQRCode(
                anyValueCreatorFixture.Create<InsertMemberCommand>()).Result;

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

        [Fact]
        public void CheckLimitNewDownline_Always_ReturnsCorrectResult()
        {
            // Arrange
            copyAccountService
                .CheckLimitNewDownline(Arg.Any<CheckLimitNewDownlineParam>())
                .Returns(new CopyAccountResult { Code = 1001, Message = "Limit new downline" });

            // Act
            var actualResult = serviceImpl.CheckLimitNewDownline(1, 4, "es-US", null);

            // Assert
            Assert.True(actualResult.Code == 1001 && actualResult.Message == "Limit new downline");
        }
    }
}
