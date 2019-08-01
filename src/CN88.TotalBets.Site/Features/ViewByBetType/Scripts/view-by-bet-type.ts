/// <reference path="../../../shared/scripts/base-totalbets.ts" />
/// <reference path="../../../shared/scripts/core.ts" />

class ViewByBetType extends BaseTotalBets {
    marketSelectionComponent: SingleSelectionDropdownComponent;
    autoRefresh: RefreshCountDown;
    isMobile: boolean;
    isShowMarket: boolean;
    $topWindow: Window;
    iconChevronDownClass = "icon-chevron-down";
    iconChevronRightClass = "icon-chevron-right";

    constructor(
        $: JQueryStatic,
        topWindow: Window,
        summaryMenu: SummaryMenu,
        stakeConditionSelectionComponent: SingleSelectionDropdownComponent,
        possTypeSelectionComponent: SingleSelectionDropdownComponent,
        marketSelectionComponent: SingleSelectionDropdownComponent,
        autoRefresh: RefreshCountDown) {
        super($, summaryMenu, stakeConditionSelectionComponent, possTypeSelectionComponent, null);

        this.marketSelectionComponent = marketSelectionComponent;
        this.$topWindow = topWindow;
        this.autoRefresh = autoRefresh;
    }

    init(): void {
        if (!this.checkReady()) {
            return;
        }

        this.$.blockUI();

        this.isShowMarket = cn88TotalBets.convertAttributeToBool(this.$("#IsShowMarketSelection").val());
        this.cacheDOMElements();
        this.renderComponents();
        this.bindEvents();

        this.$.unblockUI();
        this.$topWindow.$("body").scrollTop(0);
    }

    renderComponents(): void {
        super.renderComponents();

        if (this.isShowMarket) {
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

        if (this.isShowMarket) {
            const reloadInSeconds: number = parseInt(this.$("#ReloadTimeInSeconds").val());
            this.autoRefresh.render(new RefreshCountDownOptions("CountDownMenu", "", reloadInSeconds, this.doRefresh));
        }

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

    doRefresh = () => {
        this.onSubmit();
        this.$filterForm.trigger("submit");
    };
}

$(function (): void {
    new ViewByBetType(
        $,
        top,
        new SummaryMenu($, top),
        new SingleSelectionDropdownComponent($),
        new SingleSelectionDropdownComponent($),
        new SingleSelectionDropdownComponent($),
        new RefreshCountDown($)).init();
});