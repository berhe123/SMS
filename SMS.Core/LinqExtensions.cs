using System.Linq;
using System.Linq.Expressions;

namespace SMS.Core
{
    public static class LinqExtensions
    {
        /// <summary>Orders the sequence by specific column and direction.</summary>
        /// <param name="query">The query.</param>
        /// <param name="sortColumn">The sort column.</param>
        /// <param name="isAsc">true for ascending </param>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string sortColumn, bool isAsc)
        {
            string methodName = string.Format("OrderBy{0}", isAsc ? "" : "descending");
            // p
            ParameterExpression parameter = Expression.Parameter(query.ElementType, "p");
            // p.sortColumn
            MemberExpression memberAccess = Expression.Property(parameter, sortColumn);
            // p => p.sortColumn
            LambdaExpression orderByLambda = Expression.Lambda(memberAccess, parameter);
            // query.methodName(p => p.sortColumn);
            MethodCallExpression result = Expression.Call(
                      typeof(Queryable),
                      methodName,
                      new[] { query.ElementType, memberAccess.Type },
                      query.Expression,
                      Expression.Quote(orderByLambda));

            return query.Provider.CreateQuery<T>(result);
        }

        /// <summary>Groups the sequence by specific column</summary>
        /// <param name="query">The query.</param>
        /// <param name="groupColumn">The group column.</param>
        public static IQueryable<IGrouping<long, T>> GroupBy<T>(this IQueryable<T> query, string groupColumn)
        {
            // p
            ParameterExpression parameter = Expression.Parameter(query.ElementType, "p");
            // p.groupColumn
            MemberExpression memberAccess = Expression.Property(parameter, groupColumn);
            // p => p.groupColumn
            LambdaExpression orderByLambda = Expression.Lambda(memberAccess, parameter);
            // query.methodName(p => p.groupColumn);
            MethodCallExpression result = Expression.Call(
                      typeof(Queryable),
                      "GroupBy",
                      new[] { query.ElementType, memberAccess.Type },
                      query.Expression,
                      Expression.Quote(orderByLambda));

            return query.Provider.CreateQuery<IGrouping<long, T>>(result);
        }
    }
}
