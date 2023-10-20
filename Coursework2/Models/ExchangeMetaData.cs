namespace Coursework2.Models
{
    public class ExchangeMetaData
    {
        public Dictionary<string, ExchangeInfo> Data { get; set; }
        public ResponseStatus Status { get; set; }
    }
    public class ExchangeInfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string slug { get; set; }
        public string description { get; set; }
        public string logo { get; set; }
        public List<object> countries { get; set; }
        public List<string> fiats { get; set; }
        public List<ExchangeTags> ?tags { get; set; }
        public string type { get; set; }
        public double ?maker_fee { get; set; }
        public double ?taker_fee { get; set; }
        public long ?weekly_visits { get; set; }
        public double ?spot_volume_usd { get; set; }
        public DateTime ?spot_volume_last_updated { get; set; }
        public ExchangeUrls urls { get; set; }
        public string date_launched { get; set; }
        public int ?is_hidden { get; set; }
        public int ?is_redistributable { get; set; }
        public int ?porStatus { get; set; }
        public int ?porAuditStatus { get; set; }
        public int ?walletSourceStatus { get; set; }
        public string porSwitch { get; set; }
    }

    public class ExchangeTags
    {
        public string name { get; set; }
        public string slug { get; set; }
        public string group { get; set; }
    }

    public class ExchangeUrls
    {
        public List<string> website { get; set; }
        public List<string> twitter { get; set; }
        public List<object> blog { get; set; }
        public List<string> chat { get; set; }
        public List<string> fee { get; set; }
    }
}
