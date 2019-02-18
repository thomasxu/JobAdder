namespace JobMatcher.Application.Interfaces.ApiClients
{
    public interface IApiClient
    {
        T Get<T>(string url = "") where T : new();
    }
}