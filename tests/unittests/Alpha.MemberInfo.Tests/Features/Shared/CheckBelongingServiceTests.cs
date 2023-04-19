namespace Alpha.MemberInfo.Tests.Services
{
    using Alpha.MemberInfo.Features.Shared;
    using Alpha.MemberInfo.Features.Shared.Criteria;
    using Alpha.MemberInfo.Features.Shared.Models;
    using Alpha.MemberInfo.Features.Shared.Params;
    using Alpha.MemberInfo.Tests.Extensions;
    using Alpha.MemberInfo.Tests.Fixtures;
    using Fanex.Data.Repository;
    using KellermanSoftware.CompareNetObjects;
    using NSubstitute;
    using NSubstitute.Extensions;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Xunit;

    [Trait("Category", "CustomerList")]
    [Collection("AnyValueCreatorCollection")]
    public class CheckBelongingServiceTests
    {
        private readonly IDynamicRepository repository;
        private readonly CheckBelongingServiceImpl service;
        private readonly AnyValueCreatorFixture anyValueCreatorFixture;
        private readonly CompareLogic compareLogic = new CompareLogic();

        public CheckBelongingServiceTests(AnyValueCreatorFixture anyValueCreatorFixture)
        {
            this.anyValueCreatorFixture = anyValueCreatorFixture;
            repository = Substitute.For<IDynamicRepository>();
            service = new CheckBelongingServiceImpl(repository);
        }

        [Fact]
        public async void GetBelongingInfoAsync_Always_ReturnsCorrectModel()
        {

            var model = new BelongingInfo(true, 1, "Username");
            repository.ExecuteAsync(Arg.Any<CheckBelongingInDBCustCommand>()).Returns(x =>
            {
                ((CheckBelongingInDBCustCommand)x[0]).IsActive = true;
                ((CheckBelongingInDBCustCommand)x[0]).RoleId = 1;
                ((CheckBelongingInDBCustCommand)x[0]).Username = "Username";
                return Task.CompletedTask;
            });
            var param = new CheckBelongingParam(1, 1, 1, 1);

            var actualModel = await service.GetBelongingInfoAsync(param);

            AssertExtension.Equal(actualModel, model);
        }

        [Fact]
        public void GetBelongingInfo_Always_ReturnsCorrectModel()
        {
            var model = new BelongingInfo(true, 1, "Username");
            repository.When(r => r.Execute(Arg.Any<CheckBelongingInDBCustCommand>()))
                .Do(x =>
                {
                    ((CheckBelongingInDBCustCommand)x[0]).IsActive = true;
                    ((CheckBelongingInDBCustCommand)x[0]).RoleId = 1;
                    ((CheckBelongingInDBCustCommand)x[0]).Username = "Username";
                });
            var param = new CheckBelongingParam(1, 1, 1, 1);

            var actualModel = service.GetBelongingInfo(param);

            AssertExtension.Equal(actualModel, model);

        }
    }
}
