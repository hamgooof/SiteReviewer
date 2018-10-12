using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
                FindInsuranceTypes()
            };
        }

        private string FindInsuranceTypes()
        {
            const string insuranceTypeRegexp = @"([A-Za-z0-9\']{1,})\s(insurance)";
            var pattern = new Regex(insuranceTypeRegexp, RegexOptions.Compiled & RegexOptions.IgnoreCase);
            var matches = pattern.Matches(_document.DocumentNode.OuterHtml);
            var exclude = new[] { "on" };

            var matching = matches.Cast<Match>().Select(p => p.Groups[1].Value).ToArray();
            var grped = (from match in matching
                         where !exclude.Contains(match.ToLower())
                         group match by match.ToLower() into g
                         orderby g.Count() descending
                         select new { Insurance = g.Key, Count = g.Count() }).ToArray();

            var textList = grped.Select(p => $"Type: {p.Insurance}, Count: {p.Count}<br/>");
            return string.Join("", textList);

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