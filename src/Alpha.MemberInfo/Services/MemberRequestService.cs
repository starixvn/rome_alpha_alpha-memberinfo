namespace Alpha.MemberInfo.Services
{
    using System;
    using System.Collections.Generic;
    using Models;
    using Params;
    using Reports.Criteria.WinLoss;
    using Reports.DataAccesses;

    public interface MemberRequestService
    {
        IEnumerable<MemberRequestModel> GetMemberRequest(MemberRequestParam param);
    }

    public class MemberRequestServiceImpl : MemberRequestService
    {
        private readonly MemberRequestDataAccess dataAccess;

        public MemberRequestServiceImpl(MemberRequestDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public IEnumerable<MemberRequestModel> GetMemberRequest(MemberRequestParam param)
        {
            if (param == null)
            {
                throw new ArgumentNullException(nameof(param));
            }


            var criteria = new MemberRequestCriteria();

            var memberRequestData = dataAccess.GetMemberRequest(criteria);

            return memberRequestData;
        }

    }
}