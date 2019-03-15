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
        $('#nav-tabs a').click(function (e) {
            e.preventDefault()
            $(this).tab('show')
        })
    }
}

$(function (): void {
    new WagerAccount($).init();
});