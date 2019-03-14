"use strict";
class EventTotalBets {
    $: JQueryStatic;

    summaryMenu: SummaryMenu;
    stakeConditionSelectionComponent: SingleSelectionDropdownComponent;
    possTypeSelectionComponent: SingleSelectionDropdownComponent;
    marketSelectionComponent: SingleSelectionDropdownComponent;
    leagueSelectionComponent: SingleSelectionDropdownComponent;

    constructor(
        $: JQueryStatic,
        summaryMenu: SummaryMenu,
        stakeConditionSelectionComponent: SingleSelectionDropdownComponent,
        possTypeSelectionComponent: SingleSelectionDropdownComponent,
        marketSelectionComponent: SingleSelectionDropdownComponent,
        leagueSelectionComponent: SingleSelectionDropdownComponent) {
        this.$ = $;
        this.summaryMenu = summaryMenu;
        this.stakeConditionSelectionComponent = stakeConditionSelectionComponent;
        this.possTypeSelectionComponent = possTypeSelectionComponent;
        this.marketSelectionComponent = marketSelectionComponent;
        this.leagueSelectionComponent = leagueSelectionComponent;
    }

    init(): void {
        this.summaryMenu.render();
        this.stakeConditionSelectionComponent.render(new SingleSelectionDropdownOption("dropdown-stake-condition", "StakeCondition"));
        this.possTypeSelectionComponent.render(new SingleSelectionDropdownOption("dropdown-poss-type", "PossType"));
        this.marketSelectionComponent.render(new SingleSelectionDropdownOption("dropdown-market", "Market"));
        this.leagueSelectionComponent.render(new SingleSelectionDropdownOption("dropdown-league", "LeagueId"));

        this.bindEvents();
    }

    private bindEvents(): void {
        let self: any = this;
        let tableEvents: JQuery = this.$("#tableEvents");

        tableEvents.find(".league").click(function (): void {
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

        tableEvents.find(".stake").click(function (e: any): void {
            e.preventDefault();
            e.stopPropagation();
            window.open(this.href, "_blank", "width=400, height=400");
        });

        this.$("#filter-form").submit(function (): void {
            self.stakeConditionSelectionComponent.updateSelectedValue();
            self.possTypeSelectionComponent.updateSelectedValue();
            self.marketSelectionComponent.updateSelectedValue();
            self.leagueSelectionComponent.updateSelectedValue();
        });
    }
}

$(function (): void {
    new EventTotalBets(
        $,
        new SummaryMenu($),
        new SingleSelectionDropdownComponent($),
        new SingleSelectionDropdownComponent($),
        new SingleSelectionDropdownComponent($),
        new SingleSelectionDropdownComponent($)).init();
});