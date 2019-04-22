"use strict";
class AutoRefresh {
    private countDownContainer: JQuery;
    private filterForm: JQuery;
    private timerId: number = 0;

    constructor(countDownContainer: JQuery, filterForm: JQuery, switchCheckbox: JQuery, hiddenInput: JQuery, reloadInSeconds: number) {
        this.countDownContainer = countDownContainer;
        this.filterForm = filterForm;

        let self = this;
        switchCheckbox.change(function () {
            if (this.checked) {
                self.countDownContainer.parent().removeAttr("disabled");
                self.start(reloadInSeconds);
            } else {
                self.stop();
                self.countDownContainer.text("..");
                self.countDownContainer.parent().attr("disabled", "disabled");
            }

            hiddenInput.val(this.checked);
        });

        if (switchCheckbox.is(":checked")) {
            this.start(reloadInSeconds);
        } else {
            this.countDownContainer.parent().attr("disabled", "disabled");
        }
        
    }

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
        if (interval === 1 && this.filterForm.length) {
            this.filterForm.trigger("submit");
        }
        else {
            this.setCountDown(--interval);
            this.timerId = setTimeout(() => { this.tick(interval); }, 1000);
        }
    }

    private setCountDown(value: number): void {
        this.countDownContainer.text(value);
    }
}
