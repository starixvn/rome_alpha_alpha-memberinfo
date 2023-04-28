
class CustomerListHelper {
    viewResources: ViewResources;
    $: JQueryStatic;


    constructor($: JQueryStatic, viewResources: ViewResources) {
        this.$ = $;
        this.viewResources = viewResources;
    }

    private buildCommonAttributesJson(CustId, Username: string, roleId) {
        return [
            { Name: 'cust-id', Value: CustId },
            { Name: 'username', Value: Username },
            { Name: 'cust-role', Value: roleId }
        ];
    }

    private buildNoInfoContent(colspan: number) {
        return `<tr><td colspan='${colspan}' align='center' class='no-info'>${this.viewResources.NoInformationIsAvailable}</td></tr>`;
    }


    private renderCheckboxColumn = item => {
        const attrs = {
            'class': 'checkbox',
            'data-custid': item.CustId,
            'data-username': item.UserName,
        };

        return `<input type="checkbox" ${this.buildAttributes(attrs)} />`;
    }


    private renderEditColumn = item => {
        const attrs = {
            'class': 'edit icon-table-edit',
            'id': `setting-${item.CustId}`,
            'data-custid': item.CustId,
            'data-username': item.UserName,
            'title': this.$('#editSettingText').val()
        };

        return `<div ${this.buildAttributes(attrs)}/>`;
    }

    private renderStatusColumn = item => {

        const className = item.Status === 1 ? "open-status" :"";
        const attrs = {
            'class': `${className}`,
            'id': `status-${item.CustId}`,
            'data-custid': item.CustId,
            'data-username': item.UserName,
        };

        return `<div ${this.buildAttributes(attrs)}>${item.StatusName}</div>`;
    }

    private buildAttributes = attrs => this.$.map(attrs, (value, key) => `${key}='${value}'`).join(' ');

    private buildColumnAttributesJson(customer, levelId: number, actorId: number, customerIndex: number) {
        const memberId = siteInfrastructure.Role.Member.Id, isMember = levelId === memberId,
            isViewNickname = actorId - 1 == levelId,
            { CustId, UserName, StatusName, NickName, FirstName, LastName, CreatedDateStr, ReviewStatus, Count, CanEdit } = customer,
            commonAttributesJson = this.buildCommonAttributesJson(CustId, UserName, levelId);

        return [
            {
                Content: customerIndex.toString(),
                Attributes: [{ Name: 'class', Value: 'text-center' }],
                Visibile: true
            },
            {
                Content: this.renderCheckboxColumn(customer),
                Attributes: [{ Name: 'class', Value: 'center' }],
                Visibile: isMember
            },
            {
                Content: this.renderEditColumn(customer),
                Attributes: [{ Name: 'class', Value: 'center' }],
                Visibile: isMember
            },
            {
                Content: UserName,
                Attributes: [{ Name: 'class', Value: isMember ? '' : ' link view-downline' }].concat(commonAttributesJson),
                Visibile: true
            },
            {
                Content: NickName,
                Attributes: [],
                Visibile: isViewNickname
            },
            {
                Content: FirstName,
                Attributes: [],
                Visibile: true
            },
            {
                Content: LastName,
                Attributes: [],
                Visibile: true
            },
            {
                Content: this.renderStatusColumn(customer),
                Attributes: [{ Name: 'class', Value: 'text-center' }],
                Visibile: true
            },
            {
                Content: CreatedDateStr,
                Attributes: [{ Name: 'class', Value: 'text-center' }],
                Visibile: true
            },
            {
                Content: Count,
                Attributes: [{ Name: 'class', Value: 'text-right' }],
                Visibile: !isMember
            },
            {
                Content: ReviewStatus,
                Attributes: [{ Name: 'class', Value: 'text-center' }],
                Visibile: true
            },
            {
                Content: this.buildActionColumnOnDesktop(customer, levelId, CanEdit),
                Attributes: [{ Name: 'class', Value: 'text-center' }],
                //Visibile: !isMember
                Visibile: true
            }
        ];
    }

    private buildActionColumnOnDesktop(customer, levelId, canEdit) {
        const action = {
            Attributes: [{ Name: 'class', Value: 'chk icon-uncheck' }],
            Visibile: canEdit
        };
        const actionsJson = [
            {
                Content: '',
                Attributes: [{ Name: 'title', Value: "Check" }].concat(action.Attributes),
                Visibile: action.Visibile
            }
        ];

        const iconAttributesJson = [{ Name: 'data-toggle', Value: 'tooltip' }, { Name: 'data-placement', Value: 'top' }]
            .concat(this.buildCommonAttributesJson(customer.CustId, customer.UserName, levelId));

        let columnContent = '';
        actionsJson.forEach(span => {
            if (span.Visibile) {
                columnContent += new SpanWrapper(span.Content,
                    span.Attributes.concat(iconAttributesJson)
                        .map(attr => new Attribute(attr.Name, attr.Value))).ToString();
            }
        });

        return columnContent;
    }

    private buildAccountStatusCssClass(customer) {
        let className = "";
        switch (customer.Status) {
            case 2:
            case 4:
                className = customer.StatusNameEN;
                break;
            case 3:
                className = customer.StatusNameEN + customer.ClosedReasonName;
                break;
            default:
                className = customer.Locked === 0 ? customer.StatusNameEN : this.buildLockedAccessCssClass(customer.Locked);
                break;
        }

        return 'row-customer RowBg' + className;
    }

    private buildLockedAccessCssClass(locked: number) {
        switch (locked) {
            case 1: // OTP/Security Code
                return 'LockedFalsedOTPSC';
            case 2: // Password
                return 'LockedFalsePassword';
            default:
                return ''
        }
    }

    public buildBody(Customers, levelId: number, actorId: number, reportParam: ReportParam) {
        if (Customers.length === 0) {
            if (levelId === siteInfrastructure.Role.Member.Id) {
                return this.buildNoInfoContent(this.viewResources.TotalColumnMember);
            } else if (levelId === siteInfrastructure.Role.Agent.Id) {
                return this.buildNoInfoContent(this.viewResources.TotalColumnAgent);
            } else {
                return this.buildNoInfoContent(this.viewResources.TotalColumnMaster);
            }
        }

        let customerIndex = 1, bodyContent = '';

        for (let i = 0; i < Customers.length; i++ , customerIndex++) {
            let rowContent = '';
            this.buildColumnAttributesJson(
                Customers[i],
                levelId,
                actorId,
                customerIndex).forEach(column => {
                    if (column.Visibile) {
                        rowContent += new ColumnWrapper(column.Content,
                            column.Attributes.map(attr => new Attribute(attr.Name, attr.Value))).ToString();
                    }
                });

            bodyContent += new RowWrapper(rowContent,
                [
                    new Attribute('class', this.buildAccountStatusCssClass(Customers[i])),
                    new Attribute('cust-id', Customers[i].CustId)
                ]).ToString();
        }

        return bodyContent;
    }
}