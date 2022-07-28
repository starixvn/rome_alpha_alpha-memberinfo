namespace Alpha.MemberInfo.Site.Features.MyMonitorAccounts.MyWatchlistAccounts
{
    using Alpha.MemberInfo.Features.MyMonitorAccounts.MyWatchlistAccounts;
    using Alpha.MemberInfo.Features.MyMonitorAccounts.MyWatchlistAccounts.Models;
    using Alpha.MemberInfo.Features.MyMonitorAccounts.MyWatchlistAccounts.Params;
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Site.Features.MyMonitorAccounts.MyWatchlistAccounts.Requests;
    using Alpha.MemberInfo.Site.Features.MyMonitorAccounts.MyWatchlistAccounts.ViewModels;
    using Alpha.Web.Helpers;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class MyWatchlistAccountsHandler :
        IAsyncRequestHandler<MyWatchlistAccountsQuery, MyWatchlistAccountsViewModel>,
        IAsyncRequestHandler<WatchlistAccountsQuery, MyWatchlistAccountsPagingViewModel>,
        IAsyncRequestHandler<PersonalPresetQuery, IEnumerable<PersonalPresetModel>>,
        IAsyncRequestHandler<AddPersonalThresholdCommand, IEnumerable<NamedResult>>,
        IAsyncRequestHandler<UpdateMultiplePersonalThresholdCommand, IEnumerable<NamedResult>>,
        IAsyncRequestHandler<DeleteMultiplePersonalThresholdCommand, IEnumerable<NamedResult>>,
        IAsyncRequestHandler<UpdateGlobalThresholdCommand, Result>,
        IAsyncRequestHandler<AddMultiplePersonalPresetCommand, IEnumerable<NamedResult>>,
        IAsyncRequestHandler<UpdatePersonalPresetCommand, Result>,
        IAsyncRequestHandler<DeletePersonalPresetCommand, Result>,
        IAsyncRequestHandler<ManageWatchlistAccountsQuery, ManageWatchlistAccountsViewModel>,
        IAsyncRequestHandler<ManageGlobalThresholdQuery, ManageGlobalThresholdViewModel>,
        IAsyncRequestHandler<ManagePersonalThresholdPresetQuery, ManagePersonalThresholdPresetViewModel>
    {
        private readonly MyWatchlistAccountsService service;
        private readonly BrowserHelper browserHelper;

        public MyWatchlistAccountsHandler(
            MyWatchlistAccountsService service,
            BrowserHelper browserHelper)
        {
            this.service = service;
            this.browserHelper = browserHelper;
        }

        public async Task<MyWatchlistAccountsViewModel> Handle(MyWatchlistAccountsQuery message)
        {
            var isMobile = browserHelper.IsMobile(isOnlyTablet: false);
            var canUpdateAccount = message.CanUpdateAccount;
            var query = new WatchlistAccountsQuery(message.Viewer)
            {
                Username = message.Username,
                SortType = message.SortType,
                IsDesc = message.IsDesc,
                PageIndex = message.PageIndex,
                PageSize = message.PageSize
            };
            var pagingViewModel = await Handle(query);

            var param = new SelectGlobalThresholdParam(message.CustId);
            var globalThreshold = await service.GetGlobalThreshold(param);            

            return new MyWatchlistAccountsViewModel(
                message, 
                globalThreshold,
                isMobile,
                canUpdateAccount,
                pagingViewModel);
        }

        public async Task<MyWatchlistAccountsPagingViewModel> Handle(WatchlistAccountsQuery message)
        {
            var param = new SelectWatchlistAccountsParam(
               message.CustId,
               message.Username,
               message.SortType,
               message.IsDesc,
               message.PageIndex,
               message.PageSize);

            var watchlistAccountDTO = await service.GetWatchlistAccounts(param);

            return new MyWatchlistAccountsPagingViewModel(watchlistAccountDTO, message);
        }

        public async Task<IEnumerable<PersonalPresetModel>> Handle(PersonalPresetQuery message)
        {
            var param = new SelectPresetsParam(message.CustId);

            return await service.GetPresets(param);
        }

        public async Task<IEnumerable<NamedResult>> Handle(AddPersonalThresholdCommand message)
        {
            var param = new AddPersonalThresholdParam(
                message.Usernames,
                message.CustId,
                message.WinningThreshold,
                message.LosingThreshold,
                message.UserId,
                message.SubAccId,
                message.IP);

            return await service.AddPersonalThreshold(param);
        }

        public async Task<IEnumerable<NamedResult>> Handle(UpdateMultiplePersonalThresholdCommand message)
        {
            var tasks = new List<Task<NamedResult>>();

            foreach (var user in message.Users)
            {
                var param = new UpdatePersonalThresholdParam(
                            user.CustId,
                            user.Username,
                            message.WinningThreshold,
                            message.LosingThreshold,
                            message.UserId,
                            message.SubAccId,
                            message.IP);

                tasks.Add(service.UpdatePersonalThreshold(param));
            }

            return await Task.WhenAll(tasks);
        }

        public async Task<IEnumerable<NamedResult>> Handle(DeleteMultiplePersonalThresholdCommand message)
        {
            var tasks = new List<Task<NamedResult>>();

            foreach (var user in message.Users)
            {
                var param = new DeletePersonalThresholdParam(
                            user.CustId,
                            user.Username,
                            message.UserId,
                            message.SubAccId,
                            message.IP);

                tasks.Add(service.DeletePersonalThreshold(param));
            }

            return await Task.WhenAll(tasks);
        }

        public async Task<Result> Handle(UpdateGlobalThresholdCommand message)
        {
            var param = new UpdateGlobalThresholdParam(
                message.CustId,
                message.WinningThreshold,
                message.LosingThreshold,
                message.UserId,
                message.SubAccId,
                message.IP);

            return await service.UpdateGlobalThreshold(param);
        }

        public async Task<IEnumerable<NamedResult>> Handle(AddMultiplePersonalPresetCommand message)
        {
            var results = new List<NamedResult>();

            foreach(var preset in message.Presets)
            {
                var param = new AddPersonalPresetParam(
                    message.CustId,
                    preset.PresetTitle,
                    preset.WinningThreshold,
                    preset.LosingThreshold);

                results.Add(await service.AddPersonalPreset(param));
            }

            return results;
        }

        public async Task<Result> Handle(UpdatePersonalPresetCommand message)
        {
            var param = new UpdatePersonalPresetParam(
                message.ID,
                message.CustId,
                message.PresetTitle,
                message.WinningThreshold,
                message.LosingThreshold);

            return await service.UpdatePersonalPreset(param);
        }

        public async Task<Result> Handle(DeletePersonalPresetCommand message)
        {
            var param = new DeletePersonalPresetParam(
                message.ID,
                message.CustId);

            return await service.DeletePersonalPresetParam(param);
        }

        public async Task<ManageWatchlistAccountsViewModel> Handle(ManageWatchlistAccountsQuery message)
        {
            var personalPresets = await service.GetPresets(new SelectPresetsParam(message.CustId));
            return new ManageWatchlistAccountsViewModel(message, personalPresets);
        }

        public async Task<ManageGlobalThresholdViewModel> Handle(ManageGlobalThresholdQuery message)
        {
            var param = new SelectGlobalThresholdParam(message.CustId);
            var globalThreshold = await service.GetGlobalThreshold(param);

            return new ManageGlobalThresholdViewModel(globalThreshold);
        }

        public async Task<ManagePersonalThresholdPresetViewModel> Handle(ManagePersonalThresholdPresetQuery message)
        {
            var param = new SelectPresetsParam(message.CustId);
            var personalPresets = await service.GetPresets(param);

            return new ManagePersonalThresholdPresetViewModel(personalPresets);
        }
    }
}