using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using HtmlAgilityPack;
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

            var web = new HtmlWeb();
            var doc = web.Load(resourceUrl);

            timer.Stop();

            return new SiteContent()
            {
                ContentUrl = resourceUrl,
                Content = doc.ParsedText,
                TimeToDownload = timer.Elapsed
            };
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