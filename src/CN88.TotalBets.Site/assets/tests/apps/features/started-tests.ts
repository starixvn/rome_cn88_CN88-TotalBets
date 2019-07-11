describe("StartedTotalBets", () => {
    const jQueryFake = $;
    const summaryMenu = new SummaryMenu($, window.top);
    const stakeConditionSelectionComponent = new SingleSelectionDropdownComponent($);
    const possTypeSelectionComponent = new SingleSelectionDropdownComponent($);
    const dateTypeSelectionComponent = new SingleSelectionDropdownComponent($);
    const marketSelectionComponent = new SingleSelectionDropdownComponent($);
    const leagueSelectionComponent = new SingleSelectionDropdownComponent($);
    const autoRefresh = new RefreshCountDown($);
    let startedTotalBets: StartedTotalBets;
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
        startedTotalBets = new StartedTotalBets(
            jQueryFake,
            summaryMenu,
            stakeConditionSelectionComponent,
            possTypeSelectionComponent,
            dateTypeSelectionComponent,
            marketSelectionComponent,
            leagueSelectionComponent,
            autoRefresh);

        setFixtures(template);
    });

    it("Inherit EventTotalBets_Always_StartedTotalBets is EventTotalBets", () => {
        // assert
        expect(startedTotalBets instanceof EventTotalBets).toBeTruthy();
    });

    describe("_renderComponents_Always", () => {
        it("_Calls super.renderComponents", () => {
            // arrange
            spyOn(StartedTotalBets.prototype, "renderComponents").and.callThrough();

            // act
            startedTotalBets.renderComponents();

            // assert
            expect(StartedTotalBets.prototype.renderComponents).toHaveBeenCalled();
        });

        it("_Calls dateTypeSelectionComponent.render", () => {
            // arrange
            spyOn(StartedTotalBets.prototype, "renderComponents").and.callThrough();
            spyOn(dateTypeSelectionComponent, "render").and.callThrough();

            // act
            startedTotalBets.renderComponents();

            // assert
            expect(dateTypeSelectionComponent.render).toHaveBeenCalledWith(new SingleSelectionDropdownOption("dropdown-date-type", "DateType"));
        });
    });

    describe("_onSubmit_Always", () => {
        beforeEach(() => {
            startedTotalBets.init();
        });

        it("_Calls super.onSubmit", () => {
            // arrange
            spyOn(StartedTotalBets.prototype, "onSubmit").and.callThrough();

            // act
            startedTotalBets.onSubmit();

            // assert
            expect(StartedTotalBets.prototype.onSubmit).toHaveBeenCalled();
        });

        it("_Calls dateTypeSelectionComponent.updateSelectedValue", () => {
            // arrange
            spyOn(StartedTotalBets.prototype, "onSubmit").and.callThrough();
            spyOn(dateTypeSelectionComponent, "updateSelectedValue").and.callThrough();

            // act
            startedTotalBets.onSubmit();

            // assert
            expect(dateTypeSelectionComponent.updateSelectedValue).toHaveBeenCalled();
        });
    });
});