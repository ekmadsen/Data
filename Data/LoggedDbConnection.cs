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
        private ILogger _logger;
        private Func<Guid> _getCorrelationId;
        private bool _disposed;


        public override string ConnectionString
        {
            get => Connection.ConnectionString;
            set => Connection.ConnectionString = value;
        }


        public override string Database => Connection.Database;


        public override ConnectionState State => Connection.State;


        public override string DataSource => Connection.DataSource;


        public override string ServerVersion => Connection.ServerVersion;


        protected LoggedDbConnection(ILogger Logger, Func<Guid> GetCorrelationId)
        {
            _logger = Logger;
            _getCorrelationId = GetCorrelationId;
        }


        ~LoggedDbConnection() => Dispose(false);

        
        protected override void Dispose(bool Disposing)
        {
            if (_disposed) return;
            if (Disposing)
            {
                // Release managed resources.
                _getCorrelationId = null;
            }
            // Release unmanaged resources.
            _logger?.Dispose();
            _logger = null;
            Connection?.Dispose();
            Connection = null;
            _disposed = true;
        }


        public override void Open()
        {
            _logger.Log(_getCorrelationId(), $"Opening database connection to {Connection.ConnectionString}.");
            Connection.Open();
        }


        public override void Close()
        {
            _logger.Log(_getCorrelationId(), $"Closing database connection to {Connection.ConnectionString}.");
            Connection.Close();
        }


        public override void ChangeDatabase(string DatabaseName)
        {
            _logger.Log(_getCorrelationId(), $"Changing database to {DatabaseName}.");
            Connection.ChangeDatabase(DatabaseName);
        }


        protected override DbTransaction BeginDbTransaction(IsolationLevel IsolationLevel)
        {
            _logger.Log(_getCorrelationId(), $"Beginning database transaction with IsolationLevel = {IsolationLevel}.");
            return Connection.BeginTransaction();
        }


        protected override DbCommand CreateDbCommand()
        {
            _logger.Log(_getCorrelationId(), "Creating database command.");
            return new LoggedDbCommand(_logger, _getCorrelationId, Connection.CreateCommand());
        }
    }
}
