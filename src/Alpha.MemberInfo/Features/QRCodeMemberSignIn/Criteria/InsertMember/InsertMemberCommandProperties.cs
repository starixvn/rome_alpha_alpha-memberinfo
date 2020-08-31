namespace Alpha.MemberInfo.Features.QRCodeMemberSignIn.Criteria
{
    using Fanex.Data;

    public partial class InsertMemberCommand
    {
        public bool IsQRCode { get; }
        public decimal CreditMax { get; private set; }
        public string Username { get; }
        public string FirstName { get; }
        public string LastName { get; set; } = string.Empty;

        [SpParam(SqlParamName = "Creator")]
        public int ActorId { get; }

        [SpParam(SqlParamName = "userpwd")]
        public string Password { get; }

        public string Phone { get; set; } = string.Empty;
        public string Fax { get; set; } = string.Empty;
        public string MobilePhone { get; set; } = string.Empty;

        public decimal PlayerDiscount { get; }
        public decimal PlayerDiscountCS { get; }
        public decimal PlayerDiscount1x2 { get; }

        public decimal MinBet { get; }
        public decimal MaxBet { get; }
        public decimal MaxPerMatch { get; }

        public decimal MinBetV2 { get; }
        public decimal MaxBetV2 { get; }
        public decimal MaxPerMatchV2 { get; }

        public decimal MinBetV3 { get; }
        public decimal MaxBetV3 { get; }
        public decimal MaxPerMatchV3 { get; }

        public decimal MinBetV5 { get; }
        public decimal MaxBetV5 { get; }
        public decimal MaxPerMatchV5 { get; }

        public decimal MinBetV8 { get; }
        public decimal MaxBetV8 { get; }
        public decimal MaxPerMatchV8 { get; }

        public decimal MinBetV10 { get; }
        public decimal MaxBetV10 { get; }
        public decimal MaxPerMatchV10 { get; }

        public decimal MinBetV11 { get; }
        public decimal MaxBetV11 { get; }
        public decimal MaxPerMatchV11 { get; }

        public decimal MinBetV99 { get; }
        public decimal MaxBetV99 { get; }
        public decimal MaxPerMatchV99 { get; }

        public decimal MinBetV99MP { get; }
        public decimal MaxBetV99MP { get; }
        public decimal MaxPerMatchV99MP { get; }

        public decimal MinBetV1MP { get; }
        public decimal MaxBetV1MP { get; }
        public decimal MaxPerMatchV1MP { get; }

        public decimal MinBetV161 { get; }
        public decimal MaxBetV161 { get; }
        public decimal MaxPerMatchV161 { get; }
        public decimal MaxPerBallV161 { get; }

        public decimal MinBetV164 { get; }
        public decimal MaxBetV164 { get; }
        public decimal MaxPerMatchV164 { get; }

        public decimal Credit { get; }

        public bool DisableCasino { get; }
        public bool DisableNumber { get; }

        public int Recommend { get; }
        public string UGroup { get; }
        public bool Closed { get; }

        public decimal APositionTaking_1 { get; }
        public decimal APositionTaking_2 { get; }
        public decimal APositionTaking_3 { get; }
        public decimal APositionTaking_4 { get; }
        public decimal APositionTaking_5 { get; }
        public decimal APositionTaking_6 { get; }
        public decimal APositionTaking_7 { get; }
        public decimal APositionTaking_8 { get; }
        public decimal APositionTaking_9 { get; }
        public decimal APositionTaking_10 { get; }

        public decimal V2APositionTaking_10 { get; }
        public decimal V3APositionTaking_10 { get; }
        public decimal V5APositionTaking_10 { get; }
        public decimal V8APositionTaking_10 { get; }
        public decimal V10APositionTaking_10 { get; }
        public decimal V11APositionTaking_10 { get; }
        public decimal V99APositionTaking_10 { get; }

        public decimal APositionTaking_9999 { get; }

        public decimal ALivePositionTaking_1 { get; }
        public decimal ALivePositionTaking_3 { get; }
        public decimal ALivePositionTaking_7 { get; }
        public decimal ALivePositionTaking_8 { get; }

        public decimal V2APositionTaking_1 { get; }
        public decimal V2APositionTaking_2 { get; }
        public decimal V2APositionTaking_3 { get; }
        public decimal V2APositionTaking_7 { get; }
        public decimal V2APositionTaking_8 { get; }
        public decimal V2APositionTaking_20 { get; }
        public decimal V2APositionTaking_9999 { get; }

        public decimal V3APositionTaking_1 { get; }
        public decimal V3APositionTaking_2 { get; }
        public decimal V3APositionTaking_3 { get; }
        public decimal V3APositionTaking_20 { get; }

        public decimal V5APositionTaking_1 { get; }
        public decimal V5APositionTaking_2 { get; }
        public decimal V5APositionTaking_3 { get; }
        public decimal V5APositionTaking_20 { get; }
        public decimal V5APositionTaking_9999 { get; }

        public decimal V8APositionTaking_1 { get; }
        public decimal V8APositionTaking_3 { get; }
        public decimal V8APositionTaking_20 { get; }

        public decimal V10APositionTaking_1 { get; }
        public decimal V10APositionTaking_2 { get; }
        public decimal V10APositionTaking_3 { get; }
        public decimal V10APositionTaking_20 { get; }

        public decimal V11APositionTaking_1 { get; }
        public decimal V11APositionTaking_20 { get; }

        public decimal V99APositionTaking_1 { get; }
        public decimal V99APositionTaking_2 { get; }
        public decimal V99APositionTaking_3 { get; }
        public decimal V99APositionTaking_9 { get; }
        public decimal V99APositionTaking_20 { get; }
        public decimal V99APositionTaking_9999 { get; }

        public decimal V161APositionTaking_81 { get; }
        public decimal V161APositionTaking_83 { get; }
        public decimal V161APositionTaking_86 { get; }
        public decimal V161APositionTaking_88 { get; }

        public decimal V161ALivePositionTaking_85 { get; }
        public decimal V161ALivePositionTaking_86 { get; }
        public decimal V161ALivePositionTaking_87 { get; }
        public decimal V161ALivePositionTaking_89 { get; }
        public decimal V161ALivePositionTaking_90 { get; }

        public string IP { get; }
        public byte SystemId { get; }
        public string Language { get; }
        public int SubAccId { get; }
        public bool IsSearchAndCopy { get; }
        public int CopyFromCustId { get; }

        public int ErrCode { get; set; }
        public string ErrMsg { get; set; }
        public bool Success { get; set; }
        public int CustId { get; set; }
        public string CopyFromUsername { get; internal set; }

        public override string GetSettingKey() => "Acc_CustomerInsMember_Ag";

        public override bool IsValid() =>
            ActorId > 0 &&
            !string.IsNullOrEmpty(Password) &&
            CopyFromCustId > 0 &&
            !string.IsNullOrEmpty(Username);
    }
}
