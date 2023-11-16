namespace Coursework2.Models.CoinMarketCapApiModels
{
    public class CryptoCurrencyMetaData
    {
        public Dictionary<string, DataItem> Data { get; set; }
        public ResponseStatus Status { get; set; }
    }
    public class Urls
    {
        public List<string> website { get; set; }
        public List<string> technical_doc { get; set; }
        public List<string> twitter { get; set; }
        public List<string> reddit { get; set; }
        public List<string> message_board { get; set; }
        public List<string> announcement { get; set; }
        public List<string> chat { get; set; }
        public List<string> explorer { get; set; }
        public List<string> source_code { get; set; }
    }

    public class DataItem
    {
        public Urls Urls { get; set; }
        public string Logo { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Slug { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateLaunched { get; set; }
        public List<string> Tags { get; set; }
        public List<string> Tags_Name { get; set; }
        public List<string> Tags_Groups { get; set; }
        public Platform Platform { get; set; }
        public string Category { get; set; }
        public object Notice { get; set; }
    }
}

