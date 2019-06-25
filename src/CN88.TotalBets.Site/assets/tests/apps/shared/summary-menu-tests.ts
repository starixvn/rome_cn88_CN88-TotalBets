describe("SummaryMenu", () => {
    const jQueryStatic = $;
    const topWindow = top;
    let summaryMenu: SummaryMenu;

    const template = {
        normal: `<body>
                    <div id="summary-menu" class="menu-container">
                        <ul class="list-sports" id="summary-menu-items">
                        </ul>
                    </div>
                    <div class="main-content" id="main-content"></div>
                </body>`,
        collapse: `<body>
                    <div id="summary-menu" class="menu-container menu-container-collapsed">
                        <ul class="list-sports" id="summary-menu-items">
                        </ul>
                    </div>
                    <div class="main-content expanded"" id="main-content"></div>
                </body>`,
        mobileExpand: `<body>
                    <div id="summary-menu" class="menu-container expanded">
                        <ul class="list-sports" id="summary-menu-items">
                        </ul>
                    </div>
                    <div class="main-content expanded"" id="main-content"></div>
                </body>`
    };

    beforeEach(() => {
        summaryMenu = new SummaryMenu(jQueryStatic, topWindow);
    });

    describe("_render_Always", () => {
        beforeEach(() => {
            setFixtures(template.normal);
            summaryMenu.render();
        });

        it("-> All neccessary elements have been cached", () => {
            expect(summaryMenu.options).toBeDefined();
            expect(summaryMenu.$mainContent).toBeDefined();
            expect(summaryMenu.$summaryMenu).toBeDefined();
            expect(summaryMenu.$summaryMenuItems).toBeDefined();
            expect(summaryMenu.isDevice).toBeDefined();
        });
    });

    describe("_collapseMenu", () => {
        it("_isCollapsed -> expand menu", () => {
            // arrange
            setFixtures(template.collapse);
            summaryMenu.render();
            spyOn(summaryMenu.$summaryMenuItems, "show");
            spyOn(summaryMenu.$summaryMenu, "removeClass");
            spyOn(summaryMenu.$mainContent, "removeClass");

            // act
            summaryMenu.collapseMenu();

            // assert
            expect(summaryMenu.$summaryMenuItems.show).toHaveBeenCalled();
            expect(summaryMenu.$summaryMenu.removeClass).toHaveBeenCalledWith("menu-container-collapsed");
            expect(summaryMenu.$mainContent.removeClass).toHaveBeenCalledWith("expanded");
        });

        it("_isExpanded -> collapse menu", () => {
            // arrange
            setFixtures(template.normal);
            summaryMenu.render();
            spyOn(summaryMenu.$summaryMenuItems, "hide");
            spyOn(summaryMenu.$summaryMenu, "addClass");
            spyOn(summaryMenu.$mainContent, "addClass");

            // act
            summaryMenu.collapseMenu();

            // assert
            expect(summaryMenu.$summaryMenuItems.hide).toHaveBeenCalled();
            expect(summaryMenu.$summaryMenu.addClass).toHaveBeenCalledWith("menu-container-collapsed");
            expect(summaryMenu.$mainContent.addClass).toHaveBeenCalledWith("expanded");
        });
    });

    describe("_collapseMenuMobile", () => {
        it("_isExpanded -> collapse menu", () => {
            // arrange
            setFixtures(template.mobileExpand);
            summaryMenu.render();
            spyOn(summaryMenu.$summaryMenu, "removeClass");
            spyOn(summaryMenu.$mainContent, "removeClass");

            // act
            summaryMenu.collapseMenuMobile();

            // assert
            expect(summaryMenu.$summaryMenu.removeClass).toHaveBeenCalledWith("expanded");
            expect(summaryMenu.$mainContent.removeClass).toHaveBeenCalledWith("expanded");
        });

        it("_isExpanded -> collapse menu", () => {
            // arrange
            setFixtures(template.normal);
            summaryMenu.render();
            spyOn(summaryMenu.$summaryMenu, "addClass");
            spyOn(summaryMenu.$mainContent, "addClass");

            // act
            summaryMenu.collapseMenuMobile();

            // assert
            expect(summaryMenu.$summaryMenu.addClass).toHaveBeenCalledWith("expanded");
            expect(summaryMenu.$mainContent.addClass).toHaveBeenCalledWith("expanded");
        });
    });

    describe("_hideMenuMobile", () => {
        it("_isExpanded -> return", () => {
            // arrange
            setFixtures(template.mobileExpand);
            summaryMenu.render();
            spyOn(summaryMenu.$summaryMenu, "removeClass");
            spyOn(summaryMenu.$mainContent, "removeClass");

            // act
            summaryMenu.hideMenuMobile();

            // assert
            expect(summaryMenu.$summaryMenu.removeClass).toHaveBeenCalled();
            expect(summaryMenu.$mainContent.removeClass).toHaveBeenCalled();
        });

        it("_isCollapsed -> return", () => {
            // arrange
            setFixtures(template.normal);
            summaryMenu.render();
            spyOn(summaryMenu.$summaryMenu, "removeClass");
            spyOn(summaryMenu.$mainContent, "removeClass");

            // act
            summaryMenu.hideMenuMobile();

            // assert
            expect(summaryMenu.$summaryMenu.removeClass).not.toHaveBeenCalled();
            expect(summaryMenu.$mainContent.removeClass).not.toHaveBeenCalled();
        });
    });
});