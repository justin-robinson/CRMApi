using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using CRMApi.Models;

namespace CRMApi.AWS.DynamoDB.Linq {
    public static class QueryContext  {
        internal static object Execute(Expression expression, bool isEnumerable) {
            if (!IsQueryOverDataSource(expression)) {
                throw new InvalidProgramException("No query over the data source was specified.");
            }

            var whereFinder = new InnermostWhereFinder();
            var whereExpression = whereFinder.GetInnermostWhere(expression);
            var lambdaExpression = (LambdaExpression)((UnaryExpression)whereExpression.Arguments[1]).Operand;

            lambdaExpression = (LambdaExpression)Evaluator.PartialEval(lambdaExpression);

            var itemFinder = new ItemFinder(lambdaExpression.Body);

            var posts = ServiceHelper.GetPosts(itemFinder.ScanConditions);

            var queryablePosts = posts.AsQueryable();

            var treeCopier = new ExpressionTreeModifier(queryablePosts);
            var newExpressionTree = treeCopier.Visit(expression);

            return isEnumerable
                ? queryablePosts.Provider.CreateQuery(newExpressionTree)
                : queryablePosts.Provider.Execute(newExpressionTree);
        }

        private static bool IsQueryOverDataSource(Expression expression) {
            return expression is MethodCallExpression;
        }
    }
}
