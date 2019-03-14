"use strict";
class SummaryMenu {
    $: JQueryStatic;
    $iconMenu: JQuery;
    $summaryMenu: JQuery;
    $summaryMenuItems: JQuery;

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
    }

    registerEvents(): void {
        this.$iconMenu.click(() => { this.collapseSummaryMenu(); });
    }

    collapseSummaryMenu(): void {
        let collapsedClass = 'collapsed';
        let isCollapsed = this.$summaryMenu.hasClass(collapsedClass);
        if (isCollapsed) {
            this.$summaryMenuItems.show();
            this.$summaryMenu.removeClass(collapsedClass);
        } else {
            this.$summaryMenuItems.hide();
            this.$summaryMenu.addClass(collapsedClass)
        }
    }
}