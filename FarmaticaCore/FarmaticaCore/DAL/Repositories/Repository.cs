using System.Collections.Generic;
using System.Data;

namespace FarmaticaCore.DAL.Repositories
{
    /// <summary>
    /// Abstract Repository class to be implemented in specific repository type
    /// </summary>
    /// <typeparam name="TEntity">Entity</typeparam>
    public abstract class Repository<TEntity> where TEntity : new()
    {
        DbContext mDbContext;

        /// <summary>
        /// Intitialize the repository
        /// </summary>
        /// <param name="context"></param>
        public Repository(DbContext context)
        {
            mDbContext = context;
            Context = context;
        }

        protected DbContext Context { get;}


        protected IEnumerable<TEntity> ToList(IDbCommand command)
        {
            using (var reader = command.ExecuteReader())
            {
                List<TEntity> itemList = new List<TEntity>();
                while (reader.Read())
                {
                    var item = new TEntity();
                    Map(reader, item);
                    itemList.Add(item);
                }
                return itemList;
            } 
        }

        protected abstract void Map(IDataRecord record, TEntity entity);
    }
}