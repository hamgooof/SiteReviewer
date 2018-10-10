using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InsuranceSiteComparison.Models.SiteReview
{
    /// <summary>
    /// Overview review of a site
    /// </summary>
    public class SiteReview
    {
        /// <summary>
        /// Main URL of the site being reviewed
        /// </summary>
        public string URL { get; set; }
        /// <summary>
        /// Total time to load the web page
        /// </summary>
        public TimeSpan TimeToLoad { get; set; }
        /// <summary>
        /// Result of accessibility on the website
        /// </summary>
        public List<string> AccessibilityResult { get; set; }
        /// <summary>
        /// Keyword frequency result
        /// </summary>
        public List<string> KeywordResult { get; set; }


        public SiteReview(string url)
        {
            URL = url;
        }
    }

    /// <summary>
    /// Key: Keyword,
    /// Value: Count
    /// </summary>
    public class KeywordResult : Dictionary<string, int>
    {
    }

    public class AccessibilityResult : Dictionary<AccessibilityTestType, double>
    {
    }
    public enum AccessibilityTestType { ImageAlt }
}