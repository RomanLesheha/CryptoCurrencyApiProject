namespace Coursework2.Models
{
    public class CryptoCurrencyCategoryData
    {
        public string id { get; set; }
        public string name { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public double circulating_supply { get; set; }
        public int num_tokens { get; set; }
        public double? avg_price_change { get; set; }
        public double market_cap { get; set; }
        public double? market_cap_change { get; set; }
        public double volume { get; set; }

        public double? volume_change { get; set; }

        public DateTime last_updated { get; set; }
    }

    public class CryptoCurrencyCategoryList
    {
        public List<CryptoCurrencyCategoryData> data { get; set; }
        public ResponseStatus status { get; set; }
    }

    public static class CryptoCurrencyCategoryListExtensions
    {
        public static CryptoCurrencyCategoryList SortByAvgPriceChange(this CryptoCurrencyCategoryList list)
        {
            if (list != null && list.data != null)
            {
                list.data = list.data.OrderBy(item => item.avg_price_change).ToList();
            }
            list.data.Reverse();
            return list;
        }
    }
}
