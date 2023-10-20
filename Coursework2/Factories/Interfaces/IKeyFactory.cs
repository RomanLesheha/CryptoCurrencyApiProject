namespace Coursework2.Factories.Interfaces
{
    public interface IKeyFactory
    {
        Task<string> GetNextValidAPIKeyAsync();
        Task<bool> IsApiKeyValid(string apiKey);
        string BuildUrl(string endpoint, Dictionary<string, object> queryParams);
        string[] ApiKeys { get; }
    }
}
