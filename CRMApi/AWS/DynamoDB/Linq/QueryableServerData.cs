using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
namespace CRMApi.AWS.DynamoDB.Linq {
    public class QueryableServerData<T> : IOrderedQueryable<T> {

        public IQueryProvider Provider { get; set; }
        public Expression Expression { get; set; }

        public QueryableServerData() {
            this.Provider = new QueryProvider();
            this.Expression = Expression.Constant(this);
        }

        public QueryableServerData(QueryProvider provider, Expression expression) {
            if (expression == null) {
                throw new ArgumentNullException(nameof(expression));
            }

            if (!typeof(IQueryable<T>).IsAssignableFrom(expression.Type)) {
                throw new ArgumentOutOfRangeException(nameof(expression));
            }

            this.Provider = provider ?? throw new ArgumentNullException(nameof(provider));
            this.Expression = expression;
        }

        public Type ElementType {
            get { return typeof(T); }
        }

        public IEnumerator<T> GetEnumerator() {
            return (this.Provider.Execute<IEnumerable<T>>(this.Expression)).GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() {
            return (this.Provider.Execute<System.Collections.IEnumerable>(this.Expression)).GetEnumerator();
        }


    }
}
