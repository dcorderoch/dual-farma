using System.Data;

namespace farma_tica.DAL
{
    /// <summary>
    /// Creates, opens and retrieves DB conections.
    /// </summary>
    public interface IConnectionFactory
    {
        /// <summary>
        /// Create and retrieve a new connection
        /// </summary>
        /// <returns>Open and valid connection to DB</returns>
        IDbConnection CreateConnection();
    }
}