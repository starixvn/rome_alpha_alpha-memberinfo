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

        Task<DownlineInfo> CheckBelongingByUsername(int custId, int subAccountId, string username);
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
            var command = new CheckBelongingInDBCustCommand(
                param.ViewerId, param.CustId, param.SubAccId, param.TransId);
            dynamicRepository.Execute(command);

            return new BelongingInfo(command.IsActive, command.RoleId, command.Username);
        }

        public async Task<BelongingInfo> GetBelongingInfoAsync(CheckBelongingParam param)
        {
            var command = new CheckBelongingInDBCustCommand(
                param.ViewerId, param.CustId, param.SubAccId, param.TransId);
            await dynamicRepository.ExecuteAsync(command);

            return new BelongingInfo(command.IsActive, command.RoleId, command.Username);
        }

        public async Task<DownlineInfo> CheckBelongingByUsername(int custId, int subAccountId, string username)
        {
            var command = new CheckBelongingByUsernameCommand(custId, username, subAccountId);
            await dynamicRepository.ExecuteAsync(command);
            return new DownlineInfo(command.IsActive, command.CustId, command.IsSubAcc, command.RoleId);
        }
    }
}
