using Money.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Money.Repository
{
    public class AzureDbConnectionHelper : IDatabaseConnection
    {
        public  string _connectionString;

        public AzureDbConnectionHelper() {
            _connectionString = ConfigurationManager.ConnectionStrings["AzureDb"].ConnectionString;
        }
        /// <summary>
        /// Create DbConnection
        /// </summary>
        /// <returns></returns>
        public IDbConnection Create()
        {
            var sqlConnection = new SqlConnection(_connectionString);
            return sqlConnection;
        }
    }
}
