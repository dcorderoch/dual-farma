using System.Data;
using System.Security.Cryptography.X509Certificates;

namespace FarmaticaCore.DAL
{
    /// <summary>
    /// DB context 
    /// </summary>
    public interface IDbContext
    {
        /// <summary>
        /// Creates a unit of work implementation
        /// </summary>
        /// <returns>A unit of work to play with</returns>
        IUnitOfWork CreateUnitOfWork();

        /// <summary>
        /// Creates a command.
        /// </summary>
        /// <returns>A command.</returns>
        IDbCommand CreateDbCommand();
    }
}