using System;
using System.Configuration;
using System.Data;
using System.Data.Common;

namespace farma_tica.DAL
{
    /// <summary>
    ///     This class creates and manages the database connection
    /// </summary>
    internal class DbConnectionFactory : IConnectionFactory
    {
        private readonly string connectionString;
        private string name;
        private readonly DbProviderFactory providerFactory;

        /// <summary>
        ///     Class Constructor, it retrieves connection string from the configuration file and initializes the other class
        ///     properties.
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
            name = conStr.ProviderName;
            providerFactory = DbProviderFactories.GetFactory(conStr.ProviderName);
            connectionString = conStr.ConnectionString;
        }

        /// <summary>
        ///     Creates a connection to the database detailed in the configuration file
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