namespace Coursework2.Models
{
    public class CryptoCurrenciesListMetaData
    {
        public List<Data> data { get; set; }
        public ResponseStatus status { get; set; }
    }
    public class QuoteInfo
    {
        public decimal price { get; set; }
        public decimal volume_24h { get; set; }
        public decimal volume_change_24h { get; set; }
        public decimal percent_change_1h { get; set; }
        public decimal percent_change_24h { get; set; }
        public decimal percent_change_7d { get; set; }
        public decimal market_cap { get; set; }
        public decimal market_cap_dominance { get; set; }
        public decimal fully_diluted_market_cap { get; set; }
        public DateTime last_updated { get; set; }
    }


    public class Data
    {
        public int id { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public string ?urlLogo {get;set;}
        public string slug { get; set; }
        public int cmc_rank { get; set; }
        public int num_market_pairs { get; set; }
        public double circulating_supply { get; set; }
        public double total_supply { get; set; }
        public double ?max_supply { get; set; }
        public bool infinite_supply { get; set; }
        public DateTime last_updated { get; set; }
        public DateTime date_added { get; set; }
        public List<string> tags { get; set; }
        public object platform { get; set; }
        public object self_reported_circulating_supply { get; set; }
        public object self_reported_market_cap { get; set; }
        public Dictionary<string, QuoteInfo> quote { get; set; }
    }
}
