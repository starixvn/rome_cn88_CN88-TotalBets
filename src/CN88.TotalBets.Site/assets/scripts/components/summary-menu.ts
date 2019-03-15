"use strict";

class SummaryMenuOption {
    constructor(
        public mainContentId: string = "main-content") {
    }
}

class SummaryMenu {
    $: JQueryStatic;

    options = new SummaryMenuOption();
    $mainContent: JQuery;

    constructor($: JQueryStatic) {
        this.$ = $;
    }

    render = (options: SummaryMenuOption = new SummaryMenuOption()) => {
        this.options = <SummaryMenuOption>this.$.extend({}, this.options, options);

        this.$mainContent = this.$("#" + this.options.mainContentId);

        this.$("#head-menu").click(() => {
            this.collapseMenu();
        });
    }

    collapseMenu(): void {
        let collapsedClass = 'collapsed';
        let expandedClass = 'expanded';
        let $summaryMenu = this.$("#summary-menu");
        let $summaryMenuItems = this.$("#summary-menu-items");
        let isCollapsed = $summaryMenu.hasClass(collapsedClass);

        if (isCollapsed) {
            $summaryMenuItems.show();
            $summaryMenu.removeClass(collapsedClass);
            this.$mainContent.removeClass(expandedClass);
        } else {
            $summaryMenuItems.hide();
            $summaryMenu.addClass(collapsedClass);
            this.$mainContent.addClass(expandedClass);
        }
    }
}