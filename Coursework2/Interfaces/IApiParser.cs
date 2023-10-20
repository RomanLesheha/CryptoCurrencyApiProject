namespace Coursework2.Interfaces
{
    public interface IApiParser
    {
        Task<T> ParseAsync<T>(string endpoint);
    }
}
