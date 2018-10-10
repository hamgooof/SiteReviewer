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
        private string _html;
        private HtmlDocument _document;

        private AccessibilityAnalyzer(string html)
        {
            _document = new HtmlDocument();
            _document.LoadHtml(html);
        }

        private List<string> AnalyzeHtml()
        {
            var result = new List<string>();

            result.Add(AnalyzePercentageOfImageAltTags());

            return result;
        }

        /// <summary>
        /// Returns the amount of image tags found, and the percentage with the alt attribute
        /// </summary>
        /// <returns></returns>
        private string AnalyzePercentageOfImageAltTags()
        {
            var allImageTags = _document.DocumentNode.Descendants("img").ToList();

            var imageTagsWithAlt = allImageTags.Count(p => p.Attributes.Any(a => a.Name == "alt"));
            var totalImageTags = allImageTags.Count;

            var percentage = (imageTagsWithAlt / (double)allImageTags.Count);

            return $"{percentage:P0} of image tags have 'alt' attributes ({imageTagsWithAlt} of {totalImageTags})";
        }


        public static List<string> AnalyzeAccessibility(SiteContent content)
        {
            return new AccessibilityAnalyzer(content.Content).AnalyzeHtml();
        }

    }
}