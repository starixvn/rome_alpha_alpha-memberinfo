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

        public int CustId { get; set; }

        public string NickName { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public byte Status { get; set; }

        public string StatusName 
            => Resources.ResourceManager.GetString(ProblemAccountStatusConstants.GetProblemAccountStatus(Status));

        public DateTime CreatedDate { get; set; }

        public string CreatedDateStr => CreatedDate.ToLongDateTime24h();

        public int Count { get; set; }

        public bool IsReviewed { get; set; }

        public string ReviewStatus => IsReviewed ? Resources.Reviewed : Resources.NotReviewed;

        public bool CanEdit => !IsReviewed;
    }
}
