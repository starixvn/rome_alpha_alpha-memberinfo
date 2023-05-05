namespace Alpha.MemberInfo.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Alpha.MemberInfo.AppResources;
    using Alpha.MemberInfo.Criteria.ProblemAccount;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Models.ProblemAccount;
    using Alpha.MemberInfo.Params.ProblemAccount;
    using Fanex.Data.Repository;

    public interface IProblemAccountService
    {
        Result UpdateReviewStatus(UpdateProblemAccountParam param);

        IEnumerable<ProblemAccountModel> Get(GetProblemAccountParam param);
    }

    public class ProblemAccountService : IProblemAccountService
    {
        private readonly IDynamicRepository dynamicRepository;

        public ProblemAccountService(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public Result UpdateReviewStatus(UpdateProblemAccountParam param)
        {
            var command = new UpdateReviewStatusProblemAccountCriteria(param);
            dynamicRepository.Execute(command);

            return Result.CreateErrorResult(command.ErrCode == 0 ? Resources.UpdateSuccessfully : command.ErrMsg, command.ErrCode);
        }

        public IEnumerable<ProblemAccountModel> Get(GetProblemAccountParam param)
        //=> dynamicRepository.Fetch<ProblemAccountModel>(new GetProblemAccountCriteria(param))
        //.OrderBy(i => i.IsReviewed)
        //.ThenByDescending(i => i.CreatedDate);
        {
            var test = dynamicRepository.Fetch<ProblemAccountModel>(new GetProblemAccountCriteria(param))
        .OrderBy(i => i.IsReviewed)
        .ThenByDescending(i => i.CreatedDate);
            var a = new ProblemAccountModel()
            {
                Username2 = "Test1",
                CustId = 18363561,
                IsReviewed = false,
                Status = 1,
            };


            var b = new ProblemAccountModel()
            {
                Username2 = "Test2",
                CustId = 18363711,
                IsReviewed = false,
                Status = 1,
            };

            var c = new ProblemAccountModel()
            {
                Username2 = "Test2",
                CustId = 1176092,
                IsReviewed = false,
                Status = 1,
            };

            a.IsReviewed = false;

            var result = new List<ProblemAccountModel>() {
                   a,b,c
                };

            return result.AsEnumerable();

        }

    }
}
