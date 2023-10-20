namespace Coursework2.Models
{
    public class CryptoCurrency
    {
        public List<Datum> Data { get; set; }
        public ResponseStatus Status { get; set; }
    }
    public class Platform
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Slug { get; set; }
        public string TokenAddress { get; set; }
    }

    public class Datum
    {
        public int Id { get; set; }
        public int Rank { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public string Slug { get; set; }
        public int IsActive { get; set; }
        public DateTime FirstHistoricalData { get; set; }
        public DateTime LastHistoricalData { get; set; }
        public Platform Platform { get; set; }
    }
}
