namespace Alpha.MemberInfo.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.Common;
    using Models;
    using Params;
    using Reports.Criteria.MemberRequest;
    using Reports.DataAccesses;
    using Sunplus.Agent.Model.BL.Casino;
    using Sunplus.Agent.Model.Casino;

    public interface MemberRequestService
    {
        IEnumerable<MemberRequestModel> GetMemberRequest(MemberRequestsParam param);

        IEnumerable<Result> EnableProduct(EnableProductParam param);

        IEnumerable<Result> EnableCasino(EnableCasinoParam param);
    }

    public class MemberRequestServiceImpl : MemberRequestService
    {
        private readonly MemberRequestDataAccess dataAccess;
        private readonly CasinoService casinoService;

        public MemberRequestServiceImpl(
            MemberRequestDataAccess dataAccess,
            CasinoService casinoService)
        {
            this.dataAccess = dataAccess;
            this.casinoService = casinoService;
        }

        public IEnumerable<MemberRequestModel> GetMemberRequest(MemberRequestsParam param)
        {
            if (param == null)
            {
                throw new ArgumentNullException(nameof(param));
            }

            var criteria = new MemberRequestCriteria();

            var memberRequestData = dataAccess.GetMemberRequest(criteria);

            return memberRequestData;
        }


        public IEnumerable<Result> EnableProduct(EnableProductParam param)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Result> EnableCasino(EnableCasinoParam param)
        {
            List<int> custIds = param.CustIds.Split(',').Select(custId => Converter.ToInt(custId)).ToList();
            var results = new List<Result>();
            foreach (int custId in custIds)
            {
                var criteria = new EnableCasinoCriteria
                (
                    custId,
                    param.ActorId,
                    param.SubAccId,
                    param.LoginIP,
                    param.Language);

                var updateResult = dataAccess.EnableCasino(criteria);
                results.Add(updateResult);

                if (updateResult.IsSuccessful)
                {
                    SyncCasinoStatus(
                        custId,
                        param.LoginIP,
                        param.ActorId,
                        param.ActorName);
                }
            }

            return results;
        }

        private void SyncCasinoStatus(
           int custId,
           string ip, 
           int actorId, 
           string actorName)
        {
            var isCustomerExist = casinoService.Customer.CheckCustSetting(custId);
            if (isCustomerExist)
            {
                casinoService.Customer.UpdateCustEnableCasino(
                    custId,
                    true,
                    false,
                    ip,
                    actorId,
                    actorName,
                    CasinoBO.AgentSystem);
            }
        }
    }
}