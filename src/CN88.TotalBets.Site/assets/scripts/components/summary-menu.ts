"use strict";
class SummaryMenu {
    $: JQueryStatic;
    $iconMenu: JQuery;
    $summaryMenu: JQuery;

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
    }

    registerEvents(): void {
        this.$iconMenu.click(() => { this.collapseSummaryMenu(); });
    }

    collapseSummaryMenu(): void {
        this.$summaryMenu.hide();
    }
}