namespace Alpha.MemberInfo.Features.CustomerList.Models.AccountInfo
{
    public class DCSProductStatusModel
    {
        public DCSProductStatusModel() { }

#pragma warning disable S107 // Methods should not have too many parameters
        public DCSProductStatusModel(
             int productId,
             int sportType,
             int betType,
             bool isSync,
             bool isHidden,
             bool isDisabled,
             bool isSyncedUpline,
             bool isHiddenUpline,
             bool isDisabledUpline,
             bool hasDownline)
#pragma warning restore S107 // Methods should not have too many parameters
        {
            ProductId = productId;
            SportType = sportType;
            BetType = betType;
            IsSync = isSync;
            IsHidden = isHidden;
            IsDisabled = isDisabled;
            IsSyncedUpline = isSyncedUpline;
            IsHiddenUpline = isHiddenUpline;
            IsDisabledUpline = isDisabledUpline;
            HasDownline = hasDownline;
        }

       public int ProductId { get; private set; }

       public int SportType { get; private set; }

        public int BetType { get; private set; }

        public bool IsSync { get; private set; }

        public bool IsHidden { get; private set; }

        public bool IsDisabled { get; private set; }

        public bool IsSyncedUpline { get; private set; }

        public bool IsHiddenUpline { get; private set; }

        public bool IsDisabledUpline { get; private set; }

        public bool HasDownline { get; }
    }
}
