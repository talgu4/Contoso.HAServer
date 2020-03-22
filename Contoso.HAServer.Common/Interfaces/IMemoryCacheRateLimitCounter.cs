namespace Contoso.HAServer.Common.Interfaces
{
    public interface IMemoryCacheRateLimitCounter
    {
        RateLimitCounter GetOrCreate(string clientId);
    }
}
