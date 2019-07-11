describe("OverView", () => {
    const jQueryFake = $;
    const viewModeSelectionComponent = new SingleSelectionDropdownComponent($);
    const topWindow = top;

    let overview: OverView;

    beforeEach(() => {
        overview = new OverView(jQueryFake, viewModeSelectionComponent, topWindow);
    });

    describe("_init_Always", () => {
        beforeEach(() => {
            // arrange
            spyOn(viewModeSelectionComponent, "render").and.callThrough();
            spyOn(overview, "onReportSubmit").and.callThrough();

            // act
            overview.init();
        });

        it("_Calls viewModeSelectionComponent.render", () => {
            // assert
            expect(viewModeSelectionComponent.render).toHaveBeenCalledWith(new SingleSelectionDropdownOption("dropdown-poss-type", "PossType", overview.onReportSubmit));
        });

        it("_Defines elements", () => {
            // assert
            expect(overview.$reportForm).toBeDefined();
            expect(overview.$tooltip).toBeDefined();
        });
    });

    describe("_onReportSubmit_Always", () => {
        beforeEach(() => {
            // arrange
            overview.init();
            spyOn(viewModeSelectionComponent, "updateSelectedValue").and.callThrough();
            spyOn(overview.$reportForm, "submit").and.callThrough();

            // act
            overview.onReportSubmit();
        });

        it("_Calls viewModeSelectionComponent.updateSelectedValue", () => {
            // assert
            expect(viewModeSelectionComponent.updateSelectedValue).toHaveBeenCalled();
        });

        it("_Submit report form", () => {
            // assert
            expect(overview.$reportForm.submit).toHaveBeenCalled();
        });
    });
});