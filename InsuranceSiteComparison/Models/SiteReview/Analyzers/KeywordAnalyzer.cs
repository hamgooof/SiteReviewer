using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;
using InsuranceSiteComparison.Models.SiteReview.SiteData;

namespace InsuranceSiteComparison.Models.SiteReview.Analyzers
{
    public class KeywordAnalyzer
    {
        private readonly HtmlDocument _document;

        private KeywordAnalyzer(string htmlContent)
        {
            _document = new HtmlDocument();
            _document.LoadHtml(htmlContent);
        }

        public List<string> AnalyzeKeywords()
        {
            return new List<string>()
            {
                FindMetaKeywords(),
            };
        }

        private string FindMetaKeywords()
        {
            //Find all meta tags
            var metaTags = _document.DocumentNode.Descendants("meta")
                .Where(node => node.Attributes.Contains("content")).ToArray();

            if (!metaTags.Any()) return "No meta tags found";

            var keywordTag = metaTags.FirstOrDefault(p => p.Attributes.Contains("name") &&
                                                          p.Attributes["name"].Value.Contains("keyword"));

            if (keywordTag == null)
                return "No keyword meta tag found";

            return $"Found meta keyword: '{keywordTag.Attributes["content"].Value}'";
        }

        public static List<string> AnalyzeKeywords(string htmlContent)
        {
            return new KeywordAnalyzer(htmlContent).AnalyzeKeywords();
        }
    }
}