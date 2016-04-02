using System.Collections.Generic;
using System.Data;
using System.Dynamic;

namespace dual_farma.DAL.Repositories
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

        protected DbContext Context { get; }

        /// <summary>
        /// Method to retrieve all elements from table
        /// </summary>
        /// <returns>List of TEntity items</returns>
        public abstract IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Insert new TEntity into repository
        /// </summary>
        /// <param name="entity"> Entity to insert </param>
        public abstract void Create(TEntity entity);

        /// <summary>
        /// Get all entities that contains the given id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>List of found entities</returns>
        public abstract IEnumerable<TEntity> GetById(object id);

        /// <summary>
        /// Updates existing entity 
        /// </summary>
        /// <param name="entity"></param>
        public abstract void Update(TEntity entity);

        /// <summary>
        /// Deletes entity by its id
        /// </summary>
        /// <param name="id"></param>
        public abstract void DeleteById(object id);


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