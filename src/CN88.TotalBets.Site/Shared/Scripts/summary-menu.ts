"use strict";

class SummaryMenuOption {
    constructor(
        public mainContentId: string = "main-content") {
    }
}

class SummaryMenu {
    $: JQueryStatic;
    $topWindow: any;

    options = new SummaryMenuOption();
    $summaryMenu: JQuery;
    $summaryMenuItems: JQuery;
    $mainContent: JQuery;
    isDevice: boolean = false;

    constructor($: JQueryStatic, topWindow: any) {
        this.$ = $;
        this.$topWindow = topWindow;
    }

    render = (options: SummaryMenuOption = new SummaryMenuOption()) => {
        this.options = <SummaryMenuOption>this.$.extend({}, this.options, options);

        this.$mainContent = this.$("#" + this.options.mainContentId);
        this.$summaryMenu = this.$("#summary-menu");
        this.$summaryMenuItems = this.$("#summary-menu-items");
        this.isDevice = this.$("#IsDevice").val() == "1";

        var self = this;
        this.$("#head-menu").on("click touchend", function (event) {
            if (event.type == 'touchend') {
                self.$(this).off('click');
            }

            if (self.isDevice) {
                self.collapseMenuMobile();

                event.stopPropagation();
                return false;
            } else {
                self.collapseMenu();
            }
        });

        if (this.isDevice) {
            this.$(window).on("click touchend", () => { this.hideMenuMobile(); });
            this.$summaryMenu.on("click touchend", (event) => {
                event.stopPropagation();
            });

            this.calculateMenuHeight();

            this.$(window).resize(() => {
                this.calculateMenuHeight();
            });
        }
    }

    collapseMenu(): void {
        let collapsedClass = 'collapsed';
        let expandedClass = 'expanded';

        let isCollapsed = this.$summaryMenu.hasClass(collapsedClass);

        if (isCollapsed) {
            this.$summaryMenuItems.show();
            this.$summaryMenu.removeClass(collapsedClass);
            this.$mainContent.removeClass(expandedClass);
        } else {
            this.$summaryMenuItems.hide();
            this.$summaryMenu.addClass(collapsedClass);
            this.$mainContent.addClass(expandedClass);
        }
    }

    collapseMenuMobile(): void {
        let collapsedClass = 'collapsed';
        let expandedClass = 'expanded';

        let isCollapsed = this.$summaryMenu.hasClass(expandedClass);

        if (isCollapsed) {
            this.$summaryMenu.removeClass(expandedClass);
            this.$mainContent.removeClass(expandedClass);
        } else {
            this.$summaryMenu.addClass(expandedClass);
            this.$mainContent.addClass(expandedClass);
        }
    }

    hideMenuMobile(): void {
        let expandedClass = 'expanded';
        let isCollapsed = this.$summaryMenu.hasClass(expandedClass);
        if (!isCollapsed) {
            return;
        }

        this.$summaryMenu.removeClass(expandedClass);
        this.$mainContent.removeClass(expandedClass);
    }

    calculateMenuHeight(): void {
        if (!this.isDevice) {
            return;
        }
        var menu = this.$("#summary-menu"),
            mainHeight = this.$(this.$topWindow).height() - this.$topWindow.$("#header").height() - parseInt(menu.css("top"), 0),
            menuHeight = menu.outerHeight();

        if (menuHeight >= mainHeight) {
            menu.height(mainHeight);
        } else {
            menu.css("height", "auto");
        }
    }
}