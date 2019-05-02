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
            var $this = self.$(this);

            if ($this.parent().hasClass("active")) {
                return;
            }

            var activeGroup = self.$("#sport-groups .group.active .list-item");
            activeGroup.slideUp('fast');

            self.$sportGroups.removeClass("active");

            $this.parent().addClass("active");
            $this.next().slideDown('fast');
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