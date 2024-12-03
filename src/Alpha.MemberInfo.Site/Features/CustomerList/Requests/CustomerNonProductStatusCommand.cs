namespace Alpha.MemberInfo.Site.Features.CustomerList.Requests
{
    using Alpha.MemberInfo.Features.Shared.Models;
    using Alpha.MemberInfo.Models;
    using MediatR;

    public class CustomerNonProductStatusCommand : CustomerStatusQueryBase, IRequest<Result>
    {
        public CustomerNonProductStatusCommand(IUser viewer) : base(viewer)
        {
        }

        public bool StatusValue { get; set; }

        public string StatusName { get; set; }

        public override bool IsValid()
        {
            var isValidSearchAccount = SearchAccountId > 0 && !SearchAccountId.Equals(viewer.Id);
            var allowUpdate = CanUpdateAccount && !IsInternal;

            var isValid = isValidSearchAccount && IsValidName(StatusName) && allowUpdate;
            return !StatusName.Equals(StatusNames.DoubleComm) ? isValid : isValid && IsValidDoubleComm;
        }

        protected bool IsValidDoubleComm => viewer.LevelId == SearchAccountRoleId + 1;
    }
}