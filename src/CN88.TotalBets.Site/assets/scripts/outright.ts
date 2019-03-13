"use strict";
class Outright {
    $: JQueryStatic;

    stakeConditionSelectionComponent: SingleSelectionDropdownComponent;
    possTypeSelectionComponent: SingleSelectionDropdownComponent;
    leagueSelectionComponent: SingleSelectionDropdownComponent;

    constructor(
        $: JQueryStatic,
        stakeConditionSelectionComponent: SingleSelectionDropdownComponent,
        possTypeSelectionComponent: SingleSelectionDropdownComponent,
        leagueSelectionComponent: SingleSelectionDropdownComponent) {
        this.$ = $;
        this.stakeConditionSelectionComponent = stakeConditionSelectionComponent;
        this.possTypeSelectionComponent = possTypeSelectionComponent;
        this.leagueSelectionComponent = leagueSelectionComponent;
    }

    init(): void {
        this.stakeConditionSelectionComponent.render(new SingleSelectionDropdownOption("dropdown-stake-condition", "StakeCondition"))
        this.possTypeSelectionComponent.render(new SingleSelectionDropdownOption("dropdown-poss-type", "PossType"));
        this.leagueSelectionComponent.render(new SingleSelectionDropdownOption("dropdown-league", "LeagueId"));

        let tableOutright: JQuery = this.$("#tableOutright");
        tableOutright.find(".league").click(function (): void {
            tableOutright
                .find(`.team[data-leagueid="${$(this).data("leagueid")}"]`)
                .toggleClass("hide");
        });
        tableOutright.find(".stake").click(function (): void { });
    }
}

$(function (): void {
    new Outright(
        $,
        new SingleSelectionDropdownComponent($),
        new SingleSelectionDropdownComponent($),
        new SingleSelectionDropdownComponent($)).init();
});