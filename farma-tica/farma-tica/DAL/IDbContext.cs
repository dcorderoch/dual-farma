using System.Data;

namespace farma_tica.DAL
{
    /// <summary>
    ///     DB context
    /// </summary>
    public interface IDbContext
    {
        /// <summary>
        ///     Creates a unit of work implementation
        /// </summary>
        /// <returns>A unit of work to play with</returns>
        IUnitOfWork CreateUnitOfWork();

        /// <summary>
        ///     Creates a command.
        /// </summary>
        /// <returns>A command.</returns>
        IDbCommand CreateDbCommand();
    }
}