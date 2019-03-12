"use strict";
class Outright {
    $: JQueryStatic;

    stakeConditionSelectionComponent: SingleSelectionDropdownComponent;
    possTypeSelectionComponent: SingleSelectionDropdownComponent;
    leagueSelectionComponent: SingleSelectionDropdownComponent;

    constructor($: JQueryStatic) {
        this.$ = $;
    }

    init(): void {
        this.stakeConditionSelectionComponent.render(new SingleSelectionDropdownOption("dropdown-stake-condition", "StakeCondition"))
        this.possTypeSelectionComponent.render(new SingleSelectionDropdownOption("dropdown-poss-type", "PossType"));
        this.leagueSelectionComponent.render(new SingleSelectionDropdownOption("dropdown-league", "LeagueId"));
    }
}

$(function (): void {
    new Outright($).init();
});