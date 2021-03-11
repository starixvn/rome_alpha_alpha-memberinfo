namespace Alpha.MemberInfo.Features.CustomerList.Models
{
    public class DCSProductStatusModel
    {
        public DCSProductStatusModel() { }

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

        public bool HasDownline { get; private set; }
    }
}
