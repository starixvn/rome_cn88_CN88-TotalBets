/// <reference path="../../../shared/scripts/base-totalbets.ts" />
/// <reference path="../../../shared/scripts/core.ts" />

class ViewByBetType extends BaseTotalBets {
    marketSelectionComponent: SingleSelectionDropdownComponent;
    isMobile: boolean;
    $topWindow: Window;
    iconChevronDownClass = "icon-chevron-down";
    iconChevronRightClass = "icon-chevron-right";

    constructor(
        $: JQueryStatic,
        topWindow: Window,
        summaryMenu: SummaryMenu,
        stakeConditionSelectionComponent: SingleSelectionDropdownComponent,
        possTypeSelectionComponent: SingleSelectionDropdownComponent,
        marketSelectionComponent: SingleSelectionDropdownComponent) {
        super($, summaryMenu, stakeConditionSelectionComponent, possTypeSelectionComponent, null);

        this.marketSelectionComponent = marketSelectionComponent;
        this.$topWindow = topWindow;
    }

    init(): void {
        this.$.blockUI();

        this.cacheDOMElements();
        this.renderComponents();
        this.bindEvents();

        this.$.unblockUI();
        this.$topWindow.$("body").scrollTop(0);
    }

    renderComponents(): void {
        super.renderComponents();
        if (cn88TotalBets.convertAttributeToBool($("#IsShowMarketSelection").val())) {
            this.marketSelectionComponent.render(new SingleSelectionDropdownOption("dropdown-market", "Market"));
        }

        this.isMobile = this.$("#IsDevice").val() === "1";
    }

    onSubmit(): void {
        super.onSubmit();
        this.marketSelectionComponent.updateSelectedValue();
    }

    bindEvents(): void {
        super.bindEvents();

        const reloadInSeconds: number = this.$("#ReloadTimeInSeconds").val() as number;
        const autoRefresh: RefreshCountDown = new RefreshCountDown($, new LinkBuilderComponent($));

        autoRefresh.render(new RefreshCountDownOptions("CountDownMenu", "", reloadInSeconds, () => {
            this.onSubmit();
            this.$filterForm.trigger("submit");
        }));

        this.summaryMenu.$summaryMenuItems.find(".league-wrapper").click((e: JQueryEventObject) => {
            const $this: JQuery = this.$(e.currentTarget);
            const icon = $this.children().last();
            const child = $this.siblings(`.child-${$this.attr("id")}`);

            if (icon.hasClass(this.iconChevronDownClass)) {
                icon.removeClass().addClass(this.iconChevronRightClass);
                child.addClass("hide");
            } else {
                icon.removeClass().addClass(this.iconChevronDownClass);
                child.removeClass("hide");
            }
        });

        this.$tblTotalBets.find(".stake-link").click((e: JQueryEventObject) => {
            e.preventDefault();
            e.stopPropagation();
            const url = this.$(e.currentTarget).attr("href");

            if (this.isMobile) {
                window.open(url, "_blank");
            }
            else {
                window.open(url, "WagerDetails", "width=1000, height=500");
            }
        });

        this.$(".sport").click((e: JQueryEventObject) => {
            const $this: JQuery = this.$(e.currentTarget);
            const icon = $this.children().last();
            const listSports = $(".list-sports");
            const leagues = $(".league-wrapper");

            if (icon.hasClass(this.iconChevronDownClass)) {
                icon.removeClass().addClass(this.iconChevronRightClass);
                listSports.addClass("hide");
                leagues.each(function () {
                    $(this).children().last().removeClass().addClass("icon-chevron-right");
                });
            } else {
                icon.removeClass().addClass(this.iconChevronDownClass);
                listSports.removeClass("hide");
                leagues.each(function () {
                    $(this).children().last().removeClass().addClass("icon-chevron-down");
                });
            }
        });
    }
}

$(function (): void {
    new ViewByBetType(
        $,
        top,
        new SummaryMenu($, top),
        new SingleSelectionDropdownComponent($),
        new SingleSelectionDropdownComponent($),
        new SingleSelectionDropdownComponent($)).init();
});