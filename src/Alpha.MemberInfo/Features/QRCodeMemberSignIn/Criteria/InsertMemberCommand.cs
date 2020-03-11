namespace Alpha.MemberInfo.Features.QRCodeMemberSignIn.Criteria
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class InsertMemberCommand : NonQueryCommand
    {
        public string Username { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        [SpParam(SqlParamName = "Creator")]
        public int ActorId { get; set; }

        [SpParam(SqlParamName = "userpwd")]
        public string PassWord { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;
        public string Fax { get; set; } = string.Empty;
        public string MobilePhone { get; set; } = string.Empty;

        public decimal PlayerDiscount { get; set; }
        public decimal PlayerDiscountCS { get; set; }
        public decimal PlayerDiscount1x2 { get; set; }

        public decimal MinBet { get; set; }
        public decimal MaxBet { get; set; }
        public decimal MaxPerMatch { get; set; }

        public decimal MinBetV2 { get; set; }
        public decimal MaxBetV2 { get; set; }
        public decimal MaxPerMatchV2 { get; set; }

        public decimal MinBetV3 { get; set; }
        public decimal MaxBetV3 { get; set; }
        public decimal MaxPerMatchV3 { get; set; }

        public decimal MinBetV5 { get; set; }
        public decimal MaxBetV5 { get; set; }
        public decimal MaxPerMatchV5 { get; set; }

        public decimal MinBetV8 { get; set; }
        public decimal MaxBetV8 { get; set; }
        public decimal MaxPerMatchV8 { get; set; }

        public decimal MinBetV10 { get; set; }
        public decimal MaxBetV10 { get; set; }
        public decimal MaxPerMatchV10 { get; set; }

        public decimal MinBetV11 { get; set; }
        public decimal MaxBetV11 { get; set; }
        public decimal MaxPerMatchV11 { get; set; }

        public decimal MinBetV99 { get; set; }
        public decimal MaxBetV99 { get; set; }
        public decimal MaxPerMatchV99 { get; set; }

        public decimal MinBetV99MP { get; set; }
        public decimal MaxBetV99MP { get; set; }
        public decimal MaxPerMatchV99MP { get; set; }

        public decimal MinBetV1MP { get; set; }
        public decimal MaxBetV1MP { get; set; }
        public decimal MaxPerMatchV1MP { get; set; }

        public decimal MinBetV161 { get; set; }
        public decimal MaxBetV161 { get; set; }
        public decimal MaxPerMatchV161 { get; set; }
        public decimal MaxPerBallV161 { get; set; }

        public decimal MinBetV164 { get; set; }
        public decimal MaxBetV164 { get; set; }
        public decimal MaxPerMatchV164 { get; set; }

        public decimal Credit { get; set; }

        public bool DisableCasino { get; set; }
        public bool DisableNumber { get; set; }

        public int Recommend { get; set; }
        public string Ugroup { get; set; }
        public bool Closed { get; set; }

        public decimal Apositiontaking_1 { get; set; }
        public decimal Apositiontaking_2 { get; set; }
        public decimal Apositiontaking_3 { get; set; }
        public decimal Apositiontaking_4 { get; set; }
        public decimal Apositiontaking_5 { get; set; }
        public decimal Apositiontaking_6 { get; set; }
        public decimal Apositiontaking_7 { get; set; }
        public decimal Apositiontaking_8 { get; set; }
        public decimal Apositiontaking_9 { get; set; }
        public decimal Apositiontaking_10 { get; set; }

        public decimal V2apositiontaking_10 { get; set; }
        public decimal V3apositiontaking_10 { get; set; }
        public decimal V5apositiontaking_10 { get; set; }
        public decimal V8apositiontaking_10 { get; set; }
        public decimal V10apositiontaking_10 { get; set; }
        public decimal V11apositiontaking_10 { get; set; }
        public decimal V99apositiontaking_10 { get; set; }

        public decimal Apositiontaking_9999 { get; set; }

        public decimal Alivepositiontaking_1 { get; set; }
        public decimal Alivepositiontaking_3 { get; set; }
        public decimal Alivepositiontaking_7 { get; set; }
        public decimal Alivepositiontaking_8 { get; set; }

        public decimal V2apositiontaking_1 { get; set; }
        public decimal V2apositiontaking_2 { get; set; }
        public decimal V2apositiontaking_3 { get; set; }
        public decimal V2apositiontaking_7 { get; set; }
        public decimal V2apositiontaking_8 { get; set; }
        public decimal V2apositiontaking_20 { get; set; }
        public decimal V2apositiontaking_9999 { get; set; }

        public decimal V3apositiontaking_1 { get; set; }
        public decimal V3apositiontaking_2 { get; set; }
        public decimal V3apositiontaking_3 { get; set; }
        public decimal V3apositiontaking_20 { get; set; }

        public decimal V5apositiontaking_1 { get; set; }
        public decimal V5apositiontaking_2 { get; set; }
        public decimal V5apositiontaking_3 { get; set; }
        public decimal V5apositiontaking_20 { get; set; }
        public decimal V5apositiontaking_9999 { get; set; }

        public decimal V8apositiontaking_1 { get; set; }
        public decimal V8apositiontaking_3 { get; set; }
        public decimal V8apositiontaking_20 { get; set; }

        public decimal V10apositiontaking_1 { get; set; }
        public decimal V10apositiontaking_2 { get; set; }
        public decimal V10apositiontaking_3 { get; set; }
        public decimal V10apositiontaking_20 { get; set; }

        public decimal V11apositiontaking_1 { get; set; }
        public decimal V11apositiontaking_20 { get; set; }

        public decimal V99apositiontaking_1 { get; set; }
        public decimal V99apositiontaking_2 { get; set; }
        public decimal V99apositiontaking_3 { get; set; }
        public decimal V99apositiontaking_9 { get; set; }
        public decimal V99apositiontaking_20 { get; set; }
        public decimal V99apositiontaking_9999 { get; set; }

        public decimal V161apositiontaking_81 { get; set; }
        public decimal V161apositiontaking_83 { get; set; }
        public decimal V161apositiontaking_86 { get; set; }
        public decimal V161apositiontaking_88 { get; set; }

        public decimal V161alivepositiontaking_85 { get; set; }
        public decimal V161alivepositiontaking_86 { get; set; }
        public decimal V161alivepositiontaking_87 { get; set; }
        public decimal V161alivepositiontaking_89 { get; set; }
        public decimal V161alivepositiontaking_90 { get; set; }

        public string IP { get; set; }
        public byte SystemId { get; set; }
        public string Language { get; set; }
        public int SubAccId { get; set; }
        public bool IsSearchAndCopy { get; set; }
        public int CopyFromCustId { get; set; }

        public int ErrCode { get; set; }
        public string ErrMsg { get; set; }
        public bool Success { get; set; }
        public int Custid { get; set; }

        public override string GetSettingKey() => "Acc_CustomerInsMember_Ag";

        public override bool IsValid() => true;
    }
}
