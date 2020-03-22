namespace Contoso.HAServer.Core.Interfaces
{
    public interface IRateLimitCore
    {
        long HandleClient(string clientId);
    }
}
