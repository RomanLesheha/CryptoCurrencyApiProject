namespace Coursework2.Models
{
    public class CryptoCurrencyMetaData
    {
        public Dictionary<string, DataItem> Data { get; set; }
        public ResponseStatus Status { get; set; }
    }
    public class Urls
    {
        public List<string> Website { get; set; }
        public List<string> TechnicalDoc { get; set; }
        public List<string> Twitter { get; set; }
        public List<string> Reddit { get; set; }
        public List<string> MessageBoard { get; set; }
        public List<string> Announcement { get; set; }
        public List<string> Chat { get; set; }
        public List<string> Explorer { get; set; }
        public List<string> SourceCode { get; set; }
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
        public Platform Platform { get; set; }
        public string Category { get; set; }
        public object Notice { get; set; }
    }
}
