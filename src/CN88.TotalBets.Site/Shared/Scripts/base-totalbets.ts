"use strict";
declare var Cleave: any;
abstract class BaseTotalBets {
    $: JQueryStatic;
    $filterForm: JQuery;
    $tblTotalBets: JQuery;
    $stake: JQuery;
    $filterToggleIcon: JQuery;
    cleaveStake: any;
    isMobile: boolean;

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
        this.$stake = this.$("#Stake");
        this.$filterToggleIcon = this.$("#filter-toggle-icon");
        this.isMobile = this.$("#IsDevice").val() == "1";
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

        this.$filterToggleIcon.on("click touchend", (e) => {
            e.preventDefault();
            let isCollapsed = this.$filterToggleIcon.hasClass("icon-filter-down");

            if (isCollapsed) {
                this.$filterForm
                    .slideDown(200);
                this.$tblTotalBets
                    .slideUp(210);
                this.$filterToggleIcon
                    .removeClass("icon-filter-down")
                    .addClass("icon-filter-up");
            }
            else {
                this.$filterForm.slideUp(200);
                this.$tblTotalBets
                    .slideDown(210);
                this.$filterToggleIcon
                    .removeClass("icon-filter-up")
                    .addClass("icon-filter-down");
            }
        });
    }

    protected onSubmit(): void {
        this.stakeConditionSelectionComponent.updateSelectedValue();
        this.updateStakeValue();
        this.possTypeSelectionComponent.updateSelectedValue();
        if (this.leagueSelectionComponent != null) {
            this.leagueSelectionComponent.updateSelectedValue();
        }
    };

    updateStakeValue(): void {
        let stake = parseFloat(this.cleaveStake.getRawValue());
        this.$stake.val(stake);
    }
}