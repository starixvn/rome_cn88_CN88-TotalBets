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

        this.isShowMarketSelection = cn88TotalBets.convertAttributeToBool(this.$("#IsShowMarketSelection").val())
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

        let buttonAutoRefresh: JQuery = this.$("#btn-refresh");
        let reloadInSeconds: number = <number>this.$("#ReloadTimeInSeconds").val();

        if (buttonAutoRefresh.length) {
            buttonAutoRefresh.click(() => { this.$filterForm.trigger("submit"); });

            let autoRefresh: AutoRefresh = new AutoRefresh(
                this.$("#countDownContainer"),
                this.$filterForm,
                this.$("#chb-switch"),
                this.$("#IsAutoRefresh"),
                reloadInSeconds);
        }
    }
}