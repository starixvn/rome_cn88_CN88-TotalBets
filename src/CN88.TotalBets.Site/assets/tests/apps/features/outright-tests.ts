describe("Outright", () => {
    const jQueryFake = $;
    const summaryMenu = new SummaryMenu($, window.top);
    const stakeConditionSelectionComponent = new SingleSelectionDropdownComponent($);
    const possTypeSelectionComponent = new SingleSelectionDropdownComponent($);
    const leagueSelectionComponent = new SingleSelectionDropdownComponent($);
    let outright: Outright;
    const template = `<body>
                    <span class="icon-filter-down" id="filter-toggle-icon"></span>
                    <form class="form-inline filter" role="form" id="filter-form">
                        <input id="Stake" type="text" value="0" />
                    </form>
                    <table id="tblTotalBets"></table>
                    <input id="IsShowMarketSelection" type="hidden" value="false" />
                    <input id="IsDevice" type="hidden" value="0" />
                </body>`;

    beforeEach(() => {
        outright = new Outright(
            jQueryFake,
            summaryMenu,
            stakeConditionSelectionComponent,
            possTypeSelectionComponent,
            leagueSelectionComponent);

        setFixtures(template);
    });

    it("Inherit BaseTotalBets_Always_Outright is BaseTotalBets", () => {
        // assert
        expect(outright instanceof BaseTotalBets).toBeTruthy();
    });

    describe("_init_Always", () => {
        beforeEach(() => {
            // arrange

            spyOn(jQueryFake, "blockUI");
            spyOn(jQueryFake, "unblockUI");
            spyOn(Outright.prototype, "cacheDOMElements").and.callThrough();
            spyOn(Outright.prototype, "renderComponents").and.callThrough();
            spyOn(outright, "bindEvents").and.callThrough();

            // act
            outright.init();
        });

        it("_Calls blockUI", () => {
            // assert
            expect(jQueryFake.blockUI).toHaveBeenCalled();
        });

        it("_Calls super.cacheDOMElements", () => {
            // assert
            expect(Outright.prototype.cacheDOMElements).toHaveBeenCalled();
        });

        it("_Calls renderComponents", () => {
            // assert
            expect(Outright.prototype.renderComponents).toHaveBeenCalled();
        });

        it("_Calls bindEvents", () => {
            // assert
            expect(outright.bindEvents).toHaveBeenCalled();
        });

        it("_Calls unblockUI", () => {
            // assert
            expect(jQueryFake.unblockUI).toHaveBeenCalled();
        });
    });

    it("_bindEvents_Always_Calls super.bindEvents", () => {
        // arrange
        spyOn(Outright.prototype, "bindEvents").and.callThrough();
        outright.init();

        // act
        outright.bindEvents();

        // assert
        expect(Outright.prototype.bindEvents).toHaveBeenCalled();
    });
});