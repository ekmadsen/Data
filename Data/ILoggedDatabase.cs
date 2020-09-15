using System;
using System.Data.Common;
using System.Threading.Tasks;
using JetBrains.Annotations;


namespace ErikTheCoder.Data
{
    public interface ILoggedDatabase
    {
        [UsedImplicitly] Task<DbConnection> OpenConnectionAsync(Guid CorrelationId);
    }
}