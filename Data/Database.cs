using System;
using System.Data.Common;
using System.Threading.Tasks;
using ErikTheCoder.Logging;


namespace ErikTheCoder.Data
{
    public abstract class Database : IDatabase, IDisposable
    {
        protected ILogger Logger;
        protected Func<Guid> GetCorrelationId;
        protected string Connection;
        private bool _disposed;


        protected Database(ILogger Logger, Func<Guid> GetCorrelationId, string Connection)
        {
            this.Logger = Logger;
            this.GetCorrelationId = GetCorrelationId;
            this.Connection = Connection;
        }


        ~Database() => Dispose(false);


        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        private void Dispose(bool Disposing)
        {
            if (_disposed) return;
            if (Disposing)
            {
                // Release managed resources.
                GetCorrelationId = null;
                Connection = null;
            }
            // Release unmanaged resources.
            Logger?.Dispose();
            Logger = null;
            _disposed = true;
        }


        public abstract Task<DbConnection> OpenConnectionAsync(bool LogCommands);
    }
}