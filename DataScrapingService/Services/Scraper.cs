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

            var xmlDoc = new XmlDocument();
            var root = xmlDoc.CreateElement("root");
            xmlDoc.AppendChild(root);

            foreach (var node in htmlDocument.DocumentNode.SelectNodes(XPaths.VehicleCardXPath))
            {
                var vehicleElement = xmlDoc.CreateElement("vehicle");

                var titleNode = node.SelectSingleNode(XPaths.TitleXPath);
                if (titleNode != null)
                {
                    var titleElement = xmlDoc.CreateElement("title");
                    titleElement.InnerText = titleNode.InnerText;
                    vehicleElement.AppendChild(titleElement);
                }

                var priceNode = node.SelectSingleNode(XPaths.PriceXPath);
                if (priceNode != null)
                {
                    var priceElement = xmlDoc.CreateElement("price");
                    priceElement.InnerText = priceNode.InnerText;
                    vehicleElement.AppendChild(priceElement);
                }

                var mediaNode = node.SelectSingleNode(XPaths.MediaXPath);
                if (mediaNode != null)
                {
                    var mediaElement = xmlDoc.CreateElement("media");
                    mediaElement.InnerText = mediaNode.InnerText;
                    vehicleElement.AppendChild(mediaElement);
                }

                var footerNode = node.SelectSingleNode(XPaths.FooterXPath);
                if (footerNode != null)
                {
                    var footerElement = xmlDoc.CreateElement("footer");
                    footerElement.InnerText = footerNode.InnerText;
                    vehicleElement.AppendChild(footerElement);
                }

                root.AppendChild(vehicleElement);
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
