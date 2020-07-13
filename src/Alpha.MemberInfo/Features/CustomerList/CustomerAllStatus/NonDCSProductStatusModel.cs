namespace Alpha.MemberInfo.Features.CustomerList.Models.AccountInfo
{
    public class NonDCSProductStatusModel
    {
        public NonDCSProductStatusModel() { }

        public NonDCSProductStatusModel(
            bool syncCasino,
            bool disableCasino,
            bool uplineDisableCS,
            bool disableNumber,
            bool uplineDisableNumber,
            bool disableEsports,
            bool uplineDisableEsports,
            bool hideKeno,
            bool uplineHideGoldDeluxe,
            bool uplineDisableGoldDeluxe,
            bool disableGoldDeluxe,
            bool hideGoldDeluxe)
        {
            SyncCasino = syncCasino;
            DisableCasino = disableCasino;
            UplineDisableCS = uplineDisableCS;

            DisableNumber = disableNumber;
            UplineDisableNumber = uplineDisableNumber;

            DisableEsports = disableEsports;
            UplineDisableEsports = uplineDisableEsports;

            HideKeno = hideKeno;

            HideGoldDeluxe = hideGoldDeluxe;
            DisableGoldDeluxe = disableGoldDeluxe;
            UplineDisableGoldDeluxe = uplineDisableGoldDeluxe;
            UplineHideGoldDeluxe = uplineHideGoldDeluxe;
        }

        public bool SyncCasino { get; private set; }

        public bool DisableCasino { get; private set; }

        public bool UplineDisableCS { get; private set; }

        public bool DisableNumber { get; private set; }

        public bool UplineDisableNumber { get; private set; }

        public bool DisableEsports { get; private set; }

        public bool UplineDisableEsports { get; private set; }

        public bool HideKeno { get; private set; }

        public bool UplineHideGoldDeluxe { get; private set; }

        public bool UplineDisableGoldDeluxe { get; private set; }

        public bool DisableGoldDeluxe { get; private set; }

        public bool HideGoldDeluxe { get; private set; }
    }
}
