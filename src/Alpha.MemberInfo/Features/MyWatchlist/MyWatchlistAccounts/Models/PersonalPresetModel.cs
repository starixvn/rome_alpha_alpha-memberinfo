namespace Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Models
{
    public class PersonalPresetModel
    {
        public PersonalPresetModel()
        {
        }

        public PersonalPresetModel(int id, int sporttype, string presetTitle, decimal winningThreshold, decimal losingThreshold)
        {
            ID = id;
            Sporttype = sporttype;
            PresetTitle = presetTitle;
            WinningThreshold = winningThreshold;
            LosingThreshold = losingThreshold;
        }

        public int ID { get; }

        public int Sporttype { get; }

        public string PresetTitle { get; }

        public decimal WinningThreshold { get; }

        public decimal LosingThreshold { get; }
    }
}
