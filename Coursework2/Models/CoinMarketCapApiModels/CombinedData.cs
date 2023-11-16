namespace Coursework2.Models.CoinMarketCapApiModels
{
    public class CombinedData
    {
        public Data Data { get; set; } // Для першого джерела
        public CryptoCurrencyMetaData MetaData { get; set; } // Для другого джерела
    }
}
