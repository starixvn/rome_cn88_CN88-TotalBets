"use strict";
interface Window {
    $: JQueryStatic;
}

class CN88TotalBets {
    $: JQueryStatic;
    store: StoreJSStatic;

    constructor($: JQueryStatic, store: StoreJSStatic) {
        this.$ = $;
        this.store = store;
    }

    convertAttributeToBool = (attrValue: string) => {
        return !!attrValue && attrValue.toString().toLowerCase() === "true";
    };

    localStorage = {
        get: (key: string) => {
            const result = this.store.enabled ? this.store.get(key) : this.$.cookie(key);
            return result ? JSON.parse(result) : undefined;
        },
        set: (key: string, value) => {
            const data = JSON.stringify(value);
            this.store.enabled ? this.store.set(key, data) : this.$.cookie(key, data, this.localStorage.getCookieExpireOptions());
        },
        getCookieExpireOptions: () => {
            const cookieExpire = { expires: 365, path: window.location.pathname };
            return cookieExpire;
        }
    };
}

const cn88TotalBets = new CN88TotalBets($, store);