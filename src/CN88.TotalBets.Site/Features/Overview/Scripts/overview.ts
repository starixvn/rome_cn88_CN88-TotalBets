"use strict";
class OverView {
    $: JQueryStatic;
    viewModeSelectionComponent: SingleSelectionDropdownComponent;
    $reportForm: JQuery;
    $tooltip: JQuery;
    $topWindow: any;
    $sportGroups: any;

    constructor(
        $: JQueryStatic,
        viewModeSelectionComponent: SingleSelectionDropdownComponent,
        topWindow: any) {
        this.$ = $;
        this.viewModeSelectionComponent = viewModeSelectionComponent;
        this.$topWindow = topWindow;
    }

    init(): void {
        this.viewModeSelectionComponent.render(new SingleSelectionDropdownOption("dropdown-poss-type", "PossType", this.onReportSubmit));
        this.$reportForm = this.$("#overview-form");
        this.$tooltip = this.$('[data-toggle="tooltip"]');
        this.$tooltip.tooltip();

        var self = this;
        this.$(".page-overview table a").click(function () {
            var subMenuId = self.$(this).attr("data-submenuid");
            self.$topWindow.indexManager.subMenuClick(subMenuId);
        });

        this.$sportGroups = this.$("#sport-groups .group");
        this.$("#sport-groups .group-title").on("click touchend", function () {
            self.$sportGroups.removeClass("active");
            self.$(this).parent().addClass("active");
        });
    }

    public onReportSubmit = () => {
        this.viewModeSelectionComponent.updateSelectedValue();
        this.$reportForm.submit();
    };
}

$(function (): void {
    new OverView($, new SingleSelectionDropdownComponent($), top).init();
});