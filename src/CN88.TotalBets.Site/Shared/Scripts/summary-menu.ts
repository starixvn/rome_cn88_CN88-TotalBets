"use strict";

class SummaryMenuOption {
    constructor(
        public mainContentId: string = "main-content") {
    }
}

class SummaryMenu {
    $: JQueryStatic;

    options = new SummaryMenuOption();
    $summaryMenu: JQuery;
    $summaryMenuItems: JQuery;
    $mainContent: JQuery;

    constructor($: JQueryStatic) {
        this.$ = $;
    }

    render = (options: SummaryMenuOption = new SummaryMenuOption()) => {
        this.options = <SummaryMenuOption>this.$.extend({}, this.options, options);

        this.$mainContent = this.$("#" + this.options.mainContentId);
        this.$summaryMenu = this.$("#summary-menu");
        this.$summaryMenuItems = this.$("#summary-menu-items");

        this.$("#head-menu").click(() => {
            this.collapseMenuMobile();
        });
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
}