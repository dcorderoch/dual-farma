using System.Data;

namespace dual_farma.DAL
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