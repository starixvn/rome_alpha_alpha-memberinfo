namespace Alpha.MemberInfo.Features.MyWatchlist.MyWatchlistAccounts.Models
{
    public class PersonalPresetModel
    {
        public PersonalPresetModel()
        {
        }

        public int ID { get; }

        public int Sporttype { get; }

        public string PresetTitle { get; }

        public decimal WinningThreshold { get; }

        public decimal LosingThreshold { get; }
    }
}
