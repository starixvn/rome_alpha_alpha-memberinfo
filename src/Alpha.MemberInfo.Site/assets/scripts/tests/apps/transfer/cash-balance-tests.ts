describe("CashBalanceTransfer", () => {
    const jQueryFake = $;
    let topWindow: any;
    const amountFrom: number = -194585.88;
    const amountTo: number = 248417.24;
    let cashBalance: CashBalance;

    let template = {
        normal: `<body>
                    <div class="container-fluid cash-balance">
                        <div class="section">
                            <div class="row">
                                <form id="form-transfer" action="~/transfer/cashbalance" method="post">
                                    <div class="form-group">
                                        <label for="amount">Amount</label>
                                        <input type="text" class="textbox hasBorder text-right" id="amount" maxlength="20" autocomplete="off" autofocus>
                                    </div>
                                    <div class="form-group">
                                        <button type="submit" class="btn-submit">Transfer</button>
                                    </div>
                                </form>
                            </div>
                            <div class="row">
                                <div class="col-xs-12 note">
                                    You can transfer from -194,585.88 to 248,417.24
                                </div>
                            </div>
                        </div>
                    </div>
                    <input type="hidden" id="custId" value="1176083" />
                    <input type="hidden" id="availableAmountFrom" value="-194585.88" />
                    <input type="hidden" id="availableAmountTo" value="248417.24" />
                </body>`
    };

    beforeEach(() => {
        topWindow = {
        };

        cashBalance = new CashBalance(jQueryFake, topWindow);

        ////spyOn(singleSelectionDropdownComponent, "render");
    });

    describe("_init", () => {
        beforeEach(() => {
            // arrange
            setFixtures(template.normal);
            spyOn(cashBalance, "cacheDOMElements");
            spyOn(cashBalance, "registerEvents");

            // act
            cashBalance.init();
        });

        it("_Always_Calls cacheDOMElements", () => {
            expect(cashBalance.cacheDOMElements).toHaveBeenCalled();
        });

        it("_Always_Calls registerEvents", () => {
            expect(cashBalance.registerEvents).toHaveBeenCalled();
        });
    });
});