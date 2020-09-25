namespace Alpha.MemberInfo.Site.Features.CustomerList.ViewModels
{
    public class SyncedCasinoModel
    {
        public SyncedCasinoModel()
        {
        }

        public SyncedCasinoModel(
            int custId,
            bool syncedCasino)
        {
            CustId = custId;
            SyncedCasino = syncedCasino;
        }

        public int CustId { get; }

        public bool SyncedCasino { get; }
    }
}