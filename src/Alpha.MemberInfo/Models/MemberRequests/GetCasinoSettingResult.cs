namespace Alpha.MemberInfo.Models.MemberRequests
{
    using System.Collections.Generic;
    using System.Linq;
    using Alpha.MemberInfo.Constants;

    public class GetCasinoSettingResult : GetSettingResult
    {
        public GetCasinoSettingResult(
            IEnumerable<CasinoGameKindModel> gameKinds,
            int errorCode,
            string errorMessage) : base(false, errorCode, errorMessage)
        {
            var rngGame = gameKinds.FirstOrDefault(gameKind => gameKind.Id == CasinoGameKind.RNGGame);

            HasSetting = rngGame != null
                && (rngGame.PositionTaking != 0 || rngGame.Commission != 0);
        }

        public IEnumerable<CasinoGameKindModel> GameKinds { get; }
    }
}
