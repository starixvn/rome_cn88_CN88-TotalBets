"use strict";
class SummaryMenu {
    $: JQueryStatic;
    $iconMenu: JQuery;
    $summaryMenu: JQuery;
    $summaryMenuItems: JQuery;
    $mainContent: JQuery;

    constructor($: JQueryStatic) {
        this.$ = $;
    }

    init(): void {
        this.cacheDOMElements();
        this.registerEvents();
    }

    cacheDOMElements(): void {
        this.$iconMenu = this.$("#icon-menu");
        this.$summaryMenu = this.$("#summary-menu");
        this.$summaryMenuItems = this.$("#summary-menu-items");
        this.$mainContent = this.$("#main-content");
    }

    registerEvents(): void {
        this.$iconMenu.click(() => { this.collapseSummaryMenu(); });
    }

    collapseSummaryMenu(): void {
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
}