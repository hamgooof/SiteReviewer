declare var ko: KnockoutStatic;
declare var $: JQueryStatic;

class viewModel {
    AllSites = ko.observableArray<string>();
    SelectedSites = ko.observableArray<string>();
    IsLoading = ko.observable<boolean>(false);

    ReviewedSites = ko.observableArray<SiteReview>();

    constructor() {

        const sites = ["https://www.morethan.com",
            "https://www.gocompare.com"];
        this.AllSites(sites);

    }
    Compare() {
        this.IsLoading(true);
        $.post({
            url: '/api/ReviewSites',
            data: ko.toJSON(this.SelectedSites),
            contentType: 'application/json',
        }).done((data: SiteReview[]) => {
            this.ReviewedSites(data);
        }).fail((err) => {
            console.error(err);
        }).always(() => {
            this.IsLoading(false);
        });
    }

    loadSite() {

    }
}
interface SiteReview {
    URL: string;
    TimeToLoad: string;
    AccessibilityResult: string[];
    KeywordResult: string[];
    HtmlContent: string;
}

//https://stackoverflow.com/questions/15523457/how-to-data-bind-content-for-an-iframe-using-knockoutjs
ko.bindingHandlers.iframeContent = {
    update: function (element, valueAccessor) {
        var value = ko.unwrap(valueAccessor());
        element.contentWindow.document.close(); // Clear the content
        element.contentWindow.document.write(value);
    }
};

ko.applyBindings(new viewModel());