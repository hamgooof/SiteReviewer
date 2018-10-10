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
        public static KeywordResult AnalyzeAccessibility(SiteContent content)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(content.Content);
            var web = new HtmlWeb();
            
            var imageTags = doc.DocumentNode.Descendants("img").ToList();
            var imageTagsWithAlt23 = imageTags.Where(p => p.Attributes.Any(a => a.Name == "alt")).ToList();

            var imageTagsWithAlt = imageTags.Count(p => p.Attributes.Any(a => a.Name == "alt"));
            Console.WriteLine($"{imageTagsWithAlt} of {imageTags.Count} images have alt tags");
            return null;
        }
    }
}