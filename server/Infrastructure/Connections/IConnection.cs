using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Infrastructure.Connections
{
    /// <summary>
    /// The idea here is to extend different connections to different database types
    /// </summary>
    public interface IConnection
    {
        IDbConnection GetConnection();
    }
}
