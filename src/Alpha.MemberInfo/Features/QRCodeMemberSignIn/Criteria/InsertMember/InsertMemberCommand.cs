namespace Alpha.MemberInfo.Features.QRCodeMemberSignIn.Criteria
{
    using Alpha.MemberInfo.Features.QRCodeMemberSignIn.Models;
    using Fanex.Data.Repository;

    public partial class InsertMemberCommand : NonQueryCommand
    {
        private readonly byte alphaSystemId = 2;

        public InsertMemberCommand(
            SportBookSettingModel setting,
            string username,
            string firstName,
            int actorId,
            int custIdCopyFrom,
            string ip,
            string language,
            int subAccId,
            decimal credit,
            string password,
            string copyFromUsername,
            decimal creditMax,
            string subUsername)
        {
            SubUsername = subUsername;
            IsQRCode = true;
            CreditMax = creditMax;
            CopyFromUsername = copyFromUsername;
            Username = username;
            FirstName = firstName;
            ActorId = actorId;
            Recommend = actorId;
            CopyFromCustId = custIdCopyFrom;
            Password = password;
            IP = ip;
            Language = language;
            SubAccId = subAccId;
            IsSearchAndCopy = true;
            SystemId = alphaSystemId;

            PlayerDiscount = setting.PlayerDiscount;
            PlayerDiscountCS = setting.PlayerDiscountCS;
            PlayerDiscount1x2 = setting.PlayerDiscount1x2;
            MinBet = setting.MinBet;
            MaxBet = setting.MaxBet;
            MaxPerMatch = setting.MaxPerMatch;
            MinBetV2 = setting.MinBetV2;
            MaxBetV2 = setting.MaxBetV2;
            MaxPerMatchV2 = setting.MaxPerMatchV2;
            MinBetV3 = setting.MinBetV3;
            MaxBetV3 = setting.MaxBetV3;
            MaxPerMatchV3 = setting.MaxPerMatchV3;
            MinBetV5 = setting.MinBetV5;
            MaxBetV5 = setting.MaxBetV5;
            MaxPerMatchV5 = setting.MaxPerMatchV5;
            MinBetV8 = setting.MinBetV8;
            MaxBetV8 = setting.MaxBetV8;
            MaxPerMatchV8 = setting.MaxPerMatchV8;
            MinBetV10 = setting.MinBetV10;
            MaxBetV10 = setting.MaxBetV10;
            MaxPerMatchV10 = setting.MaxPerMatchV10;
            MinBetV11 = setting.MinBetV11;
            MaxBetV11 = setting.MaxBetV11;
            MaxPerMatchV11 = setting.MaxPerMatchV11;
            MinBetV99 = setting.MinBetV99;
            MaxBetV99 = setting.MaxBetV99;
            MaxPerMatchV99 = setting.MaxPerMatchV99;
            MinBetV99MP = setting.MinBetV99MP;
            MaxBetV99MP = setting.MaxBetV99MP;
            MaxPerMatchV99MP = setting.MaxPerMatchV99MP;
            MinBetV1MP = setting.MinBetV1MP;
            MaxBetV1MP = setting.MaxBetV1MP;
            MaxPerMatchV1MP = setting.MaxPerMatchV1MP;
            MinBetV161 = setting.MinBetV161;
            MaxBetV161 = setting.MaxBetV161;
            MaxPerMatchV161 = setting.MaxPerMatchV161;
            MaxPerBallV161 = setting.MaxPerBallV161;
            MinBetV164 = setting.MinBetV164;
            MaxBetV164 = setting.MaxBetV164;
            MaxPerMatchV164 = setting.MaxPerMatchv164;

            Credit = credit;
            DisableCasino = setting.DisableCasino;
            DisableNumber = setting.DisableNumber;
            UGroup = setting.UGroup;
            Closed = setting.Closed;

            APositionTaking_1 = setting.APositionTaking_1;
            APositionTaking_2 = setting.APositionTaking_2;
            APositionTaking_3 = setting.APositionTaking_3;
            APositionTaking_4 = setting.APositionTaking_4;
            APositionTaking_5 = setting.APositionTaking_5;
            APositionTaking_6 = setting.APositionTaking_6;
            APositionTaking_7 = setting.APositionTaking_7;
            APositionTaking_8 = setting.APositionTaking_8;
            APositionTaking_9 = setting.APositionTaking_9;
            APositionTaking_10 = setting.APositionTaking_10;

            V2APositionTaking_10 = setting.V2APositionTaking_10;
            V3APositionTaking_10 = setting.V3APositionTaking_10;
            V5APositionTaking_10 = setting.V5APositionTaking_10;
            V8APositionTaking_10 = setting.V8APositionTaking_10;
            V10APositionTaking_10 = setting.V10APositionTaking_10;
            V11APositionTaking_10 = setting.V11APositionTaking_10;
            V99APositionTaking_10 = setting.V99APositionTaking_10;

            APositionTaking_9999 = setting.APositionTaking_9999;

            ALivePositionTaking_1 = setting.ALivePositionTaking_1;
            ALivePositionTaking_3 = setting.ALivePositionTaking_3;
            ALivePositionTaking_7 = setting.ALivePositionTaking_7;
            ALivePositionTaking_8 = setting.ALivePositionTaking_8;

            V2APositionTaking_1 = setting.V2APositionTaking_1;
            V2APositionTaking_2 = setting.V2APositionTaking_2;
            V2APositionTaking_3 = setting.V2APositionTaking_3;
            V2APositionTaking_7 = setting.V2APositionTaking_7;
            V2APositionTaking_8 = setting.V2APositionTaking_8;
            V2APositionTaking_20 = setting.V2APositionTaking_20;
            V2APositionTaking_9999 = setting.V2APositionTaking_9999;

            V3APositionTaking_1 = setting.V3APositionTaking_1;
            V3APositionTaking_2 = setting.V3APositionTaking_2;
            V3APositionTaking_3 = setting.V3APositionTaking_3;
            V3APositionTaking_20 = setting.V3APositionTaking_20;

            V5APositionTaking_1 = setting.V5APositionTaking_1;
            V5APositionTaking_2 = setting.V5APositionTaking_2;
            V5APositionTaking_3 = setting.V5APositionTaking_3;
            V5APositionTaking_20 = setting.V5APositionTaking_20;
            V5APositionTaking_9999 = setting.V5APositionTaking_9999;

            V8APositionTaking_1 = setting.V8APositionTaking_1;
            V8APositionTaking_3 = setting.V8APositionTaking_3;
            V8APositionTaking_20 = setting.V8APositionTaking_20;

            V10APositionTaking_1 = setting.V10APositionTaking_1;
            V10APositionTaking_2 = setting.V10APositionTaking_2;
            V10APositionTaking_3 = setting.V10APositionTaking_3;
            V10APositionTaking_20 = setting.V10APositionTaking_20;

            V11APositionTaking_1 = setting.V11APositionTaking_1;
            V11APositionTaking_20 = setting.V11APositionTaking_20;

            V99APositionTaking_1 = setting.V99APositionTaking_1;
            V99APositionTaking_2 = setting.V99APositionTaking_2;
            V99APositionTaking_3 = setting.V99APositionTaking_3;
            V99APositionTaking_9 = setting.V99APositionTaking_9;
            V99APositionTaking_20 = setting.V99APositionTaking_20;
            V99APositionTaking_9999 = setting.V99APositionTaking_9999;

            V161APositionTaking_81 = setting.V161APositionTaking_81;
            V161APositionTaking_83 = setting.V161APositionTaking_83;
            V161APositionTaking_86 = setting.V161APositionTaking_86;
            V161APositionTaking_88 = setting.V161APositionTaking_88;

            V161ALivePositionTaking_85 = setting.V161ALivePositionTaking_85;
            V161ALivePositionTaking_86 = setting.V161ALivePositionTaking_86;
            V161ALivePositionTaking_87 = setting.V161ALivePositionTaking_87;
            V161ALivePositionTaking_89 = setting.V161ALivePositionTaking_89;
            V161ALivePositionTaking_90 = setting.V161ALivePositionTaking_90;
        }
    }
}
