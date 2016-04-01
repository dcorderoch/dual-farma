using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dual_farma.DAL
{
    /// <summary>
    /// This class creates and manages the database connection
    /// </summary>
    class DbConnectionFactory : IConnectionFactory
    {
        private DbProviderFactory providerFactory;
        private string connectionString;
        private string name;

        /// <summary>
        /// Class Constructor, it retrieves connection string from the configuration file and initializes the other class properties.
        /// </summary>
        /// <param name="conName"> Connection String in app.config file</param>
        public DbConnectionFactory(string conName)
        {
            if (conName == null)
            {
                throw new ArgumentNullException("conName");
            }
            var conStr = ConfigurationManager.ConnectionStrings[conName];
            if (conStr == null)
            {
                throw new ConfigurationErrorsException("Failed to find connection string name in app.config.");
            }
            this.name = conStr.ProviderName;
            this.providerFactory = DbProviderFactories.GetFactory(conStr.ProviderName);
            this.connectionString = conStr.ConnectionString;
        }

        /// <summary>
        /// Creates a connection to the database detailed in the configuration file
        /// </summary>
        /// <returns>Opened conection to the database</returns>
        public IDbConnection CreateConnection()
        {
            var connection = providerFactory.CreateConnection();
            if (connection == null)
            {
                throw new ConfigurationErrorsException("Failed to create a connection using the connection string ");
            }
            connection.ConnectionString = connectionString;
            connection.Open();
            return connection;

        }
    }
}
