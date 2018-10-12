using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HtmlAgilityPack;

namespace InsuranceSiteComparison.Models.SiteReview.Analyzers
{
    public abstract class AbstractHtmlAnalyzer
    {
        protected HtmlDocument _document;

        protected AbstractHtmlAnalyzer(string htmlContent)
        {
            _document = new HtmlDocument();
            _document.LoadHtml(htmlContent);
        }

        public abstract List<string> AnalyzeHtml();
    }
}