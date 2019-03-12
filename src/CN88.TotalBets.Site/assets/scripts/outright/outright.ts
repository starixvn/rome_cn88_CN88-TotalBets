"use strict";
class Outright {
    $: JQueryStatic;

    constructor($: JQueryStatic) {
        this.$ = $;
    }

    init(): void {
    }
}

$(function (): void {
    new Outright($).init();
});