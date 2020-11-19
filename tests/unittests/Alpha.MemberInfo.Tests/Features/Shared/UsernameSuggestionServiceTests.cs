namespace Alpha.MemberInfo.Tests.Services
{
    using Alpha.MemberInfo.Features.Shared;
    using Alpha.MemberInfo.Features.Shared.Criteria;
    using Alpha.MemberInfo.Features.Shared.Models;
    using Alpha.MemberInfo.Tests.Extensions;
    using Alpha.MemberInfo.Tests.Fixtures;
    using Fanex.Data.Repository;
    using KellermanSoftware.CompareNetObjects;
    using NSubstitute;
    using System.Collections.Generic;
    using Xunit;

    [Trait("Category", "CustomerList")]
    [Collection("AnyValueCreatorCollection")]
    public class UsernameSuggestionServiceTests
    {
        private readonly IDynamicRepository repository;
        private readonly UsernameSuggestionServiceImpl service;
        private readonly AnyValueCreatorFixture anyValueCreatorFixture;
        private readonly CompareLogic compareLogic = new CompareLogic();

        public UsernameSuggestionServiceTests(AnyValueCreatorFixture anyValueCreatorFixture)
        {
            this.anyValueCreatorFixture = anyValueCreatorFixture;
            repository = Substitute.For<IDynamicRepository>();
            service = new UsernameSuggestionServiceImpl(repository);
        }

        [Fact]
        public void GetUsernamesSuggestion_Always_ReturnsCorrectModel()
        {
            var model = new List<UsernameSuggestion>
            {
                new UsernameSuggestion("UserName", "FirstName", "LastName", "UserName2")
            };

            repository.Fetch<UsernameSuggestion>(Arg.Any<SelectAutoSuggestionCriteria>()).Returns(model);

            var actualModel = service.GetUsernamesSuggestion(1, "test", false, true);

            AssertExtension.Equal(actualModel, model);
        }
    }
}
