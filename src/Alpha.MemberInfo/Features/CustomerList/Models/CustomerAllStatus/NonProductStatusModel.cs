namespace Alpha.MemberInfo.Features.CustomerList.Models
{
    public class NonProductStatusModel
    {
        private readonly byte TransferDailyOption = 7;

        public NonProductStatusModel()
        {
        }

        public NonProductStatusModel(
            bool hasDownline,
            bool disableAutoMPT,
            bool closed,
            byte isDirectClosed,
            bool uplineClosed,
            bool suspended,
            bool uplineSuspend,
            bool isDirectSuspend,
            string transferOption,
            bool doubleCommLosing,
            bool uplineDoubleCommLosing,
            bool disableOutright,
            bool uplineDisableOutright,
            bool isAllDayBalTransfer,
            byte locked,
            bool isProtectedMode)
        {
            HasDownline = hasDownline;
            DisableAutoMPT = disableAutoMPT;

            Closed = closed;
            IsDirectClosed = isDirectClosed;
            UplineClosed = uplineClosed;

            Suspended = suspended;
            IsDirectSuspend = isDirectSuspend;
            UplineSuspend = uplineSuspend;

            TransferOption = transferOption;

            DoubleCommLosing = doubleCommLosing;
            UplineDoubleCommLosing = uplineDoubleCommLosing;

            DisableOutright = disableOutright;
            UplineDisableOutright = uplineDisableOutright;
            IsAllDayBalTransfer = isAllDayBalTransfer;

            Locked = locked;
            IsProtectedMode = isProtectedMode;
        }

        public bool HasDownline { get; private set; }

        public bool Closed { get; private set; }

        public byte IsDirectClosed { get; private set; }

        public bool UplineClosed { get; private set; }

        public bool Suspended { get; private set; }

        public bool IsDirectSuspend { get; private set; }

        public bool UplineSuspend { get; private set; }

        public string TransferOption { get; private set; }

        public bool TransferDaily => 
            !string.IsNullOrWhiteSpace(TransferOption) && 
            TransferOption[TransferDailyOption].Equals('0');

        public bool DoubleCommLosing { get; private set; }

        public bool UplineDoubleCommLosing { get; private set; }

        public bool DisableAutoMPT { get; private set; }

        public bool DisableOutright { get; private set; }

        public bool UplineDisableOutright { get; private set; }

        public bool IsAllDayBalTransfer { get; }

        public byte Locked { get; }

        public bool IsProtectedMode { get; }
    }
}