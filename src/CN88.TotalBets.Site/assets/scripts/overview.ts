"use strict";
class OverView {
    $: JQueryStatic;
    viewModeSelectionComponent: SingleSelectionDropdownComponent;
    $reportForm: JQuery;

    constructor(
        $: JQueryStatic,
        viewModeSelectionComponent: SingleSelectionDropdownComponent,) {
        this.$ = $;
        this.viewModeSelectionComponent = viewModeSelectionComponent;
    }

    init(): void {
        this.viewModeSelectionComponent.render(new SingleSelectionDropdownOption("dropdown-view-mode", "viewModeSelectedId", this.onReportSubmit));
        this.$reportForm = this.$("#overview-form");
    }

    public onReportSubmit = () => {
        this.viewModeSelectionComponent.updateSelectedValue();
        this.$reportForm.submit();
    };
}

$(function (): void {
    new OverView($, new SingleSelectionDropdownComponent($)).init();
});