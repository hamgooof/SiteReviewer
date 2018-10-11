using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using InsuranceSiteComparison.Models.SiteReview;

namespace InsuranceSiteComparison.Controllers
{
    [RoutePrefix("api")]
    public class HomeApiController : ApiController
    {
        [HttpPost]
        [Route("CompareSites")]
        public List<SiteReview> CompareSites(List<string> sitesToCompare)
        {
            var results = new List<SiteReview>();

            foreach (var siteUrl in sitesToCompare)
                results.Add(SiteReviewer.ReviewSite(siteUrl));

            return results;
        }
    }
}
