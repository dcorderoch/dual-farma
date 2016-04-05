using System;
using System.Data;

namespace farma_tica.DAL
{
    /// <summary>
    ///     Implementation of unit of work
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Action<UnitOfWork> mCommited;
        private readonly Action<UnitOfWork> mRolledBack;
        private IDbTransaction mTransaction;

        /// <summary>
        ///     Creates a new unit of work
        /// </summary>
        /// <param name="transaction">this is the transaction that is used as a UnitOfWork</param>
        /// <param name="rolledBack">this is used when the UoW is rolled back</param>
        /// <param name="commited">this is used when the UoW commits its changes to the DB</param>
        public UnitOfWork(IDbTransaction transaction, Action<UnitOfWork> rolledBack, Action<UnitOfWork> commited)
        {
            Transaction = transaction;
            mTransaction = transaction;
            mRolledBack = rolledBack;
            mCommited = commited;
        }

        public IDbTransaction Transaction { get; private set; }

        /// <summary>
        ///     Method to release resources
        /// </summary>
        public void Dispose()
        {
            if (mTransaction == null) return;

            mTransaction.Rollback();
            mTransaction.Dispose();
            mRolledBack(this);
            mTransaction = null;
        }

        /// <summary>
        ///     Save changes into the DataBase
        /// </summary>
        public void SaveChanges()
        {
            if (mTransaction == null)
            {
                throw new InvalidOperationException("The transaction has been already commited!");
            }

            mTransaction.Commit();
            mCommited(this);
            mTransaction = null;
        }
    }
}