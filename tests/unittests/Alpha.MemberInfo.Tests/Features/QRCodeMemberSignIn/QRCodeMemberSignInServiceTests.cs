namespace Alpha.MemberInfo.Tests.Services.MemberRequest
{
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn;
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Criteria;
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Models;
    using Alpha.MemberInfo.Tests.Extensions;
    using Fanex.Customer.Client.Services;
    using Fanex.Data.Repository;
    using NSubstitute;
    using Xunit;

    [Trait("Category", "QRCodeMemberSignIn")]
    [Collection("AnyValueCreatorCollection")]
    public class QRCodeMemberSignInServiceTests
    {
        private readonly QRCodeMemberSignInServiceImpl serviceImpl;
        private readonly IDynamicRepository repository;

        private readonly ICustomerService customerService;

        public QRCodeMemberSignInServiceTests()
        {
            repository = Substitute.For<IDynamicRepository>();
            customerService = Substitute.For<ICustomerService>();
            serviceImpl = new QRCodeMemberSignInServiceImpl(repository, customerService);
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
    }
}