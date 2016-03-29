using System;
using System.Data;
using System.Data.Common;

namespace FarmaticaCore.DAL
{
    /// <summary>
    /// Implementation of unit of work 
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private IDbTransaction mTransaction;
        private Action<UnitOfWork> mRolledBack;
        private Action<UnitOfWork> mCommited;

        public IDbTransaction Transaction { get; private set; }

        /// <summary>
        /// Creates a new unit of work
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
        /// <summary>
        /// Method to release resources
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
        /// Save changes into the DataBase
        /// </summary>
        public void SaveChanges()
        {
            if (mTransaction==null)
            {
                throw new InvalidOperationException("The transaction has been already commited!");
            }

            mTransaction.Commit();
            mCommited(this);
            mTransaction = null;
        }
    }
}