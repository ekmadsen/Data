using System;
using System.Data.Common;
using System.Threading.Tasks;
using ErikTheCoder.Logging;


namespace ErikTheCoder.Data
{
    public abstract class LoggedDatabase : ILoggedDatabase
    {
        // Do not implement dispose pattern for logger.  Its lifetime is controlled by caller.
        protected readonly ILogger Logger;
        protected readonly string Connection;


        protected LoggedDatabase(ILogger Logger, string Connection)
        {
            this.Logger = Logger;
            this.Connection = Connection;
        }


        public abstract DbConnection OpenConnection(Guid CorrelationId);
        public abstract Task<DbConnection> OpenConnectionAsync(Guid CorrelationId);
    }
}