using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ErikTheCoder.Logging;
using JetBrains.Annotations;


namespace ErikTheCoder.Data
{
    [UsedImplicitly]
    public class SqlDatabase : Database
    {
        public SqlDatabase(ILogger Logger, Func<Guid> GetCorrelationId, string Connection) : base(Logger, GetCorrelationId, Connection)
        {
        }


        public override async Task<DbConnection> OpenConnectionAsync(bool LogCommands)
        {
            DbConnection connection = LogCommands
                ? (DbConnection) new LoggedSqlConnection(Logger, GetCorrelationId, Connection)
                : new SqlConnection(Connection);
            await connection.OpenAsync();
            return connection;
        }
    }
}
