using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using Newtonsoft.Json;
using HttpWebResponse = System.Net.HttpWebResponse;

namespace InsuranceSiteComparison.Models.SiteReview.SiteData
{
    public class SiteContentDownloader
    {
        public static SiteContent GetContent(string resourceUrl)
        {
            var req = WebRequest.CreateHttp(resourceUrl);
            req.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            var timer = new Stopwatch();
            timer.Start();


            using (var res = req.GetResponse() as HttpWebResponse)
            {
                var data = GetStringFromWebResponse(res);
                timer.Stop();
                return new SiteContent()
                {
                    ContentUrl = resourceUrl,
                    ContentType = res?.ContentType,
                    Content = data.Item1,
                    TimeToDownload = timer.Elapsed
                };
            }
        }

        private static Tuple<string, bool> GetStringFromWebResponse(WebResponse response)
        {
            using (var responseStream = response.GetResponseStream())
            {
                using (var streamReader = new StreamReader(responseStream))
                {
                    var data = streamReader.ReadToEnd();
                    var isCompressed = (responseStream is GZipStream) || (responseStream is DeflateStream);
                    return new Tuple<string, bool>(data, isCompressed);
                }
            }
        }
    }
}