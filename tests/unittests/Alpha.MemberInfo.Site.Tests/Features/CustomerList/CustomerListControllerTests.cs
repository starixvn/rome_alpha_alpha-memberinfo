namespace Alpha.MemberInfo.Site.Tests.Controllers
{
    using System.Threading.Tasks;
    using Alpha.MemberInfo.Services.Transfer;
    using Alpha.MemberInfo.Site.Features.CustomerList;
    using Alpha.MemberInfo.Site.Features.CustomerList.Requests;
    using MediatR;
    using MemberInfo.Models;
    using NSubstitute;
    using Xunit;

    [Trait("Category", "CustomerList")]
    public class CustomerListControllerTests
    {
        private readonly IMediator mediator;
        private readonly IUser user;
        private readonly CustomerListController controller;
        private readonly TransferService transferService;

        public CustomerListControllerTests()
        {
            mediator = Substitute.For<IMediator>();
            transferService = Substitute.For<TransferService>();

            user = Substitute.For<IUser>();
            user.LoggedInUserId.Returns(1176070);
            user.SubAccId.Returns(536751);
            user.Id.Returns(1176070);
            user.LevelId.Returns((byte)2);
            user.Name.Returns("UserTest");
            user.Language.Returns("En");
            user.IsInternal.Returns(false);
            user.CanUpdateAccount.Returns(true);

            controller = new CustomerListController(mediator, transferService, null);
        }

        [Fact]
        public void Index_Always_CallsMediatorCustomerListRequest()
        {
            // Arrange
            var message = new CustomerListRequest(user);

            // Act
            controller.Index(message);

            // Assert
            mediator.Received().Send(message);
        }

        [Fact]
        public void GetCustomerListData_Always_CallsMediatorFetchDownlinesListRequest()
        {
            // Arrange
            var message = new FetchDownlinesListRequest(user);

            // Act
            controller.GetCustomerListData(message);

            // Assert
            mediator.Received().Send(message);
        }

        [Fact]
        public async Task MultipleEditPopup_Always_CallsMediatorCustomerStatusMultipleQuery()
        {
            // Arrange
            var message = new CustomerStatusMultipleQuery(user)
            {
                CustIds = "1,2",
                Usernames = "username1, username 2",
                UplineRoleId = 2,
                UplineName = "Upline Name",
                SearchAccountRoleId = 1
            };

            // Act
            await controller.MultipleEditPopup(message);

            // Assert
            await mediator.Received().SendAsync(message);
        }

        [Fact]
        public void CustomerStatusCommand_ValidViewModel_ReturnsView()
        {
            var message = new CustomerNonProductStatusCommand(user)
            {
                SearchAccountId = 2,
                StatusName = "outright",
                StatusValue = true
            };

            controller.UpdateCustomerStatus(message);

            mediator.Received().Send(message);
        }
    }
}