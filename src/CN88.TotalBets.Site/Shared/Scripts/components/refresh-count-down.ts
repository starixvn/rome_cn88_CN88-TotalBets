"use strict";

class RefreshCountDownOptions {
    constructor(
        public countDownMenuId: string = "CountDownMenu",
        public pageId: string = "Page1",
        public reloadInSeconds: number = 30,
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
    loadingText = "";

    constructor($: JQueryStatic, linkBuilderComponent: LinkBuilderComponent) {
        this.$ = $;
        this.linkBuilderComponent = linkBuilderComponent;
    }

    render = (options: RefreshCountDownOptions = new RefreshCountDownOptions()) => {
        this.options = <RefreshCountDownOptions>this.$.extend({}, this.options, options);
        var pageId = cn88TotalBets.localStorage.get(this.pageIdCookie);
        if (pageId != options.pageId) {
            cn88TotalBets.localStorage.set(this.countDownOptionCookie, null);
        }

        cn88TotalBets.localStorage.set(this.pageIdCookie, options.pageId);

        var self = this;
        this.$btnRefresh = this.$(".btnRefresh");
        this.$countDownMenu = this.$("#" + options.countDownMenuId);
        this.loadingText = this.$btnRefresh.attr("data-loading-text");
        this.$countDownOptions = this.$(".count-down-option");
        this.$(".icon-refresh").on("click touchend", this.doRefresh);
        this.$(".btn-show-options").on("click touchend", function (event) {
            if (event.type == 'touchend') {
                self.$(this).off('click');
            }

            self.showOptions();
            event.stopPropagation();
            return false;
        });
        this.$countDownOptions.on("click touchend", function (event) {
            self.saveOption(self.$(this).attr("data-value"));
            event.stopPropagation();
            return false;
        });
        this.$("#count-down-off").on("click touchend", function () {
            self.turnOffCountDown(self.$(this).html());
        });

        var currentOption = cn88TotalBets.localStorage.get(this.countDownOptionCookie);
        if (currentOption && currentOption != 0) {
            this.setCountDown(currentOption);
        } else {
            this.setCountDown(options.reloadInSeconds);
        }

        this.$(window).bind("click touchend", (event) => {
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
    saveOption = (option) => {
        cn88TotalBets.localStorage.set(this.countDownOptionCookie, option);
        if (option == "-1") {
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
    setCountDown = (option) => {
        this.options.reloadInSeconds = option;
        this.currentCountDown = option;
        this.activeSelectedOption(option);
        this.countDownTick();
    };
    countDownTick = () => {
        if (this.currentCountDown == -1) {
            this.$btnRefresh.html(this.$(".count-down-option.active").html().toUpperCase());
            return;
        }

        this.$btnRefresh.html(this.currentCountDown.toString());
        this.currentCountDown--;
        if (this.countDownTimer != null) {
            clearTimeout(this.countDownTimer);
        }

        this.countDownTimer = setTimeout(this.countDownTick, 1000);

        if (this.currentCountDown == 0) {
            this.options.countDownFunc();
            this.currentCountDown = this.options.reloadInSeconds;
        }
    };
    activeSelectedOption = (option) => {
        this.$countDownOptions.removeClass("active");
        for (var i = 0; i < this.$countDownOptions.length; i++) {
            var $option = this.$(this.$countDownOptions[i]);
            if ($option.attr("data-value") == this.options.reloadInSeconds.toString()) {
                $option.addClass("active");
            }
        }
    };
    turnOffCountDown = (optionText) => {
        this.$btnRefresh.html(optionText);

        if (this.countDownTimer != null) {
            clearTimeout(this.countDownTimer);
        }

        this.$countDownMenu.addClass("hide");
    };
}

