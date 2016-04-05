using System;

namespace farma_tica.DAL
{
    /// <summary>
    ///     Interface pf the Unit of Work
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        ///     Save changes into the database
        /// </summary>
        void SaveChanges();
    }
}