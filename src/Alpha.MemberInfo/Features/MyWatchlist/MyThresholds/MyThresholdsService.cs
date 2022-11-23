namespace Alpha.MemberInfo.Features.MyWatchlist.MyThresholds
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Alpha.MemberInfo.AppResources;
    using Alpha.MemberInfo.Features.MyWatchlist.MyThresholds.Criteria;
    using Alpha.MemberInfo.Features.MyWatchlist.MyThresholds.Params;
    using Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Models;
    using Alpha.MemberInfo.Features.MyWatchlist.ReachedThreshold.Models;
    using Alpha.MemberInfo.Models;
    using Alpha.PartialUM.Services;
    using Fanex.Data.Repository;
    using Fanex.Logging.Extensions.Web;

    public interface MyThresholdsService
    {
        Task<IEnumerable<ReachHistoryModel>> GetMyThresholdHistory(SelectMyThresholdHistoryParam param);

        Task<GlobalThresholdModel> GetMyThreshold(int custId, short sportType = 0);

        Task<Result> UpdateMyThreshold(UpdateMyThresholdParam param);
    }

    public class MyThresholdsServiceImpl : MyThresholdsService
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly IPartialUMService partialUMService;

        public MyThresholdsServiceImpl(
            IDynamicRepository dynamicRepository,
            IPartialUMService partialUMService)
        {
            this.dynamicRepository = dynamicRepository;
            this.partialUMService = partialUMService;
        }

        public Task<IEnumerable<ReachHistoryModel>> GetMyThresholdHistory(SelectMyThresholdHistoryParam param)
        {
            var criteria = new SelectMyThresholdHistoryCriteria(param);
            partialUMService.ThrowIfUM(criteria.GetSettingKey());

            return dynamicRepository.FetchAsync<ReachHistoryModel>(criteria);
        }

        public Task<GlobalThresholdModel> GetMyThreshold(int custId, short sportType = 0)
        {
            var criteria = new SelectMyThresholdCriteria(custId, sportType);
            partialUMService.ThrowIfUM(criteria.GetSettingKey());

            return dynamicRepository.GetAsync<GlobalThresholdModel>(criteria);
        }

        public async Task<Result> UpdateMyThreshold(UpdateMyThresholdParam param)
        {
            var command = new UpdateMyThresholdCommand(param);
            partialUMService.ThrowIfUM(command.GetSettingKey());

            await dynamicRepository.ExecuteAsync(command);
            var result = new Result { Code = command.ErrCode, Message = command.ErrMsg };

            if (result.IsSuccessful)
            {
                result.Message = Resources.WatchlistUpdateSuccessfully;

                SyncUpdateMyThreshold(command.CustList);
            }

            return result;
        }

        private void SyncUpdateMyThreshold(string custList)
        {
            if (!string.IsNullOrWhiteSpace(custList))
            {
                const string DB052 = "DB052";
                Task.Run(() => SyncUpdateMyThreshold(custList, DB052)).ConfigureAwait(false);

                const string DB053 = "DB053";
                Task.Run(() => SyncUpdateMyThreshold(custList, DB053)).ConfigureAwait(false);
            }
        }

        private async Task SyncUpdateMyThreshold(string custList, string suffix)
        {
            try
            {
                await dynamicRepository.ExecuteAsync(new SyncUpdateMyThresholdCommand(custList, suffix));
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }
        }
    }
}
