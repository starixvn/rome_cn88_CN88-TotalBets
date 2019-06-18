"use strict";
interface Window {
    $: JQueryStatic;
}

class CN88TotalBets {
    convertAttributeToBool = (attrValue: string) => {
        return !!attrValue && attrValue.toString().toLowerCase() === "true";
    };

    localStorage = {
        get: (key: string) => {
            const result = store.enabled ? store.get(key) : $.cookie(key);
            return result ? JSON.parse(result) : undefined;
        },
        set: (key: string, value) => {
            const data = JSON.stringify(value);
            store.enabled ? store.set(key, data) : $.cookie(key, data, this.localStorage.getCookieExpireOptions());
        },
        getCookieExpireOptions: () => {
            const cookieExpire = { expires: 365, path: window.location.pathname };
            return cookieExpire;
        }
    };
}

const cn88TotalBets = new CN88TotalBets();