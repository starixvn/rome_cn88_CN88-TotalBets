describe("EventTotalBets", () => {
    const jQueryFake = $;
    const summaryMenu = new SummaryMenu($, window.top);
    const stakeConditionSelectionComponent = new SingleSelectionDropdownComponent($);
    const possTypeSelectionComponent = new SingleSelectionDropdownComponent($);
    const marketSelectionComponent = new SingleSelectionDropdownComponent($);
    const leagueSelectionComponent = new SingleSelectionDropdownComponent($);
    const autoRefresh = new RefreshCountDown($);
    let eventTotalBets: EventTotalBets;

    const template = {
        normal: `<body>
                    <span class="icon-filter-down" id="filter-toggle-icon"></span>
                    <form class="form-inline filter" role="form" id="filter-form">
                        <input id="Stake" type="text" value="0" />
                    </form>
                    <table id="tblTotalBets"></table>
                    <input id="IsShowMarketSelection" type="hidden" value="false" />
                    <input id="IsDevice" type="hidden" value="0" />
                </body>`,
        showMarketSelection: `<body>
                    <div class="dropdown-refresh">
                        <div class="btnCountDown">
                            <div class="icon-refresh"></div>
                            <div class="show-values">
                                <span id="CountDownRefresh" class="btnRefresh" title="Refresh" data-loading-text="Loading">15</span>
                                <span class="icon-chevron-down btn-show-options"></span>
                            </div>
                        </div>
                        <ul id="CountDownMenu" class="list-options hide">
                                <li id="option_10" class="count-down-option" data-value="10">
                                    10
                                </li>
                                <li id="option_20" class="count-down-option" data-value="20">
                                    20
                                </li>
                                <li id="option_30" class="count-down-option active" data-value="30">
                                    30
                                </li>
                            <li id="count-down-off" class="count-down-option" data-value="-1">Off</li>
                        </ul>
                    </div>
                    <span class="icon-filter-down" id="filter-toggle-icon"></span>
                    <form class="form-inline filter" role="form" id="filter-form">
                        <input id="Stake" type="text" value="0" />
                    </form>
                    <table id="tblTotalBets"></table>
                    <input id="IsShowMarketSelection" type="hidden" value="true" />
                    <input id="IsDevice" type="hidden" value="0" />
                    <input id="ReloadTimeInSeconds" type="hidden" value="30" />
                    <input id="Type" type="hidden" value="1" />
                </body>`
    };

    beforeEach(() => {
        eventTotalBets = new EventTotalBets(
            jQueryFake,
            summaryMenu,
            stakeConditionSelectionComponent,
            possTypeSelectionComponent,
            marketSelectionComponent,
            leagueSelectionComponent,
            autoRefresh);
    });

    it("Inherit BaseTotalBets_Always_EventTotalBets is BaseTotalBets", () => {
        // assert
        expect(eventTotalBets instanceof BaseTotalBets).toBeTruthy();
    });

    describe("_init_Always", () => {
        beforeEach(() => {
            // arrange
            setFixtures(`<body><input id="Stake" type="text" /></body>`);

            spyOn(jQueryFake, "blockUI");
            spyOn(jQueryFake, "unblockUI");
            spyOn(EventTotalBets.prototype, "cacheDOMElements").and.callThrough();
            spyOn(eventTotalBets, "renderComponents").and.callThrough();
            spyOn(eventTotalBets, "bindEvents").and.callThrough();

            // act
            eventTotalBets.init();
        });

        it("_Calls blockUI", () => {
            // assert
            expect(jQueryFake.blockUI).toHaveBeenCalled();
        });

        it("_Calls super.cacheDOMElements", () => {
            // assert
            expect(EventTotalBets.prototype.cacheDOMElements).toHaveBeenCalled();
        });

        it("_Calls renderComponents", () => {
            // assert
            expect(eventTotalBets.renderComponents).toHaveBeenCalled();
        });

        it("_Calls bindEvents", () => {
            // assert
            expect(eventTotalBets.bindEvents).toHaveBeenCalled();
        });

        it("_Calls unblockUI", () => {
            // assert
            expect(jQueryFake.unblockUI).toHaveBeenCalled();
        });
    });

    describe("_renderComponents_Always", () => {
        describe("_Always", () => {
            beforeEach(() => {
                // arrange
                setFixtures(template.normal);

                spyOn(EventTotalBets.prototype, "renderComponents").and.callThrough();

                // act
                eventTotalBets.renderComponents();
            });

            it("_Calls super.renderComponents", () => {
                // assert
                expect(EventTotalBets.prototype.renderComponents).toHaveBeenCalled();
            });

            it("_Cached isShowMarketSelection", () => {
                // assert
                expect(eventTotalBets.isShowMarketSelection).toBeDefined();
            });
        });

        describe("_IsShowMarketSelection", () => {
            it("_Render marketSelectionComponent", () => {
                // arrange
                setFixtures(template.showMarketSelection);
                spyOn(marketSelectionComponent, "render").and.callThrough();

                // act
                eventTotalBets.renderComponents();

                // assert
                expect(marketSelectionComponent.render).toHaveBeenCalled();
            });
        });
    });

    describe("_onSubmit", () => {
        it("_Always_Calls super.onSubmit", () => {
            // arrange
            setFixtures(template.normal);
            spyOn(EventTotalBets.prototype, "onSubmit").and.callThrough();
            eventTotalBets.cacheDOMElements();
            eventTotalBets.renderComponents();

            // act
            eventTotalBets.onSubmit();

            // assert
            expect(EventTotalBets.prototype.onSubmit).toHaveBeenCalled();
        });

        it("_IsShowMarketSelection_Calls marketSelectionComponent.updateSelectedValue", () => {
            // arrange
            setFixtures(template.showMarketSelection);
            spyOn(marketSelectionComponent, "updateSelectedValue").and.callThrough();
            eventTotalBets.cacheDOMElements();
            eventTotalBets.renderComponents();

            // act
            eventTotalBets.onSubmit();

            // assert
            expect(marketSelectionComponent.updateSelectedValue).toHaveBeenCalled();
        });
    });

    describe("_bindEvents", () => {
        it("_Always_Calls super.bindEvents", () => {
            // arrange
            setFixtures(template.normal);
            spyOn(EventTotalBets.prototype, "bindEvents").and.callThrough();
            eventTotalBets.cacheDOMElements();
            eventTotalBets.renderComponents();

            // act
            eventTotalBets.bindEvents();

            // assert
            expect(EventTotalBets.prototype.bindEvents).toHaveBeenCalled();
        });

        it("_Has refresh count down_Calls autoRefresh.render", () => {
            // arrange
            setFixtures(template.showMarketSelection);
            spyOn(EventTotalBets.prototype, "bindEvents").and.callThrough();
            spyOn(autoRefresh, "render").and.callThrough();
            spyOn(eventTotalBets, "doRefresh").and.callThrough();
            eventTotalBets.cacheDOMElements();
            eventTotalBets.renderComponents();

            // act
            eventTotalBets.bindEvents();

            // assert
            expect(autoRefresh.render).toHaveBeenCalledWith(new RefreshCountDownOptions("CountDownMenu", "event1", 30, eventTotalBets.doRefresh));
        })
    });

    describe("_doRefresh_Always", () => {
        beforeEach(() => {
            // arrange
            setFixtures(template.showMarketSelection);
            eventTotalBets.init();
            spyOn(eventTotalBets, "onSubmit").and.callThrough();
            spyOn(eventTotalBets.$filterForm, "trigger");

            // act
            eventTotalBets.doRefresh();
        });
        it("_Calls onSubmit", () => {
            // assert
            expect(eventTotalBets.onSubmit).toHaveBeenCalled();
        });

        it("_Calls submit filterform", () => {
            // assert
            expect(eventTotalBets.$filterForm.trigger).toHaveBeenCalledWith("submit");
        });
    });
});