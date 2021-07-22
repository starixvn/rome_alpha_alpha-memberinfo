namespace Alpha.MemberInfo.Features.Shared
{
    using System.Threading.Tasks;
    using Alpha.MemberInfo.Features.Shared.Criteria;
    using Alpha.MemberInfo.Features.Shared.Models;
    using Alpha.MemberInfo.Features.Shared.Params;
    using Fanex.Data.Repository;

    public interface CheckBelongingService
    {
        BelongingInfo GetBelongingInfo(CheckBelongingParam param);

        Task<BelongingInfo> GetBelongingInfoAsync(CheckBelongingParam param);
    }

    public class CheckBelongingServiceImpl : CheckBelongingService
    {
        private readonly IDynamicRepository dynamicRepository;

        public CheckBelongingServiceImpl(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public BelongingInfo GetBelongingInfo(CheckBelongingParam param)
        {
            var command = new CheckBelongingInDBCustCommand(param);
            dynamicRepository.Execute(command);

            return new BelongingInfo(command.IsActive, command.RoleId, command.Username);
        }

        public async Task<BelongingInfo> GetBelongingInfoAsync(CheckBelongingParam param)
        {
            var command = new CheckBelongingInDBCustCommand(param);
            await dynamicRepository.ExecuteAsync(command);

            return new BelongingInfo(command.IsActive, command.RoleId, command.Username);
        }
    }
}
