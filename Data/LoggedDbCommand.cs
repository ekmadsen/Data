using System;
using System.Data;
using System.Data.Common;
using System.Text;
using ErikTheCoder.Logging;


namespace ErikTheCoder.Data
{
    // Cannot inherit from SqlCommand because it's a sealed class.
    public sealed class LoggedDbCommand : DbCommand
    {
        private ILogger _logger;
        private Func<Guid> _getCorrelationId;
        private DbCommand _command;
        private bool _disposed;


        public override string CommandText
        {
            get => _command.CommandText;
            set => _command.CommandText = value;
        }


        public override int CommandTimeout
        {
            get => _command.CommandTimeout;
            set => _command.CommandTimeout = value;
        }


        public override CommandType CommandType
        {
            get => _command.CommandType;
            set => _command.CommandType = value;
        }


        public override UpdateRowSource UpdatedRowSource
        {
            get => _command.UpdatedRowSource;
            set => _command.UpdatedRowSource = value;
        }


        protected override DbConnection DbConnection
        {
            get => _command.Connection;
            set => _command.Connection = value;
        }


        protected override DbParameterCollection DbParameterCollection => _command.Parameters;


        protected override DbTransaction DbTransaction
        {
            get => _command.Transaction;
            set => _command.Transaction = value;
        }


        public override bool DesignTimeVisible
        {
            get => _command.DesignTimeVisible;
            set => _command.DesignTimeVisible = value;
        }
        

        public LoggedDbCommand(ILogger Logger, Func<Guid> GetCorrelationId, DbCommand Command)
        {
            _logger = Logger;
            _getCorrelationId = GetCorrelationId;
            _command = Command;
        }


        ~LoggedDbCommand() => Dispose(false);


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
            _command?.Dispose();
            _command = null;
            _disposed = true;
        }


        public override void Cancel()
        {
            _logger.Log(_getCorrelationId(), "Cancelling database command.");
            _command.Cancel();
        }


        public override int ExecuteNonQuery()
        {
            LogCommandBeforeExecuted();
            int result = _command.ExecuteNonQuery();
            LogCommandAfterExecuted();
            return result;
        }


        public override object ExecuteScalar()
        {
            LogCommandBeforeExecuted();
            return _command.ExecuteScalar();
        }


        public override void Prepare()
        {
            _logger.Log(_getCorrelationId(), "Preparing database command.");
            _command.Prepare();
        }
        

        protected override DbParameter CreateDbParameter() => _command.CreateParameter();


        protected override DbDataReader ExecuteDbDataReader(CommandBehavior Behavior)
        {
            LogCommandBeforeExecuted();
            return _command.ExecuteReader(Behavior);
        }


        private void LogCommandBeforeExecuted()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Database command type = {_command.CommandType}");
            stringBuilder.AppendLine($"Database command text = {_command.CommandText}.");
            foreach (IDataParameter parameter in _command.Parameters)
            {
                if ((parameter.Direction == ParameterDirection.Output) || (parameter.Direction == ParameterDirection.ReturnValue)) continue;
                stringBuilder.AppendLine($"Database command parameter {parameter.ParameterName} = {parameter.Value}.");
            }
            _logger.Log(_getCorrelationId(), stringBuilder.ToString());
        }


        private void LogCommandAfterExecuted()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (IDataParameter parameter in _command.Parameters)
            {
                if ((parameter.Direction == ParameterDirection.Input) || (parameter.Direction == ParameterDirection.InputOutput)) continue;
                stringBuilder.AppendLine($"Database command parameter {parameter.ParameterName} = {parameter.Value}.");
            }
            _logger.Log(_getCorrelationId(), stringBuilder.ToString());
        }
    }
}