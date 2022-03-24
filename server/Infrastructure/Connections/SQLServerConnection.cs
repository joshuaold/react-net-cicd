using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Infrastructure.Connections
{
    public class SQLServerConnection : IConnection
    {
        private IDbConnection _connection;
        private string _connectionString;

        public SQLServerConnection(string sqlConnectionString)
        {
            _connectionString = sqlConnectionString;
        }

        public IDbConnection GetConnection()
        {
            if (_connection != null)
            {
                return _connection;
            }
            else
            {
                return new SqlConnection(_connectionString);
            }
        }
    }
}
