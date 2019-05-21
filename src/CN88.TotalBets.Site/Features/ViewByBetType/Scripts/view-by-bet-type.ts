/// <reference path="../../../shared/scripts/base-totalbets.ts" />
/// <reference path="../../../shared/scripts/core.ts" />

class ViewByBetType extends BaseTotalBets {
    marketSelectionComponent: SingleSelectionDropdownComponent;
    isMobile: boolean;

    constructor(
        $: JQueryStatic,
        summaryMenu: SummaryMenu,
        stakeConditionSelectionComponent: SingleSelectionDropdownComponent,
        dateTypeSelectionComponent: SingleSelectionDropdownComponent,
        possTypeSelectionComponent: SingleSelectionDropdownComponent,
        marketSelectionComponent: SingleSelectionDropdownComponent) {
        super($, summaryMenu, stakeConditionSelectionComponent, possTypeSelectionComponent, null);

        this.marketSelectionComponent = marketSelectionComponent;
    }

    init(): void {
        this.$.blockUI();

        this.cacheDOMElements();
        this.renderComponents();
        this.bindEvents();

        this.$.unblockUI();
    }

    renderComponents(): void {
        super.renderComponents();
        if (cn88TotalBets.convertAttributeToBool($("#IsShowMarketSelection").val())) {
            this.marketSelectionComponent.render(new SingleSelectionDropdownOption("dropdown-market", "Market"));
        }

        this.isMobile = this.$("#IsDevice").val() == "1";
    }

    onSubmit(): void {
        super.onSubmit();
        this.marketSelectionComponent.updateSelectedValue();
    }

    bindEvents(): void {
        super.bindEvents();

        let reloadInSeconds: number = <number>this.$("#ReloadTimeInSeconds").val();
        let autoRefresh: RefreshCountDown = new RefreshCountDown($, new LinkBuilderComponent($));
        autoRefresh.render(new RefreshCountDownOptions("CountDownMenu", reloadInSeconds, () => {
            this.onSubmit();
            this.$filterForm.trigger("submit");
        }));

        this.summaryMenu.$summaryMenuItems.find(".league-wrapper").click(function (): void {
            let $this: JQuery = $(this);
            let icon = $this.children().last();
            let child = $this.siblings(`.child-${$this.attr("id")}`);

            if (icon.hasClass("icon-chevron-down")) {
                icon.removeClass().addClass("icon-chevron-right");
                child.addClass("hide");
            } else {
                icon.removeClass().addClass("icon-chevron-down");
                child.removeClass("hide");
            }
        });

        let me = this;
        this.$tblTotalBets.find(".stake-link").click(function (e: any): void {
            e.preventDefault();
            e.stopPropagation();
            if (me.isMobile) {
                window.open(this.href, "_blank");
            }
            else {
                window.open(this.href, "WagerDetails", "width=1000, height=500");
            }
        });

        this.$(".sport").click(function (): void {
            let $this: JQuery = $(this);
            let icon = $this.children().last();
            let listSports = $(".list-sports");
            let leagues = $(".league-wrapper");

            if (icon.hasClass("icon-chevron-down")) {
                icon.removeClass().addClass("icon-chevron-right");
                listSports.addClass("hide");
                leagues.each(function () {
                    $(this).children().last().removeClass().addClass("icon-chevron-right");
                })
            } else {
                icon.removeClass().addClass("icon-chevron-down");
                listSports.removeClass("hide");
                leagues.each(function () {
                    $(this).children().last().removeClass().addClass("icon-chevron-down");
                })
            }
        });
    }
}

$(function (): void {
    new ViewByBetType(
        $,
        new SummaryMenu($, top),
        new SingleSelectionDropdownComponent($),
        new SingleSelectionDropdownComponent($),
        new SingleSelectionDropdownComponent($),
        new SingleSelectionDropdownComponent($)).init();
});