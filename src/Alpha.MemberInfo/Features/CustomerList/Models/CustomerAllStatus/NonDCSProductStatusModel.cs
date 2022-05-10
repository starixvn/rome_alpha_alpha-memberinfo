namespace Alpha.MemberInfo.Features.CustomerList.Models
{
    public class NonDCSProductStatusModel
    {
        public NonDCSProductStatusModel() { }

        public NonDCSProductStatusModel(
            bool disableNumber,
            bool uplineDisableNumber,
            bool disableEsports,
            bool uplineDisableEsports,
            bool hideKeno,
            bool disableKeno   
            )
        {           

            DisableNumber = disableNumber;
            UplineDisableNumber = uplineDisableNumber;
            DisableEsports = disableEsports;
            UplineDisableEsports = uplineDisableEsports;
            HideKeno = hideKeno;
            DisableKeno = disableKeno;
        }       

        public bool DisableNumber { get; private set; }

        public bool UplineDisableNumber { get; private set; }

        public bool DisableEsports { get; private set; }

        public bool UplineDisableEsports { get; private set; }

        public bool HideKeno { get; private set; }

        public bool DisableKeno { get; private set; }
    }
}
