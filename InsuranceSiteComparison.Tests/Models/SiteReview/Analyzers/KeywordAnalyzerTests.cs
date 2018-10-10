using Microsoft.VisualStudio.TestTools.UnitTesting;
using InsuranceSiteComparison.Models.SiteReview.Analyzers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceSiteComparison.Models.SiteReview.Analyzers.Tests
{
    [TestClass()]
    public class KeywordAnalyzerTests
    {
        [TestMethod()]
        public void AnalyzeKeywordsTest()
        {

        }
        [TestMethod()]
        public void AnalyzeMetaKeyword_NoMeta_Test()
        {
            const string expected = "No meta tags found";

            var html = string.Empty;
            var actual = KeywordAnalyzer.AnalyzeKeywords(html);

            Assert.IsTrue(actual.Contains(expected), $"Expected results to contain: '{expected}'");
        }
        [TestMethod()]
        public void AnalyzeMetaKeyword_NoKeywordMeta_Test()
        {
            const string expected = "No keyword meta tag found";

            var html = @"<meta name='someMeta'>some value </meta>";
            var actual = KeywordAnalyzer.AnalyzeKeywords(html);

            Assert.IsTrue(actual.Contains(expected), $"Expected results to contain: '{expected}'");
        }

        [TestMethod()]
        public void AnalyzeMetaKeyword_KeywordMeta_Found_Test()
        {
            const string expected = "Found meta keyword: 'Pet Insurance, Car Insurance, Vehicle Insurance'";

            var html = @"<meta name='keywords' content='Pet Insurance, Car Insurance, Vehicle Insurance'>";
            var actual = KeywordAnalyzer.AnalyzeKeywords(html);

            Assert.IsTrue(actual.Contains(expected), $"Expected results to contain: '{expected}'. Actual: {string.Join(", ",actual)}");
        }
    }
}