namespace Alpha.MemberInfo.Features.CustomerList.Models
{
    public class NonDCSProductStatusModel
    {
        public NonDCSProductStatusModel() { }

        public NonDCSProductStatusModel(
            bool disableEsports,
            bool uplineDisableEsports
            )
        {
            DisableEsports = disableEsports;
            UplineDisableEsports = uplineDisableEsports;
        }

        public bool DisableEsports { get; private set; }

        public bool UplineDisableEsports { get; private set; }
    }
}
