using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

using CRMApi.Models;

namespace CRMApi.AWS.DynamoDB.Linq {
    public class PostFinder : ExpressionVisitor {

        private Expression expression;
        private List<string> postIds;

        public PostFinder(Expression expression) {
            this.expression = expression;
        }

        public List<string> PostIds {
            get {
                if (this.postIds == null) {
                    this.postIds = new List<string>();
                    Visit(this.expression);
                }
                return this.postIds;
            }
        }

        protected override Expression VisitBinary(BinaryExpression binaryExpression) {
            if (binaryExpression.NodeType == ExpressionType.Equal) {
                if(ExpressionTreeHelpers.IsMemberEqualsValueExpression(binaryExpression, typeof(Post), "PostId")) {
                    postIds.Add(ExpressionTreeHelpers.GetValueFromEqualsExpression(binaryExpression, typeof(Post), "PostId"));
                    return binaryExpression;
                } else {
                    return base.VisitBinary(binaryExpression);
                }
            } else {
                return base.VisitBinary(binaryExpression);
            }
        }
    }
}
