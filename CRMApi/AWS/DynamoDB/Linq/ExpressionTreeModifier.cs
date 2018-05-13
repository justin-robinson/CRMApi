using System;
using System.Linq;
using System.Linq.Expressions;

using CRMApi.Models;

namespace CRMApi.AWS.DynamoDB.Linq {
    internal class ExpressionTreeModifier : ExpressionVisitor {
        private IQueryable<Post> queryablePosts;

        internal ExpressionTreeModifier(IQueryable<Post> queryablePosts) {
            this.queryablePosts = queryablePosts;
        }

        protected override Expression VisitConstant(ConstantExpression constantExpression) {
            if(constantExpression.Type == typeof(QueryableServerData<Post>)) {
                return Expression.Constant(this.queryablePosts);
            } else {
                return constantExpression;
            }
        }
    }
}
