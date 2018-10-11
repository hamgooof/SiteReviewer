//import ko = require("../../../knockout-3.4.2")
var viewModel = /** @class */ (function () {
    function viewModel() {
        this.AllSites = ko.observableArray();
        this.SelectedSites = ko.observableArray();
        this.IsLoading = ko.observable(false);
        this.ReviewedSites = ko.observableArray();
        var sites = ["https://www.morethan.com",
            "https://www.gocompare.com"];
        this.AllSites(sites);
    }
    viewModel.prototype.Compare = function () {
        var _this = this;
        this.IsLoading(true);
        $.post({
            url: '/api/CompareSites',
            data: ko.toJSON(this.SelectedSites),
            contentType: 'application/json',
        }).done(function (data) {
            _this.ReviewedSites(data);
        }).fail(function (err) {
            console.error(err);
        }).always(function () {
            _this.IsLoading(false);
        });
    };
    viewModel.prototype.loadSite = function () {
    };
    return viewModel;
}());
ko.applyBindings(new viewModel());
//# sourceMappingURL=Index.js.map