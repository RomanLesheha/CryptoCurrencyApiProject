using System.Text.Json.Serialization;

namespace Coursework2.Models
{
    public class Assets
    {
        [JsonPropertyName("Id")]
        public string id { get; set; }
        [JsonPropertyName("Rank")]
        public string rank { get; set; }
        public string symbol { get; set; }
        public string name { get; set; }
        public string supply { get; set; }
        public string maxSupply { get; set; }
        public string marketCapUsd { get; set; }
        public string volumeUsd24Hr { get; set; }
        public decimal priceUsd { get; set; }
        public string changePercent24Hr { get; set; }
        public string vwap24Hr { get; set; }

        public string explorer { get; set; }

    }

    public class AssetsResponse
    {
        public List<Assets> data { get; set; }
    }

    public class SingleAssetResponse
    {
        public Assets data { get; set; }
        public long timestamp { get; set; }
    }
}
