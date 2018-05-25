using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Amazon.DynamoDBv2.DataModel;
using CRMApi.Models;

namespace CRMApi.AWS.DynamoDB.Linq {
    public class PostFinder : ExpressionVisitor {

        private readonly Expression _expression;
        private List<Guid> _postIds;
        private LinkedList<ScanCondition> _scanConditions;

        public PostFinder(Expression expression) {
            _expression = expression;
        }

        public List<Guid> PostIds {
            get {
                if (_postIds != null) return _postIds;
                _postIds = new List<Guid>();
                Visit(_expression);
                return _postIds;
            }
        }

        protected override Expression VisitBinary(BinaryExpression binaryExpression) {
            if (binaryExpression.NodeType != ExpressionType.Equal ||
                !ExpressionTreeHelpers.IsMemberEqualsValueExpression(binaryExpression, typeof(Post), "PostId")) {
                return base.VisitBinary(binaryExpression);
            }

            var value = ExpressionTreeHelpers.GetValueFromEqualsExpression(binaryExpression, typeof(Post), "PostId");

            _postIds.Add(new Guid(value.ToString()));
            return binaryExpression;
        }
    }
}
