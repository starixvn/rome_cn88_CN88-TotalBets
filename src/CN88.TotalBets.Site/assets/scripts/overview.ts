"use strict";
class OverView {
    $: JQueryStatic;
    viewModeSelectionComponent: SingleSelectionDropdownComponent;
    $reportForm: JQuery;
    $tooltip: JQuery;

    constructor(
        $: JQueryStatic,
        viewModeSelectionComponent: SingleSelectionDropdownComponent,) {
        this.$ = $;
        this.viewModeSelectionComponent = viewModeSelectionComponent;
    }

    init(): void {
        this.viewModeSelectionComponent.render(new SingleSelectionDropdownOption("dropdown-view-mode", "viewModeSelectedId", this.onReportSubmit));
        this.$reportForm = this.$("#overview-form");
        this.$tooltip = this.$('[data-toggle="tooltip"]');
        this.$tooltip.tooltip('show');
    }

    public onReportSubmit = () => {
        this.viewModeSelectionComponent.updateSelectedValue();
        this.$reportForm.submit();
    };
}

$(function (): void {
    new OverView($, new SingleSelectionDropdownComponent($)).init();
});