namespace Alpha.MemberInfo.Models.ProblemAccount
{
    using System;
    using Alpha.MemberInfo.AppResources;
    using Alpha.MemberInfo.Constants;
    using Alpha.MemberInfo.Extensions;

    public class ProblemAccountModel
    {
        public ProblemAccountModel()
        {
        }

        public int CustId { get; internal set; }

        public string Username2 { get; internal set; }

        public string NickName => string.IsNullOrEmpty(Username2) ? string.Empty : Username2;

        public string UserName { get; } 

        public string FirstName { get; }

        public string LastName { get; }

        public byte Status { get; internal set; }

        public string StatusNameEN
            => ProblemAccountStatusConstants.GetProblemAccountStatus(Status);

        public string StatusName
            => Resources.ResourceManager.GetString(ProblemAccountStatusConstants.GetProblemAccountStatus(Status));

        public DateTime CreatedDate { get; internal set; }

        public string CreatedDateStr => CreatedDate.ToLongDateTime24h();

        public int Count { get; }

        public bool IsReviewed { get; internal set; }

        public int ClosedReason { get; }

        public string ClosedReasonName => CloseReasonConstants.GetName(ClosedReason);

        public string ReviewStatus => IsReviewed ? Resources.Reviewed : Resources.NotReviewed;

        public bool CanEdit => !IsReviewed;

        public byte Locked { get; }
    }
}
