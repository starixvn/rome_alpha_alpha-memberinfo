namespace Alpha.MemberInfo.Features.DormantAccountList.Models
{
    using System;

    public class DormantAccountEntity
    {
        public DormantAccountEntity()
        {
        }

        public DormantAccountEntity(
            int custId,
            string username,
            byte roleId,
            byte statusId,
            byte stageId,
            decimal? toBeClearedGivenCredit,
            decimal? clearedGivenCredit,
            DateTime? lastLogged,
            DateTime? closedDate,
            DateTime? disabledDate,
            DateTime? settingDeletedDate,
            DateTime? accountDeletedDate,
            bool isWarningTicket,
            int rowNumber)
        {
            CustId = custId;
            Username = username;
            RoleId = roleId;
            StatusId = statusId;
            StageId = stageId;
            ToBeClearedGivenCredit = toBeClearedGivenCredit;
            ClearedGivenCredit = clearedGivenCredit;
            LastLogged = lastLogged;
            ClosedDate = closedDate;
            DisabledDate = disabledDate;
            SettingDeletedDate = settingDeletedDate;
            AccountDeletedDate = accountDeletedDate;
            IsWarningTicket = isWarningTicket;
            RowNumber = rowNumber;
        }

        public int CustId { get; }

        public string Username { get; }

        public byte RoleId { get; }

        public byte StatusId { get; }

        public byte StageId { get; }

        public decimal? ToBeClearedGivenCredit { get; }

        public decimal? ClearedGivenCredit { get; }

        public DateTime? LastLogged { get; }

        public DateTime? ClosedDate { get; }

        public DateTime? DisabledDate { get; }

        public DateTime? SettingDeletedDate { get; }

        public DateTime? AccountDeletedDate { get; }

        public bool IsWarningTicket { get; }

        public int RowNumber { get; }

    }
}
