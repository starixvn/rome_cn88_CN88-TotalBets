"use strict";
class Outright extends BaseTotalBets {
    $: JQueryStatic;

    constructor(
        $: JQueryStatic,
        summaryMenu: SummaryMenu,
        stakeConditionSelectionComponent: SingleSelectionDropdownComponent,
        possTypeSelectionComponent: SingleSelectionDropdownComponent,
        leagueSelectionComponent: SingleSelectionDropdownComponent) {
        super($, summaryMenu, stakeConditionSelectionComponent, possTypeSelectionComponent, leagueSelectionComponent);
    }

    init(): void {
        this.$.blockUI();

        this.renderComponents();
        this.cacheDOMElements();
        this.bindEvents();

        this.$.unblockUI();
    }

    bindEvents(): void {
        super.bindEvents();
        var me = this;

        this.$tblTotalBets.find(".stake").click(function (e: any): void {
            e.preventDefault();
            e.stopPropagation();
            if (me.isMobile) {
                window.open(this.href, "_blank");
            }
            else {
                window.open(this.href, "WagerDetails", "width=1000, height=500");
            }
        });
    }
}

$(function (): void {
    new Outright(
        $,
        new SummaryMenu($),
        new SingleSelectionDropdownComponent($),
        new SingleSelectionDropdownComponent($),
        new SingleSelectionDropdownComponent($)).init();
});