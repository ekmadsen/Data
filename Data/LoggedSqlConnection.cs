using System;
using System.Data.SqlClient;
using ErikTheCoder.Logging;


namespace ErikTheCoder.Data
{
    public class LoggedSqlConnection : LoggedDbConnection
    {
        public LoggedSqlConnection(ILogger Logger, Func<Guid> GetCorrelationId, string Connection) : base(Logger, GetCorrelationId)
        {
            this.Connection = new SqlConnection(Connection);
        }
    }
}
