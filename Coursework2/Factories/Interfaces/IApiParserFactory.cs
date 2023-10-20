using Coursework2.Interfaces;
using static Coursework2.Factories.Implementations.ApiServiceFactory;

namespace Coursework2.Factories.Interfaces
{
    public interface IApiParserFactory
    {
        IApiParser GetApiParser(ApiParserType apiParserType);
    }
}
