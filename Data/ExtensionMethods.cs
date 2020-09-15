using System;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using JetBrains.Annotations;


namespace ErikTheCoder.Data
{
    [UsedImplicitly]
    public static class ExtensionMethods
    {
        // These QueryAsync methods enable mapping column values to variables captured by the lambda (Map Action) in a single traversal of the SQL result set.
        // Because calling code has no need to traverse the IEnumerable returned by the Dapper extension method, no value is returned.
        // Without this extension method, calling code would have to pass a Func to Dapper's extension method even though it never references the Func's return value.
        [UsedImplicitly]
        public static async Task QueryAsync<TFirst, TSecond>(this IDbConnection Connection, string Sql, Action<TFirst, TSecond> Map, object Param = null,
            IDbTransaction Transaction = null, bool Buffered = true, string SplitOn = "Id", int? CommandTimeout = null, CommandType? CommandType = null)
        {
            if (!Buffered) throw new Exception($"{nameof(QueryAsync)} extension method called with Map Action must be buffered.");
            Func<TFirst, TSecond, bool> map = (First, Second) =>
            {
                Map(First, Second);
                return true;
            };
            await Connection.QueryAsync(Sql, map, Param, Transaction, true, SplitOn, CommandTimeout, CommandType);
        }


        [UsedImplicitly]
        public static async Task QueryAsync<TFirst, TSecond, TThird>(this IDbConnection Connection, string Sql, Action<TFirst, TSecond, TThird> Map, object Param = null,
            IDbTransaction Transaction = null, bool Buffered = true, string SplitOn = "Id", int? CommandTimeout = null, CommandType? CommandType = null)
        {
            if (!Buffered) throw new Exception($"{nameof(QueryAsync)} extension method called with Map Action must be buffered.");
            Func<TFirst, TSecond, TThird, bool> map = (First, Second, Third) =>
            {
                Map(First, Second, Third);
                return true;
            };
            await Connection.QueryAsync(Sql, map, Param, Transaction, true, SplitOn, CommandTimeout, CommandType);
        }


        [UsedImplicitly]
        public static async Task QueryAsync<TFirst, TSecond, TThird, TFourth>(this IDbConnection Connection, string Sql, Action<TFirst, TSecond, TThird, TFourth> Map, object Param = null,
            IDbTransaction Transaction = null, bool Buffered = true, string SplitOn = "Id", int? CommandTimeout = null, CommandType? CommandType = null)
        {
            if (!Buffered) throw new Exception($"{nameof(QueryAsync)} extension method called with Map Action must be buffered.");
            Func<TFirst, TSecond, TThird, TFourth, bool> map = (First, Second, Third, Fourth) =>
            {
                Map(First, Second, Third, Fourth);
                return true;
            };
            await Connection.QueryAsync(Sql, map, Param, Transaction, true, SplitOn, CommandTimeout, CommandType);
        }


        [UsedImplicitly]
        public static async Task QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth>(this IDbConnection Connection, string Sql, Action<TFirst, TSecond, TThird, TFourth, TFifth> Map, object Param = null,
            IDbTransaction Transaction = null, bool Buffered = true, string SplitOn = "Id", int? CommandTimeout = null, CommandType? CommandType = null)
        {
            if (!Buffered) throw new Exception($"{nameof(QueryAsync)} extension method called with Map Action must be buffered.");
            Func<TFirst, TSecond, TThird, TFourth, TFifth, bool> map = (First, Second, Third, Fourth, Fifth) =>
            {
                Map(First, Second, Third, Fourth, Fifth);
                return true;
            };
            await Connection.QueryAsync(Sql, map, Param, Transaction, true, SplitOn, CommandTimeout, CommandType);
        }


        [UsedImplicitly]
        public static async Task QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth>(this IDbConnection Connection, string Sql, Action<TFirst, TSecond, TThird, TFourth, TFifth, TSixth> Map, object Param = null,
            IDbTransaction Transaction = null, bool Buffered = true, string SplitOn = "Id", int? CommandTimeout = null, CommandType? CommandType = null)
        {
            if (!Buffered) throw new Exception($"{nameof(QueryAsync)} extension method called with Map Action must be buffered.");
            Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, bool> map = (First, Second, Third, Fourth, Fifth, Sixth) =>
            {
                Map(First, Second, Third, Fourth, Fifth, Sixth);
                return true;
            };
            await Connection.QueryAsync(Sql, map, Param, Transaction, true, SplitOn, CommandTimeout, CommandType);
        }


        [UsedImplicitly]
        public static async Task QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh>(this IDbConnection Connection, string Sql, Action<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh> Map, object Param = null,
            IDbTransaction Transaction = null, bool Buffered = true, string SplitOn = "Id", int? CommandTimeout = null, CommandType? CommandType = null)
        {
            if (!Buffered) throw new Exception($"{nameof(QueryAsync)} extension method called with Map Action must be buffered.");
            Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, bool> map = (First, Second, Third, Fourth, Fifth, Sixth, Seventh) =>
            {
                Map(First, Second, Third, Fourth, Fifth, Sixth, Seventh);
                return true;
            };
            await Connection.QueryAsync(Sql, map, Param, Transaction, true, SplitOn, CommandTimeout, CommandType);
        }


        [UsedImplicitly]
        public static bool TryCommit(this IDbTransaction Transaction)
        {
            try
            {
                Transaction.Commit();
                return true;
            }
            catch
            {
                // Ignore exception.
                return false;
            }
        }


        [UsedImplicitly]
        public static bool TryRollback(this IDbTransaction Transaction)
        {
            try
            {
                Transaction.Rollback();
                return true;
            }
            catch
            {
                // Ignore exception.
                return false;
            }
        }
    }
}
