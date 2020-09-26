using System;
using System.Data;
using System.Threading.Tasks;
using JetBrains.Annotations;


namespace ErikTheCoder.Data
{
    public interface ILoggedDatabase
    {
        [UsedImplicitly] Task<IDbConnection> OpenConnectionAsync(Guid CorrelationId);
    }
}