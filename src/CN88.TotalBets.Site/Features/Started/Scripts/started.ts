"use strict";
class StartedTotalBets extends EventTotalBets {
    dateTypeSelectionComponent: SingleSelectionDropdownComponent;

    constructor(
        $: JQueryStatic,
        summaryMenu: SummaryMenu,
        stakeConditionSelectionComponent: SingleSelectionDropdownComponent,
        possTypeSelectionComponent: SingleSelectionDropdownComponent,
        dateTypeSelectionComponent: SingleSelectionDropdownComponent,
        marketSelectionComponent: SingleSelectionDropdownComponent,
        leagueSelectionComponent: SingleSelectionDropdownComponent,
        autoRefresh: RefreshCountDown) {
        super($, summaryMenu, stakeConditionSelectionComponent, possTypeSelectionComponent, marketSelectionComponent, leagueSelectionComponent, autoRefresh);
        this.dateTypeSelectionComponent = dateTypeSelectionComponent;
    }

    renderComponents(): void {
        super.renderComponents();
        this.dateTypeSelectionComponent.render(new SingleSelectionDropdownOption("dropdown-date-type", "DateType"));
    }

    onSubmit(): void {
        super.onSubmit();
        this.dateTypeSelectionComponent.updateSelectedValue();
    }
}

$(function (): void {
    new StartedTotalBets(
        $,
        new SummaryMenu($, top),
        new SingleSelectionDropdownComponent($),
        new SingleSelectionDropdownComponent($),
        new SingleSelectionDropdownComponent($),
        new SingleSelectionDropdownComponent($),
        new SingleSelectionDropdownComponent($),
        new RefreshCountDown($)).init();
});