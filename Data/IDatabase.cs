using System.Data.Common;
using System.Threading.Tasks;
using JetBrains.Annotations;


namespace ErikTheCoder.Data
{
    public interface IDatabase
    {
        [UsedImplicitly]
        Task<DbConnection> OpenConnectionAsync(bool LogCommands);
    }
}