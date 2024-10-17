namespace Alpha.MemberInfo.Features.CustomerList.Models
{
    public class DCSProductStatusModel
    {
        public DCSProductStatusModel()
        { }

        public DCSProductStatusModel(
             int productId,
             int sportType,
             int betType,
             bool isHidden,
             bool isDisabled,
             bool isHiddenUpline,
             bool isDisabledUpline,
             bool hasDownline,
             int? groupId)
        {
            ProductId = productId;
            SportType = sportType;
            BetType = betType;
            IsHidden = isHidden;
            IsDisabled = isDisabled;
            IsHiddenUpline = isHiddenUpline;
            IsDisabledUpline = isDisabledUpline;
            HasDownline = hasDownline;
            GroupId = groupId;
        }

        public int ProductId { get; set; }

        public int SportType { get; set; }

        public int BetType { get; set; }

        public bool IsHidden { get; set; }

        public bool IsDisabled { get; set; }

        public bool IsHiddenUpline { get; set; }

        public bool IsDisabledUpline { get; set; }

        public bool HasDownline { get; set; }

        public int? GroupId { get; set; }
    }
}