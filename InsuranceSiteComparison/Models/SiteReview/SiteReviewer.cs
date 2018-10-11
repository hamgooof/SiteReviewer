using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Web;
using InsuranceSiteComparison.Models.SiteReview.Analyzers;
using InsuranceSiteComparison.Models.SiteReview.SiteData;

namespace InsuranceSiteComparison.Models.SiteReview
{
    public class SiteReviewer
    {
        private readonly string _siteUrl;

        public SiteReviewer(string siteUrl)
        {
            this._siteUrl = siteUrl;
        }

        public static SiteReview ReviewSite(string url)
        {
            var review = new SiteReviewer(url);


            return review.GetReview();
        }

        private SiteReview GetReview()
        {
            //Get the main page HTML and the time to download
            var mainPage = SiteContentDownloader.GetContent(_siteUrl);

            //Analyze the HTML for keywords and accessibility features.
            var keywords = KeywordAnalyzer.AnalyzeKeywords(mainPage.Content);
            var accessibility = AccessibilityAnalyzer.AnalyzeAccessibility(mainPage.Content);

            return new SiteReview()
            {
                URL = _siteUrl,
                AccessibilityResult = accessibility,
                KeywordResult = keywords,
                TimeToLoad = mainPage.TimeToDownload,
                HtmlContent = mainPage.Content
            };
        }

    }
}