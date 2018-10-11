using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using HtmlAgilityPack;
using InsuranceSiteComparison.Models.SiteReview.SiteData;

namespace InsuranceSiteComparison.Models.SiteReview.Analyzers
{
    public class AccessibilityAnalyzer
    {
        private readonly HtmlDocument _document;

        private AccessibilityAnalyzer(string html)
        {
            _document = new HtmlDocument();
            _document.LoadHtml(html);
        }

        private List<string> AnalyzeHtml()
        {
            return new List<string>
            {
                AnalyzePercentageOfImageAltTags()

            };


        }

        /// <summary>
        /// Returns the amount of image tags found, and the percentage with the alt attribute
        /// </summary>
        /// <returns></returns>
        private string AnalyzePercentageOfImageAltTags()
        {
            var allImageTags = _document.DocumentNode.Descendants("img").ToList();
            var totalImageTags = allImageTags.Count;

            if (totalImageTags == 0)
                return "No image tags found";

            var imageTagsWithAlt = allImageTags.Count(p => p.Attributes.Any(a => a.Name == "alt"));
            var percentage = (imageTagsWithAlt / (double)allImageTags.Count);

            return $"{percentage:P0} of image tags have 'alt' attributes ({imageTagsWithAlt} of {totalImageTags})";
        }


        public static List<string> AnalyzeAccessibility(string htmlContent)
        {
            return new AccessibilityAnalyzer(htmlContent).AnalyzeHtml();
        }

    }
}