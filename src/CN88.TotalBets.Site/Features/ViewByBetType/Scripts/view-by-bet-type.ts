/// <reference path="../../../shared/scripts/base-totalbets.ts" />

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
            autoRefresh.start(5);
        }

        this.summaryMenu.$summaryMenuItems.find(".league").click(function (): void {
            let $this: JQuery = $(this);
            let icon = $this.children().last();
            let child = $this.siblings(`.child-${$this.attr("id")}`);

            if (icon.hasClass("icon-chevron-down")) {
                icon.removeClass().addClass("icon-chevron-right");
                child.addClass("hide");
            } else {
                icon.removeClass().addClass("icon-chevron-down");
                child.removeClass("hide");
            }
        });
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