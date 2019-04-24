"use strict";
class CN88TotalBets {
    convertAttributeToBool = (attrValue: string) => {
        return !!attrValue && attrValue.toString().toLowerCase() === "true";
    };
}

const cn88TotalBets = new CN88TotalBets();