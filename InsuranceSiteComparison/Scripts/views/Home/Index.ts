﻿declare var ko: KnockoutStatic;
declare var $: JQueryStatic;
//import ko = require("../../../knockout-3.4.2")
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
            url: '/api/CompareSites',
            data: ko.toJSON(this.SelectedSites),
            contentType: 'application/json',
        }).done((data: SiteReview[]) => {
            this.ReviewedSites(data);
        }).fail((err) => {
            console.error(err);
        }).always(() => {
            this.IsLoading(false);
        })
    }

    loadSite() {

    }
}
interface SiteReview {
    URL: string;
    TimeToLoad: string;
    AccessibilityResult: string[];
    KeywordResult: string[];
}

ko.applyBindings(new viewModel());