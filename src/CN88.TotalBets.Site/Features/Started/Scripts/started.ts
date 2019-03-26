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
        leagueSelectionComponent: SingleSelectionDropdownComponent) {
        super($, summaryMenu, stakeConditionSelectionComponent, possTypeSelectionComponent, marketSelectionComponent, leagueSelectionComponent);
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
        new SummaryMenu($),
        new SingleSelectionDropdownComponent($),
        new SingleSelectionDropdownComponent($),
        new SingleSelectionDropdownComponent($),
        new SingleSelectionDropdownComponent($),
        new SingleSelectionDropdownComponent($)).init();
});