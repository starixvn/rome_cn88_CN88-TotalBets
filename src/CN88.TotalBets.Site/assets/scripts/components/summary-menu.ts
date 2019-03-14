"use strict";

class SummaryMenuOption {
    constructor(
        public mainContentId: string = "main-content",
        public filterFormId: string = "filter-form",
        public inputId: string = "SportTypeGroup") {
    }
}

class SummaryMenu {
    $: JQueryStatic;

    options = new SummaryMenuOption();
    $mainContent: JQuery;

    constructor($: JQueryStatic) {
        this.$ = $;
    }

    render = (options: SummaryMenuOption = new SummaryMenuOption()) => {
        this.options = <SummaryMenuOption>this.$.extend({}, this.options, options);

        this.$mainContent = this.$("#" + this.options.mainContentId);

        let $filterForm = this.$("#" + this.options.filterFormId);
        let $hiddenInput = this.$("#" + this.options.inputId);

        this.$("#head-menu").click(() => {
            this.collapseMenu();
        });

        let this$ = this.$;
        this.$("#summary-menu-items .sport").click(function () {
            var selectedId = this$(this).attr("id");
            $hiddenInput.val(selectedId);
            $filterForm.submit();
        });
    }

    collapseMenu(): void {
        let collapsedClass = 'collapsed';
        let expandedClass = 'expanded';
        let $summaryMenu = this.$("#summary-menu");
        let $summaryMenuItems = this.$("#summary-menu-items");
        let isCollapsed = $summaryMenu.hasClass(collapsedClass);

        if (isCollapsed) {
            $summaryMenuItems.show();
            $summaryMenu.removeClass(collapsedClass);
            this.$mainContent.removeClass(expandedClass);
        } else {
            $summaryMenuItems.hide();
            $summaryMenu.addClass(collapsedClass);
            this.$mainContent.addClass(expandedClass);
        }
    }
}