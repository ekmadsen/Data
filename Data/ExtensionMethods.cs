using System;
using System.Collections;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using JetBrains.Annotations;


namespace ErikTheCoder.Data
{
    [UsedImplicitly]
    public static class ExtensionMethods
    {
        // These QueryAsync methods enable mapping column values to a variable captured by the lambda (Map Action) in a single traversal of the SQL result set.
        // Because calling code has no need to traverse the IEnumerable returned by the Dapper extension method, no value is returned.
        // Without this extension method, calling code would have to pass a Func to Dapper's extension method even though it never references the Func's return value.
        [UsedImplicitly]
        public static async Task QueryAsync<TFirst, TSecond>(this IDbConnection Connection, string Sql, Action<TFirst, TSecond> Map, object Param = null,
            IDbTransaction Transaction = null, bool Buffered = true, string SplitOn = "Id", int? CommandTimeout = null, CommandType? CommandType = null)
        {
            Action<TFirst, TSecond, object, object, object, object, object> map = (First, Second, Third, Fourth, Fifth, Sixth, Seventh) => Map(First, Second);
            await QueryAsync(Connection, Sql, map, Param, Transaction, Buffered, SplitOn, CommandTimeout, CommandType);
        }


        [UsedImplicitly]
        public static async Task QueryAsync<TFirst, TSecond, TThird>(this IDbConnection Connection, string Sql, Action<TFirst, TSecond, TThird> Map, object Param = null,
            IDbTransaction Transaction = null, bool Buffered = true, string SplitOn = "Id", int? CommandTimeout = null, CommandType? CommandType = null)
        {
            Action<TFirst, TSecond, TThird, object, object, object, object> map = (First, Second, Third, Fourth, Fifth, Sixth, Seventh) => Map(First, Second, Third);
            await QueryAsync(Connection, Sql, map, Param, Transaction, Buffered, SplitOn, CommandTimeout, CommandType);
        }


        [UsedImplicitly]
        public static async Task QueryAsync<TFirst, TSecond, TThird, TFourth>(this IDbConnection Connection, string Sql, Action<TFirst, TSecond, TThird, TFourth> Map, object Param = null,
            IDbTransaction Transaction = null, bool Buffered = true, string SplitOn = "Id", int? CommandTimeout = null, CommandType? CommandType = null)
        {
            Action<TFirst, TSecond, TThird, TFourth, object, object, object> map = (First, Second, Third, Fourth, Fifth, Sixth, Seventh) => Map(First, Second, Third, Fourth);
            await QueryAsync(Connection, Sql, map, Param, Transaction, Buffered, SplitOn, CommandTimeout, CommandType);
        }


        [UsedImplicitly]
        public static async Task QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth>(this IDbConnection Connection, string Sql, Action<TFirst, TSecond, TThird, TFourth, TFifth> Map, object Param = null,
            IDbTransaction Transaction = null, bool Buffered = true, string SplitOn = "Id", int? CommandTimeout = null, CommandType? CommandType = null)
        {
            Action<TFirst, TSecond, TThird, TFourth, TFifth, object, object> map = (First, Second, Third, Fourth, Fifth, Sixth, Seventh) => Map(First, Second, Third, Fourth, Fifth);
            await QueryAsync(Connection, Sql, map, Param, Transaction, Buffered, SplitOn, CommandTimeout, CommandType);
        }


        [UsedImplicitly]
        public static async Task QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth>(this IDbConnection Connection, string Sql, Action<TFirst, TSecond, TThird, TFourth, TFifth, TSixth> Map, object Param = null,
            IDbTransaction Transaction = null, bool Buffered = true, string SplitOn = "Id", int? CommandTimeout = null, CommandType? CommandType = null)
        {
            Action<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, object> map = (First, Second, Third, Fourth, Fifth, Sixth, Seventh) => Map(First, Second, Third, Fourth, Fifth, Sixth);
            await QueryAsync(Connection, Sql, map, Param, Transaction, Buffered, SplitOn, CommandTimeout, CommandType);
        }


        [UsedImplicitly]
        public static async Task QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh>(this IDbConnection Connection, string Sql, Action<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh> Map, object Param = null,
            IDbTransaction Transaction = null, bool Buffered = true, string SplitOn = "Id", int? CommandTimeout = null, CommandType? CommandType = null)
        {
            Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, bool> map = (First, Second, Third, Fourth, Fifth, Sixth, Seventh) =>
            {
                Map(First, Second, Third, Fourth, Fifth, Sixth, Seventh);
                return true;
            };
            IEnumerable results = await Connection.QueryAsync(Sql, map, Param, Transaction, Buffered, SplitOn, CommandTimeout, CommandType);
            if (!Buffered)
            {
                // Must traverse results to call map func on each row returned by query.
                // ReSharper disable once NotAccessedVariable
                bool lastResult;
                // ReSharper disable once RedundantAssignment
                foreach (bool result in results) lastResult = result; // Prevent for loop from being removed by compiler as a no-op.
            }
        }
    }
}
