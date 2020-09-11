using System;
using System.Data.Common;
using System.Threading.Tasks;
using ErikTheCoder.Logging;
using JetBrains.Annotations;


namespace ErikTheCoder.Data
{
    [UsedImplicitly]
    public class LoggedSqlDatabase : LoggedDatabase
    {
        public LoggedSqlDatabase(ILogger Logger, string Connection) : base(Logger, Connection)
        {
        }


        public override async Task<DbConnection> OpenConnectionAsync(Guid CorrelationId)
        {
            var connection = new LoggedSqlConnection(Logger, CorrelationId, Connection);
            await connection.OpenAsync();
            return connection;
        }
    }
}
