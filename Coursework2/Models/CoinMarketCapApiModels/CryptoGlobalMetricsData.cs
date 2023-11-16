namespace Coursework2.Models.CoinMarketCapApiModels
{
    public class CryptoGlobalMetricsData
    {
        public DataMetrcics Data { get; set; }
        public ResponseStatus Status { get; set; }
    }

    public class DataMetrcics
    {
        public int active_cryptocurrencies { get; set; }
        public int total_cryptocurrencies { get; set; }
        public int active_market_pairs { get; set; }
        public int active_exchanges { get; set; }
        public int total_exchanges { get; set; }
        public double eth_dominance { get; set; }
        public double btc_dominance { get; set; }
        public double eth_dominance_yesterday { get; set; }
        public double btc_dominance_yesterday { get; set; }
        public double eth_dominance_24h_percentage_change { get; set; }
        public double btc_dominance_24h_percentage_change { get; set; }
        public double defi_volume_24h { get; set; }
        public double defi_volume_24h_reported { get; set; }
        public double defi_market_cap { get; set; }
        public double defi_24h_percentage_change { get; set; }
        public double stablecoin_volume_24h { get; set; }
        public double stablecoin_volume_24h_reported { get; set; }
        public double stablecoin_market_cap { get; set; }
        public double stablecoin_24h_percentage_change { get; set; }
        public double derivatives_volume_24h { get; set; }
        public double derivatives_volume_24h_reported { get; set; }
        public double derivatives_24h_percentage_change { get; set; }
        public Dictionary<string, QuoteData> Quote { get; set; }
        public DateTime last_updated { get; set; }
    }

    public class QuoteData
    {
        public double total_market_cap { get; set; }
        public double total_volume_24h { get; set; }
        public double total_volume_24h_reported { get; set; }
        public double altcoin_volume_24h { get; set; }
        public double altcoin_volume_24h_reported { get; set; }
        public double altcoin_market_cap { get; set; }
        public double defi_volume_24h { get; set; }
        public double defi_volume_24h_reported { get; set; }
        public double defi_24h_percentage_change { get; set; }
        public double defi_market_cap { get; set; }
        public double stablecoin_volume_24h { get; set; }
        public double stablecoin_volume_24h_reported { get; set; }
        public double stablecoin_24h_percentage_change { get; set; }
        public double stablecoin_market_cap { get; set; }
        public double derivatives_volume_24h { get; set; }
        public double derivatives_volume_24h_reported { get; set; }
        public double derivatives_24h_percentage_change { get; set; }
        public double total_market_cap_yesterday { get; set; }
        public double total_volume_24h_yesterday { get; set; }
        public double total_market_cap_yesterday_percentage_change { get; set; }
        public double total_volume_24h_yesterday_percentage_change { get; set; }
        public DateTime last_updated { get; set; }
    }
}