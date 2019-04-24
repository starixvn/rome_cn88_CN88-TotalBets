"use strict";

class AutoRefreshOptions {
    constructor(
        public countDownContainerId: string = "countDownContainer",
        public filterFormId: string = "filter-form",
        public switchCheckboxId: string = "chb-switch",
        public hiddenInputId: string = "IsAutoRefresh",
        public reloadInSeconds: number = 15) {
    }
}

class AutoRefresh {
    private $countDownContainer: JQuery;
    private $filterForm: JQuery;
    private timerId: number = 0;
    
    $: JQueryStatic;
    linkBuilderComponent: LinkBuilderComponent;
    options = new AutoRefreshOptions();


    constructor(
        $: JQueryStatic,
        linkBuilderComponent: LinkBuilderComponent) {
        this.$ = $;
        this.linkBuilderComponent = linkBuilderComponent;
    }

    render = (options: AutoRefreshOptions = new AutoRefreshOptions()) => {
        this.options = <AutoRefreshOptions>this.$.extend({}, this.options, options);
        this.$countDownContainer = this.$("#" + this.options.countDownContainerId);
        this.$filterForm = this.$("#" + this.options.filterFormId);
        let $switchCheckbox = this.$("#" + this.options.switchCheckboxId);

        if ($switchCheckbox.is(":checked")) {
            this.start(this.options.reloadInSeconds);
        } else {
            this.$countDownContainer.parent().attr("disabled", "disabled");
        }

        let self = this;
        $switchCheckbox.change(function () {
            if (this.checked) {
                self.$countDownContainer.parent().removeAttr("disabled");
                self.start(self.options.reloadInSeconds);
            } else {
                self.stop();
                self.$countDownContainer.text("..");
                self.$countDownContainer.parent().attr("disabled", "disabled");
            }

            self.$("#" + self.options.hiddenInputId).val(this.checked);
            self.linkBuilderComponent.updateParam(self.options.hiddenInputId, this.checked);
        });
    };

    start(interval: number): void {
        this.stop();
        this.timerId = setTimeout(() => { this.tick(interval); }, 1000);
    }

    stop(): void {
        if (this.timerId !== 0) {
            clearTimeout(this.timerId);
            this.timerId = 0;
        }
    }

    private tick(interval: number): void {
        if (interval === 1 && this.$filterForm.length) {
            this.$filterForm.trigger("submit");
        }
        else {
            this.setCountDown(--interval);
            this.timerId = setTimeout(() => { this.tick(interval); }, 1000);
        }
    }

    private setCountDown(value: number): void {
        this.$countDownContainer.text(value);
    }
}
