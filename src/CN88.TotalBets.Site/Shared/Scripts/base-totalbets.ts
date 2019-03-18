"use strict";
declare var Cleave: any;
abstract class BaseTotalBets {
    $: JQueryStatic;
    $filterForm: JQuery;
    $tblTotalBets: JQuery;

    cleaveStake: any;

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
        if (this.leagueSelectionComponent != null) {
            this.leagueSelectionComponent.render(new SingleSelectionDropdownOption("dropdown-league", "LeagueId"));
        }

        this.cleaveStake = new Cleave('#Stake', {
            numeral: true,
            numeralPositiveOnly: true,
            numeralDecimalMark: '.',
            delimiter: ','
        });
    }

    protected cacheDOMElements(): void {
        this.$filterForm = this.$("#filter-form");
        this.$tblTotalBets = this.$("#tblTotalBets");
    }

    protected bindEvents(): void {
        this.$tblTotalBets.find(".group").click(function (): void {
            let $this: JQuery = $(this);
            let icon: JQuery = $this.find(".group-name-wrapper").children().first();
            let children: any = $this.siblings(`.childrow-${$this.data("leagueid")}`);

            if (icon.hasClass("icon-chevron-down")) {
                icon.removeClass().addClass("icon-chevron-right");
                children.addClass("hide");
            } else {
                icon.removeClass().addClass("icon-chevron-down");
                children.removeClass("hide");
            }
        });

        this.$filterForm.submit(() => { this.onSubmit(); });
    }

    protected onSubmit(): void {
        this.stakeConditionSelectionComponent.updateSelectedValue();
        this.possTypeSelectionComponent.updateSelectedValue();
        if (this.leagueSelectionComponent != null) {
            this.leagueSelectionComponent.updateSelectedValue();
        }
    };
}