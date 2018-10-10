using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Web;
using InsuranceSiteComparison.Models.SiteReview.SiteData;

namespace InsuranceSiteComparison.Models.SiteReview
{
    public class SiteReviewer
    {
        private readonly string _siteUrl;

        public SiteReviewer(string siteUrl)
        {
            this._siteUrl = siteUrl;
        }

        public static SiteReview ReviewSite(string url)
        {
            var review = new SiteReviewer(url);


            return review.GetReview();
        }

        private SiteReview GetReview()
        {
            var html = RequestAndTimeResource(_siteUrl);
            var mainPage = SiteContentDownloader.GetContent(_siteUrl);

            
            return null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceUri">Uri for the resource to download</param>
        /// <returns>Tuple<Item 1: resource content, Item2: time to download></Item></returns>
        private Tuple<string, TimeSpan> RequestAndTimeResource(string resourceUri)
        {
            return null;
            //using (var req = WebRequest.CreateHttp(resourceUri))
            //{
            //    req.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            //}
        }
    }
}