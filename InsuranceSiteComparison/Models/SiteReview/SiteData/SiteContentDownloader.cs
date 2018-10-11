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
            SetupDocRefs(doc, resourceUrl);
            return new SiteContent()
            {
                ContentUrl = resourceUrl,
                Content = doc.DocumentNode.OuterHtml,
                TimeToDownload = timer.Elapsed
            };
        }

        private static void SetupDocRefs(HtmlDocument doc, string urlPath)
        {
            var imgs = doc.DocumentNode.Descendants("img").Where(p => p.Attributes["src"].Value.StartsWith("/"));
            foreach (var htmlNode in imgs)
            {
                var attrib = htmlNode.Attributes["src"];
                var currentPath = attrib.Value;
                attrib.Value = $"{urlPath}{currentPath}";
            }

            var css = doc.DocumentNode.Descendants("link").Where(p => p.Attributes.Contains("href") &&
                                                                      p.Attributes["href"].Value.StartsWith("/"));
            foreach (var htmlNode in css)
            {
                var attrib = htmlNode.Attributes["href"];
                var currentPath = attrib.Value;
                attrib.Value = $"{urlPath}{currentPath}";
            }

            doc.DocumentNode.Descendants()
                .Where(n => n.Name == "script")
                .ToList()
                .ForEach(n => n.Remove());

            //Lastly replace url('/ in html style to allow for background images!
            var nodes = doc.DocumentNode.Descendants("style").ToList();
            foreach (var htmlNode in nodes)
            {
                if (htmlNode.InnerHtml.Contains("url('/"))
                    htmlNode.InnerHtml = htmlNode.InnerHtml.Replace("url('/", $"url('{urlPath}/");
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