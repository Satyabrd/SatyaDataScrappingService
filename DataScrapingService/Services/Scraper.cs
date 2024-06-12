using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Xsl;
using HtmlAgilityPack;
using DataScrapingService.Config;
using Microsoft.Extensions.Configuration;

namespace DataScrapingService.Services
{
    public class Scraper
    {
        private readonly HttpClient _httpClient;
        private readonly string _targetUrl;

        public Scraper(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _targetUrl = configuration["AppConfig:TargetUrl"];
        }

        public async Task ScrapeAndTransformAsync()
        {
            var html = await _httpClient.GetStringAsync(_targetUrl);

            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(html);

            var nodes = htmlDocument.DocumentNode.SelectNodes(XPaths.HeadlineXPath);

            var xmlDoc = new XmlDocument();
            var root = xmlDoc.CreateElement("root");
            xmlDoc.AppendChild(root);

            foreach (var node in nodes)
            {
                var element = xmlDoc.CreateElement("h1");
                element.InnerText = node.InnerText;
                root.AppendChild(element);
            }

            var xslt = new XslCompiledTransform();
            xslt.Load("transform.xslt");

            using (var writer = XmlWriter.Create("transformed.html"))
            {
                xslt.Transform(xmlDoc, writer);
            }

            Console.WriteLine("Transformation complete. Check transformed.html");
        }
    }
}
