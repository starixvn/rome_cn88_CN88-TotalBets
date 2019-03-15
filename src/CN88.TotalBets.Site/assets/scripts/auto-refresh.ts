"use strict";
class AutoRefresh {
    private countDownContainer: JQuery;
    private onSubmit: Function;
    private timerId: number = 0;

    constructor(countDownContainer: JQuery, onSubmit: Function) {
        this.countDownContainer = countDownContainer;
        this.onSubmit = onSubmit;
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
        if (interval === 1 && this.onSubmit) {
            this.onSubmit();
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
