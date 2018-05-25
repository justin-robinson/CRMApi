using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Amazon.DynamoDBv2.DataModel;
using Expression = System.Linq.Expressions.Expression;

namespace CRMApi.AWS.DynamoDB.Linq {
    public class ItemFinder : ExpressionVisitor {

        private readonly Expression _expression;
        private ICollection<ScanCondition> _scanConditions;

        public ItemFinder(Expression expression) {
            _expression = expression;
        }

        public ICollection<ScanCondition> ScanConditions {
            get {
                if (_scanConditions != null) return _scanConditions;
                _scanConditions = new LinkedList<ScanCondition>();
                Visit(_expression);
                return _scanConditions;
            }
        }

        protected override Expression VisitBinary(BinaryExpression binaryExpression) {
            if (!ExpressionTypeHelper.ExpressionTypeToScanOperator.TryGetValue(binaryExpression.NodeType, out var scanOperator)) {
                throw new Exception($"Unsupported {nameof(ExpressionType)}");
            }

            if (!ExpressionTreeHelpers.TryGetExpressionFieldAndValue(binaryExpression, out var fieldName, out var value)) {
                throw new Exception("Failed to get expression field name and value");
            }
            ScanConditions.Add(new ScanCondition(fieldName, scanOperator, value));
            return binaryExpression;
        }
    }
}
