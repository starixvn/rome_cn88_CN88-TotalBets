/// <reference path="../../../shared/scripts/core.ts" />
"use strict";
class EventTotalBets extends BaseTotalBets {
    marketSelectionComponent: SingleSelectionDropdownComponent;

    isShowMarketSelection: boolean;

    constructor(
        $: JQueryStatic,
        summaryMenu: SummaryMenu,
        stakeConditionSelectionComponent: SingleSelectionDropdownComponent,
        possTypeSelectionComponent: SingleSelectionDropdownComponent,
        marketSelectionComponent: SingleSelectionDropdownComponent,
        leagueSelectionComponent: SingleSelectionDropdownComponent) {
        super($, summaryMenu, stakeConditionSelectionComponent, possTypeSelectionComponent, leagueSelectionComponent);

        this.marketSelectionComponent = marketSelectionComponent;
    }

    init(): void {
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

        const reloadInSeconds: number = this.$("#ReloadTimeInSeconds").val() as number;
        const autoRefresh: RefreshCountDown = new RefreshCountDown($, new LinkBuilderComponent($));
        if (this.$(".btnCountDown").length) {
            const eventType = this.$("#Type");

            autoRefresh.render(new RefreshCountDownOptions("CountDownMenu", eventType.length ? `event${eventType.val()}` : "event0", reloadInSeconds, () => {
                this.onSubmit();
                this.$filterForm.trigger("submit");
            }));
        }
    }
}