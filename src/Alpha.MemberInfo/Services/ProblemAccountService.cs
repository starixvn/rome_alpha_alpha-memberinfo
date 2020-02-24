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
            => dynamicRepository.Fetch<ProblemAccountModel>(new GetProblemAccountCriteria(param))
            .OrderByDescending(i => i.IsReviewed)
            .ThenBy(i => i.Status);
    }
}
