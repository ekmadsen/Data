using System;
using System.Data;
using System.Data.Common;
using ErikTheCoder.Logging;


namespace ErikTheCoder.Data
{
    // Cannot inherit from SqlConnection because it's sealed.
    public abstract class LoggedDbConnection : DbConnection
    {
        protected DbConnection Connection;
        private readonly ILogger _logger;
        private readonly Guid _correlationId;
        private bool _disposed;


        public override string ConnectionString
        {
            get => Connection.ConnectionString;
            set => Connection.ConnectionString = value;
        }


        public override string Database => Connection.Database;


        public override string DataSource => Connection.DataSource;


        public override string ServerVersion => Connection.ServerVersion;


        public override ConnectionState State => Connection.State;
        
        
        protected LoggedDbConnection(ILogger Logger, Guid CorrelationId)
        {
            _logger = Logger;
            _correlationId = CorrelationId;
        }


        ~LoggedDbConnection() => Dispose(false);

        
        protected override void Dispose(bool Disposing)
        {
            if (_disposed) return;
            if (Disposing)
            {
                // No managed resources to release.
            }
            // Release unmanaged resources.
            Connection?.Dispose();
            Connection = null;
            // Do not release logger.  Its lifetime is controlled by caller.
            _disposed = true;
        }


        public override void ChangeDatabase(string DatabaseName)
        {
            _logger.Log(_correlationId, $"Changing database to {DatabaseName}.");
            Connection.ChangeDatabase(DatabaseName);
        }


        public override void Close()
        {
            _logger.Log(_correlationId, $"Closing database connection to {Connection.ConnectionString}.");
            Connection.Close();
        }


        public override void Open()
        {
            _logger.Log(_correlationId, $"Opening database connection to {Connection.ConnectionString}.");
            Connection.Open();
        }


        protected override DbTransaction BeginDbTransaction(IsolationLevel IsolationLevel)
        {
            _logger.Log(_correlationId, $"Beginning database transaction with IsolationLevel = {IsolationLevel}.");
            return Connection.BeginTransaction();
        }


        protected override DbCommand CreateDbCommand()
        {
            _logger.Log(_correlationId, "Creating database command.");
            return new LoggedDbCommand(_logger, _correlationId, Connection.CreateCommand());
        }
    }
}
