"use strict";
class OverView {
    $: JQueryStatic;
    viewModeSelectionComponent: SingleSelectionDropdownComponent;
    $reportForm: JQuery;
    $tooltip: JQuery;
    $topWindow: any;
    $sportGroups: any;
    isScrolling = false;

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
        $(document).on("touchmove", () => { self.isScrolling = true; });

        $(document).on("touchend", () => { self.isScrolling = false; });

        this.$(".page-overview table a").click(function () {
            var subMenuId = self.$(this).attr("data-submenuid");
            self.$topWindow.indexManager.subMenuClick(subMenuId);
        });

        this.$sportGroups = this.$("#sport-groups .group");
        this.$("#sport-groups .group-title").on("click touchend", function (event) {
            if (self.isScrolling) {
                self.isScrolling = false;
                event.stopPropagation();
                return false;
            }

            if (event.type == 'touchend') {
                $(this).off('click');
            }

            var $this = self.$(this);

            if ($this.parent().hasClass("active")) {
                $this.next().slideUp('fast', () => { $this.parent().removeClass("active"); });
                return;
            }

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