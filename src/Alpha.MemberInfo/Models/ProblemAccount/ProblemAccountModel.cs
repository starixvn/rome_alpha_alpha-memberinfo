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

        public int CustId { get; }

        public string Username2 { get; }

        public string NickName => string.IsNullOrEmpty(Username2) ? string.Empty : Username2;

        public string UserName { get; }

        public string FirstName { get; }

        public string LastName { get; }

        public byte Status { get; }

        public string StatusName
            => Resources.ResourceManager.GetString(ProblemAccountStatusConstants.GetProblemAccountStatus(Status));

        public DateTime CreatedDate { get; }

        public string CreatedDateStr => CreatedDate.ToLongDateTime24h();

        public int Count { get; }

        public bool IsReviewed { get; }

        public string ReviewStatus => IsReviewed ? Resources.Reviewed : Resources.NotReviewed;

        public bool CanEdit => !IsReviewed;
    }
}
