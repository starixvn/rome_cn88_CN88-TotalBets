describe("CN88TotalBets", () => {
    let cn88TotalBets: CN88TotalBets;
    const jQueryFake = $;
    const storeFake = store;

    beforeEach(() => {
        cn88TotalBets = new CN88TotalBets(jQueryFake, storeFake);
    });
    describe("_localStorage.get", () => {
        it("_Enable store -> Gets data from store", () => {
            // arrange
            cn88TotalBets.store.enabled = true;
            cn88TotalBets.localStorage.set("SomeKey", "SomeValue");
            spyOn(jQueryFake, "cookie");

            // act
            const actualValue = cn88TotalBets.localStorage.get("SomeKey");

            // assert
            expect(actualValue).toEqual("SomeValue");
            expect(jQueryFake.cookie).not.toHaveBeenCalled();
        });
        it("_Disable store -> Gets data from cookie", () => {
            // arrange
            cn88TotalBets.store.enabled = false;
            cn88TotalBets.localStorage.set("SomeKey", "1");
            spyOn(jQueryFake, "cookie").and.callFake(() => "1");

            // act
            const actualValue = cn88TotalBets.localStorage.get("SomeKey");

            // assert
            expect(jQueryFake.cookie).toHaveBeenCalled();
            expect(actualValue).toEqual(1);
        });
    });
    describe("_localStorage.set", () => {
        it("_Enable store -> Sets data to store", () => {
            // arrange
            cn88TotalBets.store.enabled = true;
            spyOn(storeFake, "set");
            spyOn(jQueryFake, "cookie");

            // act
            cn88TotalBets.localStorage.set("SomeKey", "SomeValue");

            // assert
            expect(storeFake.set).toHaveBeenCalled();
            expect(jQueryFake.cookie).not.toHaveBeenCalled();
        });
        it("_Disable store -> Sets data to cookie", () => {
            // arrange
            cn88TotalBets.store.enabled = false;
            spyOn(storeFake, "set");
            spyOn(jQueryFake, "cookie");

            // act
            cn88TotalBets.localStorage.set("SomeKey", "SomeValue");

            // assert
            expect(jQueryFake.cookie).toHaveBeenCalled();
            expect(storeFake.set).not.toHaveBeenCalled();
        });
    });
    describe("_localStorage.getCookieExpireOptions", () => {
        it("_Always  -> Returns expire days and path", () => {
            // arrange
            const expectedCookieExpireOptions = { expires: 365, path: window.location.pathname };

            // act
            const actualCookieExpireOptions = cn88TotalBets.localStorage.getCookieExpireOptions();

            // assert
            expect(actualCookieExpireOptions).toEqual(expectedCookieExpireOptions);
        });
    });
    describe("_convertAttributeToBool", () => {
        const convertAttributeToBoolTestCase = (attributeValue: string, expectedValue: boolean) => {
            // act
            const actualValue = cn88TotalBets.convertAttributeToBool(attributeValue);

            // assert
            expect(actualValue).toEqual(expectedValue);
        };

        it("_Null Attribute -> Returns False", () => {
            convertAttributeToBoolTestCase(null, false);
        });
        it("_Empty Value -> Returns False", () => {
            convertAttributeToBoolTestCase("", false);
        });
        it("_Not True Value -> Returns False", () => {
            convertAttributeToBoolTestCase("False", false);
            convertAttributeToBoolTestCase(" ", false);
            convertAttributeToBoolTestCase("  ", false);
        });
        it("_True Value -> Returns True", () => {
            convertAttributeToBoolTestCase("true", true);
            convertAttributeToBoolTestCase("True", true);
        });
    });
});