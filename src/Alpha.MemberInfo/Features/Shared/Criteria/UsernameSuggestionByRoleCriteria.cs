namespace Alpha.MemberInfo.Features.Shared.Criteria
{
    using Fanex.Data.Repository;

    public class UsernameSuggestionByRoleCriteria : CriteriaBase
    {
        public UsernameSuggestionByRoleCriteria(
            int custId,
            string username,
            byte roleId
            )
        {
            CustId = custId;
            Username = username;
            RoleId = roleId;
        }

        public int CustId { get; set; }

        public string Username { get; set; }

        public byte RoleId { get; set; }

        public override string GetSettingKey() => "Age_CustomerSelUserSearch_ByRole";

        public override bool IsValid() => CustId > 0 && !string.IsNullOrEmpty(Username);
    }
}
