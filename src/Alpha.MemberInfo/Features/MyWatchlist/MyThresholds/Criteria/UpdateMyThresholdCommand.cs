namespace Alpha.MemberInfo.Features.MyWatchlist.MyThresholds.Criteria
{
    using Alpha.MemberInfo.Features.MyWatchlist.MyThresholds.Params;
    using Fanex.Data.Repository;

    public class UpdateMyThresholdCommand : NonQueryCommand
    {
        public UpdateMyThresholdCommand(UpdateMyThresholdParam param)
        {
            CustId = param.CustId;
            SportType = param.SportType;
            WinningThreshold = param.WinningThreshold;
            LosingThreshold = param.LosingThreshold;
            UserId = param.UserId;
            SubAccId = param.SubAccId;
            IP = param.IP;
        }

        public int CustId { get; }

        public short SportType { get; }

        public decimal? WinningThreshold { get; }

        public decimal? LosingThreshold { get; }

        public int UserId { get; }

        public int SubAccId { get; }

        public string IP { get; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public string CustList { get; set; }

        public override string GetSettingKey()
            => "Acc_WLMonitored_SMA_Adjust";

        public override bool IsValid()
            => CustId > 0;
    }
}
