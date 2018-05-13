using System;
using System.Linq;
using System.Linq.Expressions;

namespace CRMApi.AWS.DynamoDB.Linq {
    public class QueryProvider : IQueryProvider {
        public IQueryable CreateQuery(Expression expression) {
            Type elementType = TypeSystem.GetElementType(expression.Type);
            try {
                return (IQueryable)Activator.CreateInstance(typeof(QueryableServerData<>).MakeGenericType(elementType), new object[] { this, expression });
            } catch (System.Reflection.TargetInvocationException e) {
                throw e.InnerException;
            }
        }

        public IQueryable<T> CreateQuery<T>(Expression expression) {
            return new QueryableServerData<T>(this, expression);
        }

        public object Execute(Expression expression) {
            return QueryContext.Execute(expression, false);
        }

        public T Execute<T>(Expression expression) {
            bool isEnumerable = typeof(T).Name == "IEnumerable`1";

            return (T)QueryContext.Execute(expression, isEnumerable);
        }
    }
}
