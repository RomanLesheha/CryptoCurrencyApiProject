namespace Coursework2.Models.CoinMarketCapApiModels
{
    public class CryptoCurrencyCategoryMetaData
    {
        public CategoryData data { get; set; }
        public ResponseStatus status { get; set; }
    }

    public class CategoryData : CryptoCurrencyCategoryData
    {
        public List<Data> coins { get; set; }

    }

}
