namespace Alpha.MemberInfo.Features.MyMonitorAccounts.MyWatchlistAccounts.Models
{
    public class PersonalPresetResultModel
    {
        public PersonalPresetResultModel()
        {
        }

        public PersonalPresetResultModel(string presetTitle, int errCode, string errMsg)
        {
            PresetTitle = presetTitle;
            ErrCode = errCode;
            ErrMsg = errMsg;
        }

        public string PresetTitle { get; }

        public int ErrCode { get; }

        public string ErrMsg { get; }
    }
}
