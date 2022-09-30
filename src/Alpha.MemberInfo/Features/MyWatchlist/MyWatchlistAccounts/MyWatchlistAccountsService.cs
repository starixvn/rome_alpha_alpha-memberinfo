namespace Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Criteria;
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Models;
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Params;
    using Alpha.MemberInfo.Models;
    using Alpha.PartialUM.Services;
    using Fanex.Data.Repository;

    public interface MyWatchlistAccountsService
    {
        Task<WatchlistAccountReport> GetWatchlistAccounts(SelectWatchlistAccountsParam param);

        Task<IEnumerable<PersonalPresetModel>> GetPresets(SelectPresetsParam param);

        Task<IEnumerable<NamedResult>> AddPersonalThreshold(AddPersonalThresholdParam param);

        Task<NamedResult> UpdatePersonalThreshold(UpdatePersonalThresholdParam param);

        Task<NamedResult> DeletePersonalThreshold(DeletePersonalThresholdParam param);

        Task<GlobalThresholdModel> GetGlobalThreshold(SelectGlobalThresholdParam param);

        Task<Result> UpdateGlobalThreshold(UpdateGlobalThresholdParam param);

        Task<NamedResult> AddPersonalPreset(AddPersonalPresetParam param);

        Task<Result> UpdatePersonalPreset(UpdatePersonalPresetParam param);

        Task<Result> DeletePersonalPreset(DeletePersonalPresetParam param);
    }

    public class MyWatchlistAccountsServiceImpl : MyWatchlistAccountsService
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly IPartialUMService partialUMService;

        public MyWatchlistAccountsServiceImpl(
            IDynamicRepository dynamicRepository,
            IPartialUMService partialUMService)
        {
            this.dynamicRepository = dynamicRepository;
            this.partialUMService = partialUMService;
        }

        public async Task<WatchlistAccountReport> GetWatchlistAccounts(SelectWatchlistAccountsParam param)
        {
            var criteria = new SelectWatchlistAccountsCriteria(
                param.AgentId, 
                param.Username, 
                param.SortType, 
                param.IsDesc,
                param.PageIndex,
                param.PageSize);
            partialUMService.ThrowIfUM(criteria.GetSettingKey());

            var watchlistAccounts = await dynamicRepository.FetchAsync<WatchlistAccountModel>(criteria);

            return new WatchlistAccountReport(watchlistAccounts, criteria.TotalItems);
        }

        public async Task<IEnumerable<PersonalPresetModel>> GetPresets(SelectPresetsParam param)
        {
            var criteria = new SelectPresetsCriteria(param.AgentId);
            partialUMService.ThrowIfUM(criteria.GetSettingKey());

            return await dynamicRepository.FetchAsync<PersonalPresetModel>(criteria);
        }

        public async Task<IEnumerable<NamedResult>> AddPersonalThreshold(AddPersonalThresholdParam param)
        {
            var criteria = new InsertMultiplePersonalThresholdCriteria(
                param.Usernames,
                param.AgentId,
                param.WinningThreshold,
                param.LosingThreshold,
                param.UserId,
                param.SubAccId,
                param.IP);
            partialUMService.ThrowIfUM(criteria.GetSettingKey());

            return await dynamicRepository.FetchAsync<NamedResult>(criteria);
        }

        public async Task<NamedResult> UpdatePersonalThreshold(UpdatePersonalThresholdParam param)
        {
            var criteria = new UpdatePersonalThresholdCommand(
                param.CustId,
                param.WinningThreshold,
                param.LosingThreshold,
                param.UserId,
                param.SubAccId,
                param.IP);
            partialUMService.ThrowIfUM(criteria.GetSettingKey());

            await dynamicRepository.ExecuteAsync(criteria);

            return new NamedResult { Name = param.Username, Code = criteria.ErrCode, Message = criteria.ErrMsg };
        }

        public async Task<NamedResult> DeletePersonalThreshold(DeletePersonalThresholdParam param)
        {
            var command = new DeletePersonalThresholdCommand(
               param.CustId,
               param.UserId,
               param.SubAccId,
               param.IP);
            partialUMService.ThrowIfUM(command.GetSettingKey());

            await dynamicRepository.ExecuteAsync(command);

            return new NamedResult { Name = param.Username, Code = command.ErrCode, Message = command.ErrMsg };
        }

        public async Task<GlobalThresholdModel> GetGlobalThreshold(SelectGlobalThresholdParam param)
        {
            var criteria = new SelectGlobalThresholdCriteria(param.AgentId);
            partialUMService.ThrowIfUM(criteria.GetSettingKey());

            return await dynamicRepository.GetAsync<GlobalThresholdModel>(criteria);
        }

        public async Task<Result> UpdateGlobalThreshold(UpdateGlobalThresholdParam param)
        {
            var command = new UpdateGlobalThresholdCommand(
                param.AgentId,
                param.WinningThreshold,
                param.LosingThreshold,
                param.UserId,
                param.SubAccId,
                param.IP);
            partialUMService.ThrowIfUM(command.GetSettingKey());

            await dynamicRepository.ExecuteAsync(command);

            return new Result { Code = command.ErrCode, Message = command.ErrMsg };
        }

        public async Task<NamedResult> AddPersonalPreset(AddPersonalPresetParam param)
        {
            var command = new AddPersonalPresetCommand(
                param.AgentId,
                param.PresetTitle,
                param.WinningThreshold,
                param.LosingThreshold);
            partialUMService.ThrowIfUM(command.GetSettingKey());

            await dynamicRepository.ExecuteAsync(command);

            return new NamedResult { Name = param.PresetTitle, Code = command.ErrCode, Message = command.ErrMsg };
        }

        public async Task<Result> UpdatePersonalPreset(UpdatePersonalPresetParam param)
        {
            var command = new UpdatePersonalPresetCommand(
                param.AgentId,
                param.ID,
                param.PresetTitle,
                param.WinningThreshold,
                param.LosingThreshold);

            await dynamicRepository.ExecuteAsync(command);

            return new Result { Code = command.ErrCode, Message = command.ErrMsg };
        }

        public async Task<Result> DeletePersonalPreset(DeletePersonalPresetParam param)
        {
            var command = new DeletePersonalPresetCommand(
                param.AgentId,
                param.ID);
            partialUMService.ThrowIfUM(command.GetSettingKey());

            await dynamicRepository.ExecuteAsync(command);

            return new Result { Code = command.ErrCode, Message = command.ErrMsg };
        }
    }
}
