using System;
using System.Linq.Expressions;
namespace CRMApi.AWS.DynamoDB.Linq {
    public class InnermostWhereFinder : ExpressionVisitor {
        private MethodCallExpression innermostWhereExpression;

        public MethodCallExpression GetInnermostWhere(Expression expression) {
            this.Visit(expression);
            return innermostWhereExpression;
        }

        protected override Expression VisitMethodCall(MethodCallExpression expression) {
            if (expression.Method.Name == "Where") {
                this.innermostWhereExpression = expression;
            }

            this.Visit(expression.Arguments[0]);

            return expression;
        }
    }
}
