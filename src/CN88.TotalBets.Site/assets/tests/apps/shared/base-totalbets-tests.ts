describe("BaseTotalBets", () => {
    const jQueryFake = $;
    const summaryMenu = new SummaryMenu($, top);
    const stakeConditionSelectionComponent = new SingleSelectionDropdownComponent($);
    const possTypeSelectionComponent = new SingleSelectionDropdownComponent($);
    const leagueSelectionComponent = new SingleSelectionDropdownComponent($);
    let baseTotalbets: BaseTotalBets;
    const template = `<body>
                        <span class="icon-filter-down" id="filter-toggle-icon"></span>
                        <form class="form-inline filter" role="form" id="filter-form">
                            <input id="Stake" type="text" value="0" />
                        </form>
                        <table id="tblTotalBets"></table>
                        <input id="IsDevice" type="hidden" value="0" />
                    </body>`;

    beforeEach(() => {
        baseTotalbets = new BaseTotalBets(
            jQueryFake,
            summaryMenu,
            stakeConditionSelectionComponent,
            possTypeSelectionComponent,
            leagueSelectionComponent);
    });

    it("_cacheDOMElements_Always -> All neccessary elements have been cached", () => {
        // arrange
        setFixtures(template);

        // act
        baseTotalbets.cacheDOMElements();

        // assert
        expect(baseTotalbets.$filterForm).toBeDefined();
        expect(baseTotalbets.$tblTotalBets).toBeDefined();
        expect(baseTotalbets.$stake).toBeDefined();
        expect(baseTotalbets.$filterToggleIcon).toBeDefined();
        expect(baseTotalbets.$filterForm).toBeDefined();
        expect(baseTotalbets.isMobile).toBeDefined();
    });

    describe("_renderComponents_Always", () => {
        beforeEach(() => {
            // arrange
            setFixtures(`<input id="Stake" value=0 type="text" />`);
            spyOn(summaryMenu, "render").and.callThrough();
            spyOn(stakeConditionSelectionComponent, "render").and.callThrough();
            spyOn(possTypeSelectionComponent, "render").and.callThrough();
            spyOn(leagueSelectionComponent, "render").and.callThrough();

            // act
            baseTotalbets.renderComponents();
        });

        it("_render summaryMenu", () => {
            expect(summaryMenu.render).toHaveBeenCalled();
        });

        it("_render stakeConditionSelectionComponent", () => {
            expect(stakeConditionSelectionComponent.render).toHaveBeenCalled();
        });

        it("_render possTypeSelectionComponent", () => {
            expect(possTypeSelectionComponent.render).toHaveBeenCalled();
        });

        it("_render leagueSelectionComponent", () => {
            expect(leagueSelectionComponent.render).toHaveBeenCalled();
        });

        it("_cache cleaveStake", () => {
            expect(baseTotalbets.cleaveStake).toBeDefined();
        });
    });

    it("_checkReady_Document is not loaded yet (empty) -> Returns False", () => {
        // arrange
        setFixtures("");

        // act
        var actual = baseTotalbets.checkReady();

        // assert
        expect(actual).toBeFalsy();
    });

    it("_checkReady_Document is loaded (ready) -> Returns True", () => {
        // arrange
        setFixtures(`<input id="Stake" value=0 type="text" />`);

        // act
        var actual = baseTotalbets.checkReady();

        // assert
        expect(actual).toBeTruthy();
    });
});