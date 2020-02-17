using System;
using System.Collections.Generic;
using Alpha.MemberInfo.Enumerations;
namespace Alpha.MemberInfo.Site.Requests.ProblemAccount
{
    using Alpha.MemberInfo.Models;
    using Alpha.MemberInfo.Models.ProblemAccount;
    using MediatR;

    public class ProblemAccountRequest: MemberInfoBaseQuery, IRequest<IEnumerable<ProblemAccountModel>>
    {
        public ProblemAccountRequest(IUser viewer):base(viewer)
        {            
        }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int UserSelectedReviewedStatusId { get; set; }

        public int UplineId { get; set; }

        public string UplineName { get; set; }

        public byte SearchRoleId { get; set; }

        public UserLevel SearchRole => Enumeration.FromValue<UserLevel>(SearchRoleId);

        public string UserName { get; set; }
    }
}