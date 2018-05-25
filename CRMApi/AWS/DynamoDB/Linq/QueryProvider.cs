using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace CRMApi.AWS.DynamoDB.Linq {
    public class QueryProvider : IQueryProvider {
        public IQueryable CreateQuery(Expression expression) {
            Type elementType = TypeSystem.GetElementType(expression.Type);
            try {
                return (IQueryable)Activator.CreateInstance(typeof(QueryableServerData<>).MakeGenericType(elementType), this, expression);
            } catch (TargetInvocationException e) {
                throw e.InnerException;
            }
        }

        public IQueryable<T> CreateQuery<T>(Expression expression) {
            return new QueryableServerData<T>(this, expression);
        }

        public object Execute(Expression expression) {
            return QueryContext.Execute<object, object>(expression, false);
        }

        public T Execute<T>(Expression expression) {
            var returnType = typeof(T);
            var isEnumerable = returnType.Name == "IEnumerable`1";
            var itemType = isEnumerable
                ? returnType.GenericTypeArguments[0]
                : returnType;

            return (T) typeof(QueryContext)
                .GetMethod(nameof(QueryContext.Execute), BindingFlags.Public | BindingFlags.Static)
                .MakeGenericMethod(returnType, itemType)
                .Invoke(null, new object[]{expression, isEnumerable});
        }
    }
}
