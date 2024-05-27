namespace Wanted.Persistence;

public interface IAppContext
{
    /*
     * Verify that we can connect to the database.
     * Great for health checks.
     */
    Task<bool> CanConnectAsync();
}
