using System;
using System.Linq.Expressions;

namespace CRMApi.AWS.DynamoDB.Linq {
    internal class ExpressionTreeHelper {

        internal static bool TryGetExpressionFieldAndValue(BinaryExpression be, out string fieldName, out object value) {
            if (be.Left.NodeType == ExpressionType.MemberAccess) {
                var me = (MemberExpression) be.Left;
                fieldName = me.Member.Name;
                value = GetValueFromExpression(be.Right);
                return true;
            }
            if (be.Right.NodeType == ExpressionType.MemberAccess) {
                var me = (MemberExpression) be.Right;
                fieldName = me.Member.Name;
                value = GetValueFromExpression(be.Left);
                return true;
            }

            fieldName = default;
            value = default;
            return false;
        }

        internal static object GetValueFromExpression(Expression expression) {
            if (expression.NodeType == ExpressionType.Constant) {
                return ((ConstantExpression)expression).Value;
            }
            throw new InvalidQueryException(
                    String.Format("The expression type {0} is not supported to obtain a value.", expression.NodeType));
        }
    }
}