using System;
using System.Data.SqlClient;
using ErikTheCoder.Logging;


namespace ErikTheCoder.Data
{
    public class LoggedSqlConnection : LoggedDbConnection
    {
        public LoggedSqlConnection(ILogger Logger, Guid CorrelationId, string Connection) : base(Logger, CorrelationId)
        {
            this.Connection = new SqlConnection(Connection);
        }
    }
}
