using System.Collections.Generic;
using System.Linq.Expressions;
using Amazon.DynamoDBv2.DocumentModel;

namespace CRMApi.AWS.DynamoDB.Linq {
    public static class ExpressionTypeHelper {
        public static readonly Dictionary<ExpressionType, ScanOperator> ExpressionTypeToScanOperator =
            new Dictionary<ExpressionType, ScanOperator> {
                {ExpressionType.Equal, ScanOperator.Equal},
                {ExpressionType.NotEqual, ScanOperator.NotEqual},
                {ExpressionType.LessThanOrEqual, ScanOperator.LessThanOrEqual},
                {ExpressionType.LessThan, ScanOperator.LessThan},
                {ExpressionType.GreaterThanOrEqual, ScanOperator.GreaterThanOrEqual},
                {ExpressionType.GreaterThan, ScanOperator.GreaterThan},
            };
    }
}