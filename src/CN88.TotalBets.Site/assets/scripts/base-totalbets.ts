"use strict";
abstract class BaseTotalBets {
    $: JQueryStatic;
    $filterForm: JQuery;
    $tblTotalBets: JQuery;

    summaryMenu: SummaryMenu;
    stakeConditionSelectionComponent: SingleSelectionDropdownComponent;
    possTypeSelectionComponent: SingleSelectionDropdownComponent;
    leagueSelectionComponent: SingleSelectionDropdownComponent;

    protected constructor(
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

    protected renderComponents(): void {
        this.summaryMenu.render();
        this.stakeConditionSelectionComponent.render(new SingleSelectionDropdownOption("dropdown-stake-condition", "StakeCondition"));
        this.possTypeSelectionComponent.render(new SingleSelectionDropdownOption("dropdown-poss-type", "PossType"));
        this.leagueSelectionComponent.render(new SingleSelectionDropdownOption("dropdown-league", "LeagueId"));
    }

    protected cacheDOMElements(): void {
        this.$filterForm = this.$("#filter-form");
        this.$tblTotalBets = this.$("#tblTotalBets");
    }

    protected bindEvents(): void {
        this.$tblTotalBets.find(".league").click(function (): void {
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

        this.$tblTotalBets.find(".stake").click(function (e: any): void {
            e.preventDefault();
            e.stopPropagation();
            window.open(this.href, "_blank", "width=400, height=400");
        });

        this.$filterForm.submit(() => { this.onSubmit(); });
    }

    protected onSubmit(): void {
        this.stakeConditionSelectionComponent.updateSelectedValue();
        this.possTypeSelectionComponent.updateSelectedValue();
        this.leagueSelectionComponent.updateSelectedValue();
    };
}