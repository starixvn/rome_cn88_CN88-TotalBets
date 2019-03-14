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
}

$(function (): void {
    new Outright(
        $,
        new SummaryMenu($),
        new SingleSelectionDropdownComponent($),
        new SingleSelectionDropdownComponent($),
        new SingleSelectionDropdownComponent($)).init();
});