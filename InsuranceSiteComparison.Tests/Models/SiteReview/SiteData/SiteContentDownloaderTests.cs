using Microsoft.VisualStudio.TestTools.UnitTesting;
using InsuranceSiteComparison.Models.SiteReview.SiteData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InsuranceSiteComparison.Models.SiteReview.Analyzers;
using Newtonsoft.Json;

namespace InsuranceSiteComparison.Models.SiteReview.SiteData.Tests
{
    [TestClass()]
    public class SiteContentDownloaderTests
    {
        [TestMethod()]
        public async Task GetContentTestAsync()
        {
            var result =  SiteContentDownloader.GetContent(@"https://www.morethan.com/");
            Console.WriteLine(JsonConvert.SerializeObject(result));
            AccessibilityAnalyzer.AnalyzeAccessibility(result);
        }
    }
}