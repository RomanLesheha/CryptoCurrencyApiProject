using Coursework2.Factories.Interfaces;
using Coursework2.Interfaces;
using Coursework2.Parsers;

namespace Coursework2.Factories.Implementations
{
    public class ApiServiceFactory : IApiParserFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public enum ApiParserType
        {
            CoinCap,
            CoinMarketCap

        }
        public ApiServiceFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public IApiParser GetApiParser(ApiParserType apiParserType)
        {
            switch (apiParserType)
            {
                case ApiParserType.CoinCap:
                    return _serviceProvider.GetRequiredService<CoinCapDataParser>();
                case ApiParserType.CoinMarketCap:
                    return _serviceProvider.GetRequiredService<CoinMarketCapDataParser>();
                default:
                    throw new ArgumentException($"Unsupported parser type: {apiParserType}");
            }
        }
    }
}
