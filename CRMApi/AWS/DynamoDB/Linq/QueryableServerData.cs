using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
namespace CRMApi.AWS.DynamoDB.Linq {
    public class QueryableServerData<T> : IOrderedQueryable<T> {

        public IQueryProvider Provider { get; set; }
        public Expression Expression { get; set; }

        public QueryableServerData() {
            Provider = new QueryProvider();
            Expression = Expression.Constant(this);
        }

        public QueryableServerData(QueryProvider provider, Expression expression) {
            if (expression == null) {
                throw new ArgumentNullException(nameof(expression));
            }

            if (!typeof(IQueryable<T>).IsAssignableFrom(expression.Type)) {
                throw new ArgumentOutOfRangeException(nameof(expression));
            }

            Provider = provider ?? throw new ArgumentNullException(nameof(provider));
            Expression = expression;
        }

        public Type ElementType => typeof(T);

        public IEnumerator<T> GetEnumerator() {
            return Provider.Execute<IEnumerable<T>>(Expression).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return Provider.Execute<IEnumerable>(Expression).GetEnumerator();
        }

    }
}
