describe("ViewByBetType ", () => {
    const jQueryFake = $;
    const topWindow = top;
    const summaryMenu = new SummaryMenu($, window.top);
    const stakeConditionSelectionComponent = new SingleSelectionDropdownComponent($);
    const possTypeSelectionComponent = new SingleSelectionDropdownComponent($);
    const marketSelectionComponent = new SingleSelectionDropdownComponent($);
    const autoRefresh = new RefreshCountDown($);
    let viewByBetType: ViewByBetType;

    const template = `<body>
                    <span class="icon-filter-down" id="filter-toggle-icon"></span>
                    <form class="form-inline filter" role="form" id="filter-form">
                        <input id="Stake" type="text" value="0" />
                    </form>
                    <table id="tblTotalBets"></table>
                    <input id="IsDevice" type="hidden" value="0" />
                    <input id="IsShowMarketSelection" type="hidden" value="true" />
                    <input id="ReloadTimeInSeconds" type="hidden" value="30" />
                </body>`;

    beforeEach(() => {
        viewByBetType = new ViewByBetType(
            jQueryFake,
            topWindow,
            summaryMenu,
            stakeConditionSelectionComponent,
            possTypeSelectionComponent,
            marketSelectionComponent,
            autoRefresh);

        setFixtures(template);
    });

    it("Inherit BaseTotalBets_Always_ViewByBetType is BaseTotalBets", () => {
        // assert
        expect(viewByBetType instanceof BaseTotalBets).toBeTruthy();
    });

    describe("_init_Always", () => {
        beforeEach(() => {
            // arrange
            spyOn(jQueryFake, "blockUI");
            spyOn(jQueryFake, "unblockUI");
            spyOn(ViewByBetType.prototype, "cacheDOMElements").and.callThrough();
            spyOn(viewByBetType, "renderComponents").and.callThrough();
            spyOn(viewByBetType, "bindEvents").and.callThrough();

            // act
            viewByBetType.init();
        });

        it("_Calls blockUI", () => {
            // assert
            expect(jQueryFake.blockUI).toHaveBeenCalled();
        });

        it("_Calls super.cacheDOMElements", () => {
            // assert
            expect(ViewByBetType.prototype.cacheDOMElements).toHaveBeenCalled();
        });

        it("_Calls renderComponents", () => {
            // assert
            expect(viewByBetType.renderComponents).toHaveBeenCalled();
        });

        it("_Calls bindEvents", () => {
            // assert
            expect(viewByBetType.bindEvents).toHaveBeenCalled();
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

                spyOn(ViewByBetType.prototype, "renderComponents").and.callThrough();

                // act
                viewByBetType.renderComponents();
            });

            it("_Calls super.renderComponents", () => {
                // assert
                expect(ViewByBetType.prototype.renderComponents).toHaveBeenCalled();
            });

            it("_Cached isMobile", () => {
                // assert
                expect(viewByBetType.isMobile).toBeDefined();
            });
        });

        describe("_IsShowMarketSelection", () => {
            it("_Render marketSelectionComponent", () => {
                // arrange
                spyOn(marketSelectionComponent, "render").and.callThrough();

                // act
                viewByBetType.renderComponents();

                // assert
                expect(marketSelectionComponent.render).toHaveBeenCalledWith(new SingleSelectionDropdownOption("dropdown-market", "Market"));
            });
        });
    });

    describe("_onSubmit_Always", () => {
        beforeEach(() => {
            // arrange
            spyOn(ViewByBetType.prototype, "onSubmit").and.callThrough();
            spyOn(marketSelectionComponent, "updateSelectedValue").and.callThrough();

            viewByBetType.init();

            // act
            viewByBetType.onSubmit();
        });

        it("_Calls super.onSubmit", () => {
            // assert
            expect(ViewByBetType.prototype.onSubmit).toHaveBeenCalled();
        });

        it("_Calls marketSelectionComponent.updateSelectedValue", () => {
            // assert
            expect(marketSelectionComponent.updateSelectedValue).toHaveBeenCalled();
        });
    });

    describe("_bindEvents_Always", () => {
        beforeEach(() => {
            // arrange
            spyOn(ViewByBetType.prototype, "bindEvents").and.callThrough();
            spyOn(autoRefresh, "render");
            spyOn(viewByBetType, "doRefresh");
            viewByBetType.cacheDOMElements();

            // act
            viewByBetType.bindEvents();
        });

        it("_Calls super.bindEvents", () => {
            // assert
            expect(ViewByBetType.prototype.bindEvents).toHaveBeenCalled();
        });

        it("_Calls autoRefresh.render", () => {
            // assert
            expect(autoRefresh.render).toHaveBeenCalledWith(new RefreshCountDownOptions("CountDownMenu", "", 30, viewByBetType.doRefresh));
        })
    });

    describe("_doRefresh_Always", () => {
        beforeEach(() => {
            // arrange
            viewByBetType.init();
            spyOn(viewByBetType, "onSubmit").and.callThrough();
            spyOn(viewByBetType.$filterForm, "trigger");

            // act
            viewByBetType.doRefresh();
        });
        it("_Calls onSubmit", () => {
            // assert
            expect(viewByBetType.onSubmit).toHaveBeenCalled();
        });

        it("_Calls submit filterform", () => {
            // assert
            expect(viewByBetType.$filterForm.trigger).toHaveBeenCalledWith("submit");
        });
    });
});