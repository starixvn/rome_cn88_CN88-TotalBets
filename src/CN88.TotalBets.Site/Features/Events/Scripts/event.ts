/// <reference path="../../../shared/scripts/core.ts" />
"use strict";
class EventTotalBets extends BaseTotalBets {
    marketSelectionComponent: SingleSelectionDropdownComponent;
    autoRefresh: RefreshCountDown;

    isShowMarketSelection: boolean;

    constructor(
        $: JQueryStatic,
        summaryMenu: SummaryMenu,
        stakeConditionSelectionComponent: SingleSelectionDropdownComponent,
        possTypeSelectionComponent: SingleSelectionDropdownComponent,
        marketSelectionComponent: SingleSelectionDropdownComponent,
        leagueSelectionComponent: SingleSelectionDropdownComponent,
        autoRefresh: RefreshCountDown) {
        super($, summaryMenu, stakeConditionSelectionComponent, possTypeSelectionComponent, leagueSelectionComponent);

        this.marketSelectionComponent = marketSelectionComponent;
        this.autoRefresh = autoRefresh;
    }

    init(): void {
        if (!this.checkReady()) {
            return;
        }

        this.$.blockUI();

        this.cacheDOMElements();
        this.renderComponents();
        this.bindEvents();

        this.$.unblockUI();
    }

    renderComponents(): void {
        super.renderComponents();

        this.isShowMarketSelection = cn88TotalBets.convertAttributeToBool(this.$("#IsShowMarketSelection").val());
        if (this.isShowMarketSelection) {
            this.marketSelectionComponent.render(new SingleSelectionDropdownOption("dropdown-market", "Market"));
        }
    }

    onSubmit(): void {
        super.onSubmit();

        if (this.isShowMarketSelection) {
            this.marketSelectionComponent.updateSelectedValue();
        }
    }

    bindEvents(): void {
        super.bindEvents();

        const reloadInSeconds = parseInt(this.$("#ReloadTimeInSeconds").val());
        if (this.$(".btnCountDown").length) {
            const eventType = this.$("#Type");

            this.autoRefresh.render(new RefreshCountDownOptions("CountDownMenu", eventType.length ? `event${eventType.val()}` : "event0", reloadInSeconds, this.doRefresh));
        }
    }

    doRefresh = () => {
        this.onSubmit();
        this.$filterForm.trigger("submit");
    };
}