"use strict";
class OverView {
    $: JQueryStatic;
    viewModeSelectionComponent: SingleSelectionDropdownComponent;
    $reportForm: JQuery;
    $tooltip: JQuery;
    $topWindow: Window;
    isScrolling = false;

    constructor(
        $: JQueryStatic,
        viewModeSelectionComponent: SingleSelectionDropdownComponent,
        topWindow: Window) {
        this.$ = $;
        this.viewModeSelectionComponent = viewModeSelectionComponent;
        this.$topWindow = topWindow;
    }

    init(): void {
        this.viewModeSelectionComponent.render(new SingleSelectionDropdownOption("dropdown-poss-type", "PossType", this.onReportSubmit));
        this.$reportForm = this.$("#overview-form");
        this.$tooltip = this.$('[data-toggle="tooltip"]');
        this.$tooltip.tooltip();

        $(document).on("touchmove", () => { this.isScrolling = true; });

        $(document).on("touchend", () => { this.isScrolling = false; });

        this.$(".page-overview table a, .page-overview .link[data-submenuid]").click((event: JQueryEventObject) => {
            const subMenuId = this.$(event.currentTarget).attr("data-submenuid");
            this.$topWindow["indexManager"].subMenuClick(subMenuId);
        });

        this.$("#sport-groups .group-title").on("click touchend", (event: JQueryEventObject) => {
            if (this.isScrolling) {
                this.isScrolling = false;
                event.stopPropagation();
                return false;
            }

            const $this = this.$(event.currentTarget);

            if (event.type === "touchend") {
                $this.off("click");
            }

            if ($this.parent().hasClass("active")) {
                $this.next().slideUp("fast", () => { $this.parent().removeClass("active"); });
                return;
            }

            $this.parent().addClass("active");
            $this.next().slideDown("fast");
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