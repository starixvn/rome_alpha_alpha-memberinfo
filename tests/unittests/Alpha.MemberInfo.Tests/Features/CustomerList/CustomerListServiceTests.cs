namespace Alpha.MemberInfo.Tests.Services
{
    using Alpha.MemberInfo.Enumerations;
    using Alpha.MemberInfo.Features.CustomerList;
    using Alpha.MemberInfo.Features.CustomerList.Criteria;
    using Alpha.MemberInfo.Features.CustomerList.Models;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.CustomerList.Params;
    using Alpha.MemberInfo.Site.Features.CustomerList.ViewModels;
    using Alpha.MemberInfo.Tests.Fixtures;
    using Fanex.Data.Repository;
    using KellermanSoftware.CompareNetObjects;
    using NSubstitute;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    [Trait("Category", "CustomerList")]
    [Collection("AnyValueCreatorCollection")]
    public class CustomerListServiceTests
    {
        private readonly IDynamicRepository repository;
        private readonly CustomerListServiceImpl service;
        private readonly CustomerListDataAccess dataAccess;
        private readonly IDictionary<UserLevel, Func<CustomerListParam, CustomerListDTO>> fetchData;
        private readonly AnyValueCreatorFixture anyValueCreatorFixture;
        private readonly CompareLogic compareLogic = new CompareLogic();

        public CustomerListServiceTests(AnyValueCreatorFixture anyValueCreatorFixture)
        {
            this.anyValueCreatorFixture = anyValueCreatorFixture;
            fetchData = new Dictionary<UserLevel, Func<CustomerListParam, CustomerListDTO>>
            {
                {UserLevel.Super, FectchMasterList},
                {UserLevel.Master, FetchAgentList},
                {UserLevel.Agent, FetchMemberList}
            };

            repository = Substitute.For<IDynamicRepository>();
            dataAccess = Substitute.For<CustomerListDataAccess>();
            service = new CustomerListServiceImpl(repository, dataAccess);
        }

        [Fact]
        public void GetUnsyncedProductIds_Always_OrderReturn()
        {
            // Arrange
            var expectResult = new List<int> { 1, 2, 3 };

            repository.Fetch<int>(Arg.Any<SelectUnsyncProductsCriteria>()).Returns(expectResult);

            // Act
            var actualResulf = service.GetUnsyncedProductIds(1).ToList();

            // Assert
            Assert.True(compareLogic.Compare(actualResulf, expectResult).AreEqual);
        }

        [Fact]
        public void FectchMasterList_Always_ReturnsCorrectModel()
        {
            var param = BuildCustomerParam(4);
            var downlinesList = anyValueCreatorFixture.Create<IEnumerable<MasterListModel>>();
            var totalDownlines = 100;
            var totalPages = 10;
            var expectedModel = new CustomerListDTO(downlinesList, totalDownlines, totalPages, param);

            repository
                .Fetch<MasterListModel>(Arg.Any<SelectMasterListCriteria>())
                .Returns(downlinesList);

            repository
               .When(repo => repo.Fetch<MasterListModel>(Arg.Any<SelectMasterListCriteria>()))
               .Do((callInfo) =>
               {
                   var query = callInfo[0] as SelectMasterListCriteria;
                   query.TotalItems = totalDownlines;
                   query.TotalPages = totalPages;
               });


            var actualModel = service.FectchMasterList(param);

            Assert.True(compareLogic.Compare(actualModel, expectedModel).AreEqual);
        }

        [Fact]
        public void FetchAgentList_Always_ReturnsCorrectModel()
        {
            var param = BuildCustomerParam(3);
            var downlinesList = anyValueCreatorFixture.Create<IEnumerable<AgentListModel>>();
            var totalDownlines = 100;
            var totalPages = 10;
            var expectedModel = new CustomerListDTO(downlinesList, totalDownlines, totalPages, param);

            repository
                .Fetch<AgentListModel>(Arg.Any<SelectAgentListCriteria>())
                .Returns(downlinesList);

            repository
             .When(repo => repo.Fetch<AgentListModel>(Arg.Any<SelectAgentListCriteria>()))
             .Do((callInfo) =>
             {
                 var query = callInfo[0] as SelectAgentListCriteria;
                 query.TotalItems = totalDownlines;
                 query.TotalPages = totalPages;
             });

            var actualModel = service.FetchAgentList(param);

            Assert.True(compareLogic.Compare(actualModel, expectedModel).AreEqual);
        }

        [Fact]
        public void FetchMemberList_Always_ReturnsCorrectModel()
        {
            var param = BuildCustomerParam(2);
            var downlinesList = anyValueCreatorFixture.Create<IEnumerable<MemberListModel>>();
            var totalDownlines = 100;
            var totalPages = 10;
            var expectedModel = new CustomerListDTO(downlinesList, totalDownlines, totalPages, param);

            repository
                .Fetch<MemberListModel>(Arg.Any<SelectMemberListCriteria>())
                .Returns(downlinesList);

            repository
            .When(repo => repo.Fetch<MemberListModel>(Arg.Any<SelectMemberListCriteria>()))
            .Do((callInfo) =>
            {
                var query = callInfo[0] as SelectMemberListCriteria;
                query.TotalItems = totalDownlines;
                query.TotalPages = totalPages;
            });

            var actualModel = service.FetchMemberList(BuildCustomerParam(2));

            Assert.True(compareLogic.Compare(actualModel, expectedModel).AreEqual);
        }

        [Fact]
        public void GetCustomerStatus_Always_ReturnsCorrectModel()
        {
            var data = anyValueCreatorFixture.Create<CustomerStatusModel>();

            dataAccess
                .GetCustomerStatus(Arg.Any<SelectCustomerAllStatusModelCriteria>())
                .Returns(data);

            var actualModel = service.GetCustomerStatus(2);

            Assert.True(compareLogic.Compare(actualModel, data).AreEqual);
        }

        [Fact]
        public void UpdateCustomerStatus_Always_ReturnsCorrectModel()
        {
            var result = new Result() { Code = 0, Message = string.Empty };

            dataAccess
                .UpdateCustomerStatus(Arg.Any<UpdateCustomerStatusCommand>())
                .Returns(result);

            var actualModel = service.UpdateCustomerStatus(anyValueCreatorFixture.Create<UpdateCustomerStatusParam>());

            Assert.True(compareLogic.Compare(actualModel, result).AreEqual);
        }

        [Fact]
        public void GetSyncedCasinoModel_Always_ReturnsCorrectModel()
        {
            var data = anyValueCreatorFixture.Create<IEnumerable<SyncedCasinoModel>>();

            repository
                 .Fetch<SyncedCasinoModel>(Arg.Any<SelectSyncedCasinoList>())
                 .Returns(data);

            var actualModel = service.GetSyncedCasinoModel("1,2");

            Assert.True(compareLogic.Compare(actualModel, data).AreEqual);
        }

        private CustomerListDTO FectchMasterList(CustomerListParam param) => anyValueCreatorFixture.Create<CustomerListDTO>();

        private CustomerListDTO FetchAgentList(CustomerListParam param) => anyValueCreatorFixture.Create<CustomerListDTO>();

        private CustomerListDTO FetchMemberList(CustomerListParam param) => anyValueCreatorFixture.Create<CustomerListDTO>();

        private CustomerListParam BuildCustomerParam(byte roleId)
            => new CustomerListParam(
                8,
                Enumeration.FromValue<UserLevel>(roleId),
                12,
                "Test",
                true,
                true,
                true,
                2,
                2,
                50,
                1,
                false,
                false,
                true);
    }
}
