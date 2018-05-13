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

            InnermostWhereFinder whereFinder = new InnermostWhereFinder();
            MethodCallExpression whereExpression = whereFinder.GetInnermostWhere(expression);
            LambdaExpression lambdaExpression = (LambdaExpression)((UnaryExpression)(whereExpression.Arguments[1])).Operand;

            lambdaExpression = (LambdaExpression)Evaluator.PartialEval(lambdaExpression);

            PostFinder postFinder = new PostFinder(lambdaExpression.Body);
            List<string> postIds = postFinder.PostIds;

            Post[] posts;
            if (postIds.Count == 0) {
                posts = ServiceHelper.GetPosts();
            } else {
                posts = ServiceHelper.GetPostsByPostId(postIds);
            }

            IQueryable<Post> queryablePosts = posts.AsQueryable<Post>();

            ExpressionTreeModifier treeCopier = new ExpressionTreeModifier(queryablePosts);
            Expression newExpressionTree = treeCopier.Visit(expression);

            if (isEnumerable) {
                return queryablePosts.Provider.CreateQuery(newExpressionTree);
            }

            return queryablePosts.Provider.Execute(newExpressionTree);
        }

        private static bool IsQueryOverDataSource(Expression expression) {
            return expression is MethodCallExpression;
        }
    }
}
