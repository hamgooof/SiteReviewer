using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using Newtonsoft.Json;

namespace InsuranceSiteComparison.Models.SiteReview.SiteData
{
    public class SiteContent
    {
        //public int? ContentSize => Content?.Length;
        //public int? CompressedSize { get; set; }
        //public DecompressionMethods DecompressionMethod { get; set; }
        public string ContentUrl { get; set; }
        public string ContentType { get; set; }
        public TimeSpan TimeToDownload { get; set; }
        [JsonIgnore]
        public string Content { get; set; }

    }
}