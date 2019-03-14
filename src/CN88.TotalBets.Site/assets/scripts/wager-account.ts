"use strict";
class WagerAccount {
    $: JQueryStatic;

    constructor(
        $: JQueryStatic) {
        this.$ = $;
    }

    init(): void {
    }
}

$(function (): void {
    new WagerAccount($).init();
});