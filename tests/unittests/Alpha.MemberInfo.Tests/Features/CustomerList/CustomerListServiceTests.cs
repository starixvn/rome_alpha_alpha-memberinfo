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
        public void FectchMasterList_Always_ReturnsCorrectModel()
        {
            var param = BuildCustomerParam(4);
            var downlinesList = FakeMasterList();
            var totalDownlines = 100;
            var totalPages = 10;
            var expectedModel = new CustomerListDTO(downlinesList, totalDownlines, totalPages);

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
            var downlinesList = FakeAgentList();
            var totalDownlines = 100;
            var totalPages = 10;
            var expectedModel = new CustomerListDTO(downlinesList, totalDownlines, totalPages);

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
            var downlinesList = FakeMemberList();
            var totalDownlines = 100;
            var totalPages = 10;
            var expectedModel = new CustomerListDTO(downlinesList, totalDownlines, totalPages);

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

            var actualModel = service.FetchMemberList(param);

            Assert.True(compareLogic.Compare(actualModel, expectedModel).AreEqual);
        }

        [Fact]
        public void GetCustomerStatus_Always_ReturnsCorrectModel()
        {
            var data = anyValueCreatorFixture.Create<CustomerStatusModel>();

            dataAccess
                .GetCustomerStatus(Arg.Any<SelectCustomerAllStatusModelCriteria>())
                .Returns(data);

            var actualModel = service.GetCustomerStatus(2, Arg.Any<byte>());

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


        private CustomerListDTO FectchMasterList(CustomerListParam param) =>
            new CustomerListDTO(FakeMasterList(), 100, 2);

        private CustomerListDTO FetchAgentList(CustomerListParam param) =>
            new CustomerListDTO(FakeAgentList(), 100, 2);

        private CustomerListDTO FetchMemberList(CustomerListParam param) =>
             new CustomerListDTO(FakeMemberList(), 100, 2);

        private CustomerListParam BuildCustomerParam(byte roleId)
            => new CustomerListParam(
                8,
                Enumeration.FromValue<UserLevel>(roleId),
                12,
                "Test",
                true,
                true,
                true,
                false,
                2,
                50,
                1,
                false,
                false,
                true,
                0);

        private IEnumerable<MasterListModel> FakeMasterList() =>
            new List<MasterListModel>
            {
                new MasterListModel(),
                new MasterListModel(
                    1,
                    "master1",
                    3,
                    "a",
                    string.Empty,
                     string.Empty,
                      string.Empty,
                      "1234",
                      string.Empty,
                      string.Empty,
                      20,
                      1,
                      1,
                      new DateTime(2020, 11 , 30),
                      false,
                      true,
                      2,
                      2,
                      true,
                      true,
                      true,
                      true,
                      (decimal)0.05,
                      (decimal)0.05,
                      (decimal)0.05,
                      (decimal)0.05,
                      (decimal)0.05,
                      (decimal)0.05,
                      true,
                      (decimal)0.05,
                      0)
            };

        private IEnumerable<AgentListModel> FakeAgentList() =>
           new List<AgentListModel>
           {
                new AgentListModel(),
                new AgentListModel(
                    1,
                    "agent1",
                    2,
                    "a",
                    string.Empty,
                     string.Empty,
                      string.Empty,
                      "1234",
                      string.Empty,
                      string.Empty,
                      20,
                      1,
                      1,
                      new DateTime(2020, 11 , 30),
                      false,
                      true,
                      2,
                      2,
                      true,
                      true,
                      true,
                      true,
                      (decimal)0.05,
                      (decimal)0.05,
                      (decimal)0.05,
                      (decimal)0.05,
                      (decimal)0.05,
                      (decimal)0.05,
                      (decimal)0.05,
                      0)
           };

        private IEnumerable<MemberListModel> FakeMemberList() =>
           new List<MemberListModel>
           {
                new MemberListModel(),
                new MemberListModel(
                    1,
                    "agent1",
                    2,
                    "a",
                    string.Empty,
                     string.Empty,
                      string.Empty,
                      "1234",
                      string.Empty,
                      string.Empty,
                      20,
                      1,
                      1,
                      new DateTime(2020, 11 , 30),
                      false,
                      true,
                      2,
                      2,
                      true,
                      true,
                      true,
                      true,
                      (decimal)0.05,
                      (decimal)0.05,
                      (decimal)0.05,
                      (decimal)0.05,
                      (decimal)0.05,
                      (decimal)0.05,
                      (decimal)0.05,
                      (decimal)0.05,
                      0)
           };
    }
}
