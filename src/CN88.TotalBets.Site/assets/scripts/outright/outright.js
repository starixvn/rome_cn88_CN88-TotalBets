"use strict";
var Outright = /** @class */ (function () {
    function Outright($) {
        this.$ = $;
    }
    Outright.prototype.init = function () {
        this.stakeConditionSelectionComponent.render(new SingleSelectionDropdownOption("dropdown-stake-condition", "StakeCondition"));
        this.possTypeSelectionComponent.render(new SingleSelectionDropdownOption("dropdown-poss-type", "PossType"));
        this.leagueSelectionComponent.render(new SingleSelectionDropdownOption("dropdown-league", "LeagueId"));
    };
    return Outright;
}());
$(function () {
    new Outright($).init();
});
//# sourceMappingURL=outright.js.map