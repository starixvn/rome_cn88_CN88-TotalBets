/// <reference path="../../../assets/scripts/base-totalbets.ts" />

class ViewByBetType extends BaseTotalBets {
    marketSelectionComponent: SingleSelectionDropdownComponent;

    constructor(
        $: JQueryStatic,
        summaryMenu: SummaryMenu,
        stakeConditionSelectionComponent: SingleSelectionDropdownComponent,
        possTypeSelectionComponent: SingleSelectionDropdownComponent,
        marketSelectionComponent: SingleSelectionDropdownComponent) {
        super($, summaryMenu, stakeConditionSelectionComponent, possTypeSelectionComponent, null);

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
        //this.marketSelectionComponent.updateSelectedValue();
    }

    bindEvents(): void {
        super.bindEvents();

        let buttonAutoRefresh: JQuery = this.$("#btn-refresh");

        if (buttonAutoRefresh.length) {
            buttonAutoRefresh.click(() => { this.$filterForm.trigger("submit"); });

            let autoRefresh: AutoRefresh = new AutoRefresh(this.$("#countDownContainer"), this.$filterForm);
            autoRefresh.start(300);
        }
    }
}

$(function (): void {
    new ViewByBetType(
        $,
        new SummaryMenu($),
        new SingleSelectionDropdownComponent($),
        new SingleSelectionDropdownComponent($),
        new SingleSelectionDropdownComponent($)).init();
});