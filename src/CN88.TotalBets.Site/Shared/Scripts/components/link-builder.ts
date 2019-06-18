class LinkBuilderOption {
    linkClasses: string[];

    constructor(linkClasses: string[] = ["a.link,.sport>a,a.back-to-sum"]) {
        this.linkClasses = linkClasses;
    }
}

class LinkBuilderComponent {
    $: JQueryStatic;

    options = new LinkBuilderOption();

    constructor($: JQueryStatic) {
        this.$ = $;
    }

    render = (options: LinkBuilderOption = new LinkBuilderOption) => {
        this.options = this.$.extend({}, this.options, options);
    };

    updateParam = (name: string, value: string) => {
        this.options.linkClasses.forEach((className: string) => {
            const $linkClass = this.$(className);

            $linkClass.each((index: number, element: Element) => {
                const $link = this.$(element);
                let currentUrl = $link.attr("href");

                currentUrl = this.replaceQueryStringValue(currentUrl, name, value);

                $link.attr("href", currentUrl);
            });
        });
    };

    replaceQueryStringValue = (queryString: string, paramName: string, paramValue) => {
        if (typeof queryString === "undefined"
            || queryString === null) {
            return "";
        }

        const regex = new RegExp("([?&])" + paramName + "=[^&]*", "i");

        const newQueryString = regex.test(queryString)
            ? queryString.replace(regex, "$1" + paramName + "=" + paramValue)
            : queryString + (queryString.indexOf("?") >= 0 ? "&" : "?") + paramName + "=" + paramValue;

        return newQueryString;
    };
}