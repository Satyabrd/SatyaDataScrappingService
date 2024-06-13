namespace DataScrapingService.Config
{
    public static class XPaths
    {
        public const string VehicleCardXPath = "//li[contains(@class, 'vehicle-card')]";
        public const string TitleXPath = ".//span[@data-testid='title-skeleton']";
        public const string PriceXPath = ".//span[@data-testid='price-skeleton']";
        public const string MediaXPath = ".//span[@data-testid='media-skeleton']";
        public const string FooterXPath = ".//span[@data-testid='footer-skeleton']";
    }
}
