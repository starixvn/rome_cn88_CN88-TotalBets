"use strict";
class BaseTotalBets {
    $: JQueryStatic;
    $filterForm: JQuery;
    $tblTotalBets: JQuery;
    $stake: JQuery;
    $filterToggleIcon: JQuery;
    cleaveStake: Cleave;
    isMobile: boolean;

    summaryMenu: SummaryMenu;
    stakeConditionSelectionComponent: SingleSelectionDropdownComponent;
    possTypeSelectionComponent: SingleSelectionDropdownComponent;
    leagueSelectionComponent: SingleSelectionDropdownComponent;

    iconFilterDownClass = "icon-filter-down";
    iconFilterUpClass = "icon-filter-up";
    filterFormSlideDuration = 200;
    tblSlideDuration = 210;

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

    public renderComponents(): void {
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
            delimiter: ',',
            numeralIntegerScale: 10
        });
    }

    public cacheDOMElements(): void {
        this.$filterForm = this.$("#filter-form");
        this.$tblTotalBets = this.$("#tblTotalBets");
        this.$stake = this.$("#Stake");
        this.$filterToggleIcon = this.$("#filter-toggle-icon");
        this.isMobile = this.$("#IsDevice").val() === "1";
    }

    public checkReady(): boolean {
        return this.$("#Stake").length > 0;
    }

    public bindEvents(): void {
        this.$tblTotalBets.find(".group").click(function (): void {
            const $this: JQuery = $(this);
            const icon: JQuery = $this.find(".group-name-wrapper").children().first();
            const children = $this.siblings(`.childrow-${$this.data("leagueid")}`);

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
            const isCollapsed = this.$filterToggleIcon.hasClass(this.iconFilterDownClass);

            if (isCollapsed) {
                this.$filterForm
                    .slideDown(this.filterFormSlideDuration);
                this.$tblTotalBets
                    .slideUp(this.tblSlideDuration);
                this.$filterToggleIcon
                    .removeClass(this.iconFilterDownClass)
                    .addClass(this.iconFilterUpClass);
            }
            else {
                this.$filterForm.slideUp(this.filterFormSlideDuration);
                this.$tblTotalBets
                    .slideDown(this.tblSlideDuration);
                this.$filterToggleIcon
                    .removeClass(this.iconFilterUpClass)
                    .addClass(this.iconFilterDownClass);
            }
        });
    }

    public onSubmit(): void {
        this.stakeConditionSelectionComponent.updateSelectedValue();
        this.updateStakeValue();
        this.possTypeSelectionComponent.updateSelectedValue();
        if (this.leagueSelectionComponent != null) {
            this.leagueSelectionComponent.updateSelectedValue();
        }
    }

    updateStakeValue(): void {
        const stake = parseFloat(this.cleaveStake.getRawValue());
        this.$stake.val(stake);
    }
}