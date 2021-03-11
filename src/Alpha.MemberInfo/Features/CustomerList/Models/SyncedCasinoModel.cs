namespace Alpha.MemberInfo.Site.Features.CustomerList.ViewModels
{
    public class SyncedCasinoModel
    {
        public SyncedCasinoModel()
        {
        }

        public SyncedCasinoModel(
            int custId,
            bool syncCasino)
        {
            CustId = custId;
            SyncCasino = syncCasino;
        }

        public int CustId { get; }

        public bool SyncCasino { get; }
    }
}