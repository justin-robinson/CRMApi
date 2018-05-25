using System;
using System.Linq;
using System.Linq.Expressions;

using CRMApi.Models;

namespace CRMApi.AWS.DynamoDB.Linq {
    internal class ExpressionTreeModifier<T> : ExpressionVisitor {
        private readonly IQueryable<T> _queryableItems;

        internal ExpressionTreeModifier(IQueryable<T> queryableItems) {
            _queryableItems = queryableItems;
        }

        protected override Expression VisitConstant(ConstantExpression constantExpression) {
            if(constantExpression.Type == typeof(QueryableServerData<T>)) {
                return Expression.Constant(_queryableItems);
            }
            return constantExpression;
        }
    }
}
