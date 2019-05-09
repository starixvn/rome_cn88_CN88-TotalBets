"use strict";
class CN88TotalBets {
    convertAttributeToBool = (attrValue: string) => {
        return !!attrValue && attrValue.toString().toLowerCase() === "true";
    };

    localStorage = {
        get: (key: string) => {
            var result = store.enabled ? store.get(key) : $.cookie(key);
            return result ? JSON.parse(result) : undefined;
        },
        set: (key: string, value: any) => {
            var data = JSON.stringify(value);
            store.enabled ? store.set(key, data) : $.cookie(key, data, this.localStorage.getCookieExpireOptions());
        },
        getCookieExpireOptions: () => {
            var cookieExpire: any = { expires: 365, path: window.location.pathname };
            return cookieExpire;
        }
    };
}

const cn88TotalBets = new CN88TotalBets();