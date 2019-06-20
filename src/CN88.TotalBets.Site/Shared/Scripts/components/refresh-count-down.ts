"use strict";

class RefreshCountDownOptions {
    constructor(
        public countDownMenuId = "CountDownMenu",
        public pageId = "Page1",
        public reloadInSeconds = 30,
        public countDownFunc: Function = () => { }) {
    }
}

class RefreshCountDown {
    $: JQueryStatic;
    linkBuilderComponent: LinkBuilderComponent;
    options = new RefreshCountDownOptions();
    $btnRefresh: JQuery;
    $countDownMenu: JQuery;
    $countDownOptions: JQuery;

    countDownOptionCookie = "cn88-total-bets-count-down";
    pageIdCookie = "cn88-total-bets-page-id";
    currentCountDown = 0;
    countDownTimer = null;
    countDownTimeOut = 1000;
    loadingText = "";
    clickTouchendEvents = "click touchend";

    constructor($: JQueryStatic, linkBuilderComponent: LinkBuilderComponent) {
        this.$ = $;
        this.linkBuilderComponent = linkBuilderComponent;
    }

    render = (options: RefreshCountDownOptions = new RefreshCountDownOptions()) => {
        this.options = this.$.extend({}, this.options, options) as RefreshCountDownOptions;
        const pageId = cn88TotalBets.localStorage.get(this.pageIdCookie);
        options.pageId = options.pageId || pageId;
        if (pageId !== options.pageId) {
            cn88TotalBets.localStorage.set(this.countDownOptionCookie, null);
        }

        cn88TotalBets.localStorage.set(this.pageIdCookie, options.pageId);

        this.$btnRefresh = this.$(".btnRefresh");
        this.$countDownMenu = this.$("#" + options.countDownMenuId);
        this.loadingText = this.$btnRefresh.attr("data-loading-text");
        this.$countDownOptions = this.$(".count-down-option");
        this.$(".icon-refresh").on(this.clickTouchendEvents, this.doRefresh);
        this.$(".btn-show-options").on(this.clickTouchendEvents, (event) => {
            if (event.type === "touchend") {
                this.$(event.currentTarget).off("click");
            }

            this.showOptions();
            event.stopPropagation();
            return false;
        });
        this.$countDownOptions.on(this.clickTouchendEvents, (event) => {
            this.saveOption(parseInt(this.$(event.currentTarget).attr("data-value")));
            event.stopPropagation();
            return false;
        });
        this.$("#count-down-off").on(this.clickTouchendEvents, (event) => {
            this.turnOffCountDown(this.$(event.currentTarget).html());
        });

        const currentOption = cn88TotalBets.localStorage.get(this.countDownOptionCookie);
        if (currentOption && currentOption !== 0) {
            this.setCountDown(currentOption);
        } else {
            this.setCountDown(options.reloadInSeconds);
        }

        this.$(window).bind(this.clickTouchendEvents, () => {
            this.$countDownMenu.addClass("hide");
        });
    };

    showOptions = () => {
        if (this.$countDownMenu.hasClass("hide")) {
            this.$countDownMenu.removeClass("hide");
        } else {
            this.$countDownMenu.addClass("hide");
        }
    };

    saveOption = (option: number) => {
        cn88TotalBets.localStorage.set(this.countDownOptionCookie, option);
        if (option === -1) {
            return;
        }

        this.setCountDown(option);
        this.$countDownMenu.addClass("hide");
    };

    doRefresh = () => {
        if (this.countDownTimer != null) {
            clearTimeout(this.countDownTimer);
        }

        this.$btnRefresh.html(this.loadingText);
        this.options.countDownFunc();
        this.currentCountDown = this.options.reloadInSeconds;
        this.countDownTick();
    };

    setCountDown = (option: number) => {
        this.options.reloadInSeconds = option;
        this.currentCountDown = option;
        this.activeSelectedOption();
        this.countDownTick();
    };

    countDownTick = () => {
        if (this.currentCountDown === -1) {
            this.$btnRefresh.html(this.$(".count-down-option.active").html().toUpperCase());
            return;
        }

        this.$btnRefresh.html(this.currentCountDown.toString());
        this.currentCountDown--;
        if (this.countDownTimer != null) {
            clearTimeout(this.countDownTimer);
        }

        this.countDownTimer = setTimeout(this.countDownTick, this.countDownTimeOut);

        if (this.currentCountDown === 0) {
            this.options.countDownFunc();
            this.currentCountDown = this.options.reloadInSeconds;
        }
    };

    activeSelectedOption = () => {
        this.$countDownOptions.removeClass("active");
        this.$countDownOptions.each((index: number, elem: Element) => {
            const $option = this.$(elem);
            if ($option.attr("data-value") === this.options.reloadInSeconds.toString()) {
                $option.addClass("active");
            }
        });
    };

    turnOffCountDown = (optionText) => {
        this.$btnRefresh.html(optionText);

        if (this.countDownTimer != null) {
            clearTimeout(this.countDownTimer);
        }

        this.$countDownMenu.addClass("hide");
    };
}