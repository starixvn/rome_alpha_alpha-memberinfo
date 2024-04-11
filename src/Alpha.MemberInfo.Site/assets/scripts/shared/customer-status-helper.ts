const CustomerClosedReason = {
    ByAdmin: 1,
    ByUpline: 2,
    ByArchive: 4
};

const CustomerLockedAccess = {
    FalseOTPSC: 1,
    FalsePassword: 2
};

const CustomerStatusClasses = {
    Closed: [
        {
            Id: CustomerClosedReason.ByAdmin,
            Css: 'closed-byadmin'
        },
        {
            Id: CustomerClosedReason.ByUpline,
            Css: 'closed-byupline'
        },
        {
            Id: CustomerClosedReason.ByArchive,
            Css: 'closed-byArchive'
        }
    ],
    Suspended: {
        Css: 'RowBgSuspended'
    },
    Disabled: {
        Css: 'RowBgDisabled'
    },
    Enabled: {
        Css: 'enabled'
    },
    Locked: [
        {
            Id: CustomerLockedAccess.FalseOTPSC,
            Css: 'locked-falseOTPSC'
        },
        {
            Id: CustomerLockedAccess.FalsePassword,
            Css: 'locked-falsePassword'
        }
    ]
};

class CustomerStatusHelper {
    static getRowClassName(data) {
        if (data.Status === "Disabled") {
            return CustomerStatusClasses.Disabled.Css;
        }

        if (data.Status === "Suspended") {
            return CustomerStatusClasses.Suspended.Css;
        }

        if (data.ClosedReason !== 0) {
            const closed = CustomerStatusClasses.Closed.filter(closedReason => data.ClosedReason === closedReason.Id);
            return (closed && closed.length) ? closed[0].Css : "";
        }

        if (data.Locked !== 0) {
            const locked = CustomerStatusClasses.Locked.filter(locked => data.Locked === locked.Id);
            return (locked && locked.length) ? locked[0].Css : "";
        }

        return "";
    }
}