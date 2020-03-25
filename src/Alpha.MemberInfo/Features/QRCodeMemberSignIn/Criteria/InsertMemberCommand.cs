namespace Alpha.MemberInfo.Features.QRCodeMemberSignIn.Criteria
{
    using Fanex.Data;
    using Fanex.Data.Repository;

    public class InsertMemberCommand : NonQueryCommand
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
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
        public string UGroup { get; set; }
        public bool Closed { get; set; }

        public decimal APositionTaking_1 { get; set; }
        public decimal APositionTaking_2 { get; set; }
        public decimal APositionTaking_3 { get; set; }
        public decimal APositionTaking_4 { get; set; }
        public decimal APositionTaking_5 { get; set; }
        public decimal APositionTaking_6 { get; set; }
        public decimal APositionTaking_7 { get; set; }
        public decimal APositionTaking_8 { get; set; }
        public decimal APositionTaking_9 { get; set; }
        public decimal APositionTaking_10 { get; set; }

        public decimal V2APositionTaking_10 { get; set; }
        public decimal V3APositionTaking_10 { get; set; }
        public decimal V5APositionTaking_10 { get; set; }
        public decimal V8APositionTaking_10 { get; set; }
        public decimal V10APositionTaking_10 { get; set; }
        public decimal V11APositionTaking_10 { get; set; }
        public decimal V99APositionTaking_10 { get; set; }

        public decimal APositionTaking_9999 { get; set; }

        public decimal ALivePositionTaking_1 { get; set; }
        public decimal ALivePositionTaking_3 { get; set; }
        public decimal ALivePositionTaking_7 { get; set; }
        public decimal ALivePositionTaking_8 { get; set; }

        public decimal V2APositionTaking_1 { get; set; }
        public decimal V2APositionTaking_2 { get; set; }
        public decimal V2APositionTaking_3 { get; set; }
        public decimal V2APositionTaking_7 { get; set; }
        public decimal V2APositionTaking_8 { get; set; }
        public decimal V2APositionTaking_20 { get; set; }
        public decimal V2APositionTaking_9999 { get; set; }

        public decimal V3APositionTaking_1 { get; set; }
        public decimal V3APositionTaking_2 { get; set; }
        public decimal V3APositionTaking_3 { get; set; }
        public decimal V3APositionTaking_20 { get; set; }

        public decimal V5APositionTaking_1 { get; set; }
        public decimal V5APositionTaking_2 { get; set; }
        public decimal V5APositionTaking_3 { get; set; }
        public decimal V5APositionTaking_20 { get; set; }
        public decimal V5APositionTaking_9999 { get; set; }

        public decimal V8APositionTaking_1 { get; set; }
        public decimal V8APositionTaking_3 { get; set; }
        public decimal V8APositionTaking_20 { get; set; }

        public decimal V10APositionTaking_1 { get; set; }
        public decimal V10APositionTaking_2 { get; set; }
        public decimal V10APositionTaking_3 { get; set; }
        public decimal V10APositionTaking_20 { get; set; }

        public decimal V11APositionTaking_1 { get; set; }
        public decimal V11APositionTaking_20 { get; set; }

        public decimal V99APositionTaking_1 { get; set; }
        public decimal V99APositionTaking_2 { get; set; }
        public decimal V99APositionTaking_3 { get; set; }
        public decimal V99APositionTaking_9 { get; set; }
        public decimal V99APositionTaking_20 { get; set; }
        public decimal V99APositionTaking_9999 { get; set; }

        public decimal V161APositionTaking_81 { get; set; }
        public decimal V161APositionTaking_83 { get; set; }
        public decimal V161APositionTaking_86 { get; set; }
        public decimal V161APositionTaking_88 { get; set; }

        public decimal V161ALivePositionTaking_85 { get; set; }
        public decimal V161ALivePositionTaking_86 { get; set; }
        public decimal V161ALivePositionTaking_87 { get; set; }
        public decimal V161ALivePositionTaking_89 { get; set; }
        public decimal V161ALivePositionTaking_90 { get; set; }

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
