
class CustomerListHelper {
    viewResources: ViewResources;

    constructor(viewResources: ViewResources) {
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

    private buildColumnAttributesJson(customer, levelId: number, customerIndex: number) {
        const memberId = siteInfrastructure.Role.Member.Id, isMember = levelId === memberId,
            isMaster = levelId === siteInfrastructure.Role.Master.Id,
            { CustId, UserName, StatusName, NickName, FirstName, LastName, CreatedDateStr, ReviewStatus, Count, CanEdit } = customer,
            commonAttributesJson = this.buildCommonAttributesJson(CustId, UserName, levelId);

        return [
            {
                Content: customerIndex.toString(),
                Attributes: [{ Name: 'class', Value: 'text-center' }],
                Visibile: true
            },
            {
                Content: UserName,
                Attributes: [{ Name: 'class', Value: isMember ? '' : ' link view-downline' }].concat(commonAttributesJson),
                Visibile: true
            },
            {
                Content: NickName,
                Attributes: [],
                Visibile: isMaster
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
                Content: StatusName,
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

    public buildBody(Customers, levelId: number, reportParam: ReportParam) {
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
                customerIndex, ).forEach(column => {
                    if (column.Visibile) {
                        rowContent += new ColumnWrapper(column.Content,
                            column.Attributes.map(attr => new Attribute(attr.Name, attr.Value))).ToString();
                    }
                });

            bodyContent += new RowWrapper(rowContent,
                [
                    new Attribute('class', 'row-customer' + ' RowBg' + Customers[i].StatusNameEN + (Customers[i].Status == 3 ? Customers[i].ClosedReasonName : '')),
                    new Attribute('cust-id', Customers[i].CustId)
                ]).ToString();
        }

        return bodyContent;
    }
}