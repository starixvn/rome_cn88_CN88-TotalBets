"use strict";
class EventTotalBets extends BaseTotalBets {
    marketSelectionComponent: SingleSelectionDropdownComponent;

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
        this.marketSelectionComponent.render(new SingleSelectionDropdownOption("dropdown-market", "Market"));
        this.bindEvents();

        this.$.unblockUI();
    }

    onSubmit(): void {
        super.onSubmit();
        this.marketSelectionComponent.updateSelectedValue();
    }

    bindEvents(): void {
        super.bindEvents();

        let buttonAutoRefresh: JQuery = this.$("#btn-refresh");

        if (buttonAutoRefresh.length) {
            buttonAutoRefresh.click(() => { this.$filterForm.trigger("submit"); });

            let autoRefresh: AutoRefresh = new AutoRefresh(this.$("#countDownContainer"), this.$filterForm);
            autoRefresh.start(6);
        }
    }
}

$(function (): void {
    new EventTotalBets(
        $,
        new SummaryMenu($),
        new SingleSelectionDropdownComponent($),
        new SingleSelectionDropdownComponent($),
        new SingleSelectionDropdownComponent($),
        new SingleSelectionDropdownComponent($)).init();
});