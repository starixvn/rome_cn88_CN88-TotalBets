"use strict";

class SummaryMenuOption {
    constructor(
        public mainContentId = "main-content") {
    }
}

class SummaryMenu {
    $: JQueryStatic;
    $topWindow: Window;

    options = new SummaryMenuOption();
    $summaryMenu: JQuery;
    $summaryMenuItems: JQuery;
    $mainContent: JQuery;
    isDevice = false;
    collapsedClass = "collapsed";
    expandedClass = "expanded";
    clickTouchendEvents = "click touchend";

    constructor($: JQueryStatic, topWindow: Window) {
        this.$ = $;
        this.$topWindow = topWindow;
    }

    render = (options: SummaryMenuOption = new SummaryMenuOption()) => {
        this.options = this.$.extend({}, this.options, options) as SummaryMenuOption;

        this.$mainContent = this.$("#" + this.options.mainContentId);
        this.$summaryMenu = this.$("#summary-menu");
        this.$summaryMenuItems = this.$("#summary-menu-items");
        this.isDevice = this.$("#IsDevice").val() === "1";

        this.$("#head-menu").on(this.clickTouchendEvents, (event: JQueryEventObject) => {
            if (event.type === "touchend") {
                this.$(event.currentTarget).off('click');
            }

            if (this.isDevice) {
                this.collapseMenuMobile();

                event.stopPropagation();
                return false;
            } else {
                this.collapseMenu();
            }
        });

        if (this.isDevice) {
            this.$(window).on(this.clickTouchendEvents, this.hideMenuMobile);
            this.$summaryMenu.on(this.clickTouchendEvents, (event) => {
                event.stopPropagation();
            });

            this.calculateMenuHeight();

            this.$(window).resize(this.calculateMenuHeight);
        }
    }

    collapseMenu(): void {
        const isCollapsed = this.$summaryMenu.hasClass(this.collapsedClass);

        if (isCollapsed) {
            this.$summaryMenuItems.show();
            this.$summaryMenu.removeClass(this.collapsedClass);
            this.$mainContent.removeClass(this.expandedClass);
        } else {
            this.$summaryMenuItems.hide();
            this.$summaryMenu.addClass(this.collapsedClass);
            this.$mainContent.addClass(this.expandedClass);
        }
    }

    collapseMenuMobile(): void {
        const isCollapsed = this.$summaryMenu.hasClass(this.expandedClass);

        if (isCollapsed) {
            this.$summaryMenu.removeClass(this.expandedClass);
            this.$mainContent.removeClass(this.expandedClass);
        } else {
            this.$summaryMenu.addClass(this.expandedClass);
            this.$mainContent.addClass(this.expandedClass);
        }
    }

    hideMenuMobile = () => {
        const isCollapsed = this.$summaryMenu.hasClass(this.expandedClass);
        if (!isCollapsed) {
            return;
        }

        this.$summaryMenu.removeClass(this.expandedClass);
        this.$mainContent.removeClass(this.expandedClass);
    }

    calculateMenuHeight = () => {
        if (!this.isDevice) {
            return;
        }

        const mainHeight = this.$(this.$topWindow).height() - this.$topWindow.$("#header").height() - parseInt(this.$summaryMenu.css("top"), 0);
        const menuHeight = this.$summaryMenu.outerHeight();

        if (menuHeight >= mainHeight) {
            this.$summaryMenu.height(mainHeight);
        } else {
            this.$summaryMenu.css("height", "auto");
        }
    }
}