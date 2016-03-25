using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading;

namespace FarmaticaCore.DAL
{
    /// <summary>
    /// Data base context
    /// </summary>
    public class DbContext : IDbContext
    {
        private IDbConnection mConnection;
        private IConnectionFactory mConnectionFactory;
        private ReaderWriterLockSlim mRwLockSlim= new ReaderWriterLockSlim();
        private LinkedList<UnitOfWork> mUowList =new LinkedList<UnitOfWork>();

        /// <summary>
        /// Initialize a new database Context
        /// </summary>
        /// <param name="connectionFactory"></param>
        public DbContext(IConnectionFactory connectionFactory)
        {
            mConnectionFactory = connectionFactory;
            mConnection = mConnectionFactory.CreateConnection();
        } 

        /// <summary>
        /// Creates a unit of work
        /// </summary>
        /// <returns>created unit of work</returns>
        public IUnitOfWork CreateUnitOfWork()
        {
            var transaction = mConnection.BeginTransaction();
            var uow = new UnitOfWork(transaction, RemoveTransaction, RemoveTransaction);
            mRwLockSlim.EnterWriteLock();
            mUowList.AddLast(uow);
            mRwLockSlim.ExitWriteLock();

            return uow;
        }

        /// <summary>
        /// Creates new command 
        /// </summary>
        /// <returns>created command</returns>
        public IDbCommand CreateDbCommand()
        {
            var command = mConnection.CreateCommand();
            mRwLockSlim.EnterReadLock();
            if (mUowList.Count > 0)
            {
                command.Transaction = mUowList.First.Value.Transaction;
            }
            mRwLockSlim.ExitReadLock();
            return command;
        }

        private void RemoveTransaction(UnitOfWork uow)
        {
            mRwLockSlim.EnterWriteLock();
            mUowList.Remove(uow);
            mRwLockSlim.ExitWriteLock();
        }

        /// <summary>
        /// Releases resources
        /// </summary>
        public void Dispose()
        {
            mConnection.Dispose();
        }
    }
}