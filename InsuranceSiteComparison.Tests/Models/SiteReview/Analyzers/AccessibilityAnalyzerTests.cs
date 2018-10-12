using Microsoft.VisualStudio.TestTools.UnitTesting;
using InsuranceSiteComparison.Models.SiteReview.Analyzers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceSiteComparison.Models.SiteReview.SiteData;

namespace InsuranceSiteComparison.Models.SiteReview.Analyzers.Tests
{
    [TestClass()]
    public class AccessibilityAnalyzerTests
    {
        [TestMethod()]
        public void AnalyzeAccessibilityTest()
        {
            var expectedResults = new List<string>()
            {
                "75% of image tags have 'alt' attributes (3 of 4)"
            };

            var testHtml = GetHtml();
            var actual = new AccessibilityAnalyzer(testHtml).AnalyzeHtml();

            foreach (var actualResult in actual)
            {
                Console.WriteLine(actualResult);
            }

            foreach (var expectedResult in expectedResults)
            {
                Assert.IsTrue(actual.Contains(expectedResult));
            }

        }

        private string GetHtml()
        {
            return @"
                <img src='/image.jpg'/>
                <img src='/image2.jpg' alt='My image 2'/>
                <img src='/image3.jpg' alt='My image 3'/>
                <img src='/image4.jpg' alt='My image 4'/>";
        }
    }
}