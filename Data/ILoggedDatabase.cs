using System;
using System.Data.Common;
using System.Threading.Tasks;
using JetBrains.Annotations;


namespace ErikTheCoder.Data
{
    public interface ILoggedDatabase
    {
        [UsedImplicitly] DbConnection OpenConnection(Guid CorrelationId);
        [UsedImplicitly] Task<DbConnection> OpenConnectionAsync(Guid CorrelationId);
    }
}