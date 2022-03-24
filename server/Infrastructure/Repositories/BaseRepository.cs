using Core.Interfaces.Domain;
using Core.Interfaces.Repositories;
using Dapper;
using Dapper.Contrib.Extensions;
using Infrastructure.Connections;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IAggregateRoot
    {
        private readonly IConnection _connection;

        public BaseRepository(IConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var openConnection = GetOpenConnection();
            using (openConnection)
            {
                return await openConnection.GetAllAsync<T>();
            }
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var openConnection = GetOpenConnection();
            using (openConnection)
            {
                return await openConnection.GetAsync<T>(id);
            }
        }

        public async Task UpdateAsync(T entity)
        {
            var openConnection = GetOpenConnection();
            using (openConnection)
            {
                await openConnection.UpdateAsync(entity);
            }
        }

        public IDbConnection GetOpenConnection()
        {
            var connection = _connection.GetConnection();
            connection.Open();
            return connection;
        }
    }
}
