namespace Contoso.HAServer.RateLimitService.Interfaces
{
    public interface IRateLimitService
    {
        bool IsRateLimitReach(string clientId);
    }
}
