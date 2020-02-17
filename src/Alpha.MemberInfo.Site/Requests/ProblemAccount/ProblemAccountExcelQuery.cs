using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Alpha.MemberInfo.Enumerations;
using Alpha.MemberInfo.Models;
using Alpha.MemberInfo.Site.ViewModels;
using MediatR;

namespace Alpha.MemberInfo.Site.Requests.ProblemAccount
{
    public class ProblemAccountExcelQuery: MemberInfoBaseQuery, IRequest<ExcelFileViewModel>
    {
        public ProblemAccountExcelQuery(IUser viewer)
            : base(viewer)
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