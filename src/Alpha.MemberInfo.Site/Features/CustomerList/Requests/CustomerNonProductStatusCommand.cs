namespace Alpha.MemberInfo.Site.Features.CustomerList.Requests
{
    using Alpha.MemberInfo.Features.Shared.Models;
    using Alpha.MemberInfo.Models;
    using MediatR;

    public class CustomerNonProductStatusCommand : CustomerStatusQueryBase, IRequest<Result>
    {
        private readonly IUser viewer;

        public CustomerNonProductStatusCommand(IUser viewer) : base(viewer)
        {
            this.viewer = viewer;
        }

        public bool SyncedCasino3rd { get; set; }

        public bool StatusValue { get; set; }

        public string StatusName { get; set; }

        public override bool IsValid()
        {
            var isValid = base.IsValid() && IsValidName(StatusName) && CanUpdateAccount && !IsInternal;
            return !StatusName.Equals(StatusNames.DoubleComm) ? isValid : isValid && IsValidDoubleComm;
        }

        internal bool IsValidDoubleComm => viewer.LevelId == SearchAccountRoleId + 1;
    }
}