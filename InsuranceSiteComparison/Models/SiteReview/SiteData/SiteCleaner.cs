using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;

namespace InsuranceSiteComparison.Models.SiteReview.SiteData
{
    /// <summary>
    /// Cleans the site HTML by making relative URLs absolute to original site.
    /// </summary>
    public class SiteCleaner
    {
        private readonly HtmlDocument _document;
        private readonly string _url;

        private SiteCleaner(string uncleanHtml, string url)
        {
            _document = new HtmlDocument();
            _document.LoadHtml(uncleanHtml);
            _url = url;
        }

        /// <summary>
        /// Parses the site content's HTML and turns relative URLs into absolute URLs as well are removing javascript
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string CleanSiteHtml(SiteContent content)
        {
            return new SiteCleaner(content.Content, content.ContentUrl).Clean();
        }

        private string Clean()
        {
            RemoveAllScripts();

            //TODO Condense methods  
            FixStyleUrls();
            FixExternalStyleSheetUrls();
            FixImageSourceUrls();
            FixAnchorTags();
            return _document.DocumentNode.OuterHtml;
        }

        private void FixImageSourceUrls()
        {
            var imgs = _document.DocumentNode.Descendants("img").Where(p => p.Attributes["src"].Value.StartsWith("/"));
            foreach (var htmlNode in imgs)
            {
                var attrib = htmlNode.Attributes["src"];
                var currentPath = attrib.Value;
                attrib.Value = $"{_url}{currentPath}";
            }
        }

        private void FixExternalStyleSheetUrls()
        {
            var css = _document.DocumentNode.Descendants("link").Where(p => p.Attributes.Contains("href") &&
                                                                                  p.Attributes["href"].Value.StartsWith("/"));
            foreach (var htmlNode in css)
            {
                var attrib = htmlNode.Attributes["href"];
                var currentPath = attrib.Value;
                attrib.Value = $"{_url}{currentPath}";
            }
        }

        private void RemoveAllScripts()
        {
            _document.DocumentNode.Descendants()
                .Where(n => n.Name == "script")
                .ToList()
                .ForEach(n => n.Remove());
        }

        private void FixAnchorTags()
        {
            //Get anchor tags and make them point to the true site
            var anchorNodes = _document.DocumentNode.Descendants("a").Where(p => p.Attributes.Contains("href")).ToList();
            foreach (var htmlNode in anchorNodes)
            {
                var hrefAttr = htmlNode.Attributes["href"];
                if (hrefAttr.Value.StartsWith("/"))
                    hrefAttr.Value = $"{_url}{hrefAttr.Value}";
            }
        }

        private void FixStyleUrls()
        {
            //Lastly replace url('/ in html style to allow for background images!
            var styleNodes = _document.DocumentNode.Descendants("style").ToList();
            foreach (var htmlNode in styleNodes)
            {
                if (htmlNode.InnerHtml.Contains("url('/"))
                    htmlNode.InnerHtml = htmlNode.InnerHtml.Replace("url('/", $"url('{_url}/");
            }
        }
    }
}