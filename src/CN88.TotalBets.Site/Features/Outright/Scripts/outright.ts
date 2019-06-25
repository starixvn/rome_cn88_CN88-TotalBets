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
        if (!this.checkReady()) {
            return;
        }

        this.$.blockUI();

        this.renderComponents();
        this.cacheDOMElements();
        this.bindEvents();

        this.$.unblockUI();
    }

    bindEvents(): void {
        super.bindEvents();

        this.$tblTotalBets.find(".stake").click((e: JQueryEventObject) => {
            e.preventDefault();
            e.stopPropagation();
            const url = this.$(e.currentTarget).attr("href");

            if (this.isMobile) {
                window.open(url, "_blank");
            }
            else {
                window.open(url, "WagerDetails", "width=1000, height=500");
            }
        });
    }
}

$(function (): void {
    new Outright(
        $,
        new SummaryMenu($, top),
        new SingleSelectionDropdownComponent($),
        new SingleSelectionDropdownComponent($),
        new SingleSelectionDropdownComponent($)).init();
});