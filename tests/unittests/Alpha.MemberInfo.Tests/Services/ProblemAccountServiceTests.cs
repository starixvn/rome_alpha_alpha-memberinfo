using System;
using System.Collections.Generic;
using System.Linq;
using Alpha.MemberInfo.Constants;
using Alpha.MemberInfo.Criteria.ProblemAccount;
using Alpha.MemberInfo.Models.ProblemAccount;
using Alpha.MemberInfo.Params;
using Alpha.MemberInfo.Params.ProblemAccount;
using Alpha.MemberInfo.Services;
using Fanex.Data.Repository;
using NSubstitute;
using Xunit;

namespace Alpha.MemberInfo.Tests.Services
{
    [Trait("Category", "ProblemAccount")]
    public class ProblemAccountServiceTests
    {
        private readonly IProblemAccountService service;
        private readonly IDynamicRepository repository;

        public ProblemAccountServiceTests()
        {
            repository = Substitute.For<IDynamicRepository>();
            service = new ProblemAccountService(repository);
        }

        [Theory]
        [InlineData(1, "errrrorrr")]
        [InlineData(0, "Successfully updated!")]
        public void UpdateReviewStatus_Always_BuildCorrectResult(int errCode, string errMessage)
        {
            // Arrange
            repository.When(x => x.Execute(Arg.Any<UpdateReviewStatusProblemAccountCriteria>()))
                .Do(
                    Callback.First(x => ((UpdateReviewStatusProblemAccountCriteria)x.Args()[0]).ErrCode = errCode)
                    .AndAlways(x => ((UpdateReviewStatusProblemAccountCriteria)x.Args()[0]).ErrMsg = errMessage));

            // Act
            var act = service.UpdateReviewStatus(new UpdateProblemAccountParam());

            // Assert
            Assert.Equal(errCode, act.Code);
            Assert.Equal(errMessage, act.Message);
        }

        [Fact]
        public void Get_Always_OrderReturn()
        {
            // Arrange
            var accounts = new List<ProblemAccountModel>
            {
                new ProblemAccountModel{ CustId = 1, CreatedDate = DateTime.Now, IsReviewed = true},
                new ProblemAccountModel{ CustId = 2, CreatedDate = new DateTime(2020,2,25), IsReviewed = false},
                new ProblemAccountModel{ CustId = 3, CreatedDate = new DateTime(2020,2,20), IsReviewed = false},
            };

            repository.Fetch<ProblemAccountModel>(Arg.Any<GetProblemAccountCriteria>())
                .Returns(accounts);

            // Act
            var act = service.Get(new GetProblemAccountParam()).ToList();

            // Assert
            Assert.Equal(2, act[0].CustId);
            Assert.Equal(3, act[1].CustId);
            Assert.Equal(1, act[2].CustId);
        }

        [Fact]
        public void ProblemAccountModel_Properties_HasDefaultValues()
        {
            var act = new ProblemAccountModel();
            Assert.Null(act.UserName);
            Assert.Null(act.FirstName);
            Assert.Null(act.LastName);
            Assert.True(act.Count == 0);
            Assert.True(act.ClosedReason == 0);
            Assert.False(act.IsReviewed);
            Assert.True(act.CanEdit);
            Assert.True(act.ClosedReason == 0);
            Assert.True(act.ClosedReasonName == string.Empty);
            Assert.True(act.StatusNameEN == string.Empty);
        }
              
    }
}
