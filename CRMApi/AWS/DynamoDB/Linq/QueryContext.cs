﻿using System;
using System.Linq;
using System.Linq.Expressions;
using Amazon.Lambda.Core;

namespace CRMApi.AWS.DynamoDB.Linq {
    public class QueryContext  {
        public static T Execute<T, TU>(Expression expression, bool isEnumerable) {
            if (!IsQueryOverDataSource(expression)) {
                throw new InvalidProgramException("No query over the data source was specified.");
            }

            var whereFinder = new InnermostWhereFinder();
            var whereExpression = whereFinder.GetInnermostWhere(expression);
            var lambdaExpression = (LambdaExpression)((UnaryExpression)whereExpression.Arguments[1]).Operand;

            lambdaExpression = (LambdaExpression)Evaluator.PartialEval(lambdaExpression);

            var itemFinder = new ItemFinder(lambdaExpression.Body);

            var items = ServiceHelper.GetItems<TU>(itemFinder.ScanConditions);

            var queryableItems = items.AsQueryable();

            var treeCopier = new ExpressionTreeModifier<TU>(queryableItems);
            var newExpressionTree = treeCopier.Visit(expression);

            T output = default;
            try {
                output = (T) (isEnumerable
                    ? queryableItems.Provider.CreateQuery(newExpressionTree)
                    : queryableItems.Provider.Execute(newExpressionTree));
            } catch (InvalidOperationException e) {
                LambdaLogger.Log(e.Message);
            }

            return output;
        }

        private static bool IsQueryOverDataSource(Expression expression) {
            return expression is MethodCallExpression;
        }
    }
}
