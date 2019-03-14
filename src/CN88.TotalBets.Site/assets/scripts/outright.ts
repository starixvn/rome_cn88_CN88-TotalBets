"use strict";
class Outright {
    $: JQueryStatic;

    summaryMenu: SummaryMenu;
    stakeConditionSelectionComponent: SingleSelectionDropdownComponent;
    possTypeSelectionComponent: SingleSelectionDropdownComponent;
    leagueSelectionComponent: SingleSelectionDropdownComponent;

    constructor(
        $: JQueryStatic,
        summaryMenu: SummaryMenu,
        stakeConditionSelectionComponent: SingleSelectionDropdownComponent,
        possTypeSelectionComponent: SingleSelectionDropdownComponent,
        leagueSelectionComponent: SingleSelectionDropdownComponent) {
        this.$ = $;
        this.summaryMenu = summaryMenu;
        this.stakeConditionSelectionComponent = stakeConditionSelectionComponent;
        this.possTypeSelectionComponent = possTypeSelectionComponent;
        this.leagueSelectionComponent = leagueSelectionComponent;
    }

    init(): void {
        this.summaryMenu.render();
        this.stakeConditionSelectionComponent.render(new SingleSelectionDropdownOption("dropdown-stake-condition", "StakeCondition"));
        this.possTypeSelectionComponent.render(new SingleSelectionDropdownOption("dropdown-poss-type", "PossType"));
        this.leagueSelectionComponent.render(new SingleSelectionDropdownOption("dropdown-league", "LeagueId"));

        this.bindEvents();
    }

    private bindEvents(): void {
        let tableOutright: JQuery = this.$("#tableOutright");

        tableOutright.find(".league").click(function (): void {
            let $this: JQuery = $(this);
            let icon: JQuery = $this.find(".league-name-wrapper").children().first();
            let children: any = $this.siblings(`.childrow-${$this.data("leagueid")}`);

            if (icon.hasClass("icon-chevron-down")) {
                icon.removeClass().addClass("icon-chevron-right");
                children.addClass("hide");
            } else {
                icon.removeClass().addClass("icon-chevron-down");
                children.removeClass("hide");
            }
        });

        tableOutright.find(".stake").click(function (e: any): void {
            e.preventDefault();
            e.stopPropagation();
            window.open(this.href, "_blank", "width=400, height=400");
        });

        this.$("#filter-form").submit(this.onSubmit);
    }

    onSubmit = () => {
        this.stakeConditionSelectionComponent.updateSelectedValue();
        this.possTypeSelectionComponent.updateSelectedValue();
        this.leagueSelectionComponent.updateSelectedValue();
    };
}

$(function (): void {
    new Outright(
        $,
        new SummaryMenu($),
        new SingleSelectionDropdownComponent($),
        new SingleSelectionDropdownComponent($),
        new SingleSelectionDropdownComponent($)).init();
});