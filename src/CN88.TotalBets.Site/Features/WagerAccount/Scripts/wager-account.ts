"use strict";
class WagerAccount {
    $: JQueryStatic;
    $navTabs: JQuery;

    constructor(
        $: JQueryStatic) {
        this.$ = $;
    }

    init(): void {
        this.$navTabs = this.$("#nav-tabs");
        $('#nav-tabs .lnk-changetab:first').tab('show');        
    }
}

$(function (): void {
    new WagerAccount($).init();
});