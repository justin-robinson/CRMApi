using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CRMApi.AWS.DynamoDB.Linq {
    public static class Evaluator {

        public static Expression PartialEval(Expression expression, Func<Expression, bool> fnCanBeEvaluated) {
            return new SubtreeEvaluator(new Nominator(fnCanBeEvaluated).Nominate(expression)).Eval(expression);
        }

        public static Expression PartialEval(Expression expression) {
            return PartialEval(expression, Evaluator.CanBeEvaluatedLocally);
        }

        public static bool CanBeEvaluatedLocally(Expression expression) {
            return expression.NodeType != ExpressionType.Parameter;
        }
    }

    class SubtreeEvaluator : ExpressionVisitor {
        HashSet<Expression> candidates;

        internal SubtreeEvaluator(HashSet<Expression> candidates) {
            this.candidates = candidates;
        }

        internal Expression Eval(Expression expression) {
            return this.Visit(expression);
        }

        public override Expression Visit(Expression expression) {
            if (expression == null) {
                return null;
            }

            if (this.candidates.Contains(expression)) {
                return this.Evaluate(expression);
            }

            return base.Visit(expression);
        }

        private Expression Evaluate(Expression expression) {
            if (expression.NodeType == ExpressionType.Constant) {
                return expression;
            }
            LambdaExpression lambda = Expression.Lambda(expression);
            Delegate fn = lambda.Compile();
            return Expression.Constant(fn.DynamicInvoke(null), expression.Type);
        }
    }

    class Nominator : ExpressionVisitor {
        Func<Expression, bool> fnCanBeEvaluated;
        HashSet<Expression> candidates;
        bool cannotBeEvaluated;

        internal Nominator(Func<Expression, bool> fnCanBeEvaluated) {
            this.fnCanBeEvaluated = fnCanBeEvaluated;
        }

        internal HashSet<Expression> Nominate(Expression expression) {
            this.candidates = new HashSet<Expression>();
            this.Visit(expression);
            return this.candidates;
        }

        public override Expression Visit(Expression expression) {
            if (expression != null) {
                bool saveCannotBeEvaluated = this.cannotBeEvaluated;
                this.cannotBeEvaluated = false;
                base.Visit(expression);
                if (!this.cannotBeEvaluated) {
                    if (this.fnCanBeEvaluated(expression)) {
                        this.candidates.Add(expression);
                    } else {
                        this.cannotBeEvaluated = true;
                    }
                }
                this.cannotBeEvaluated |= saveCannotBeEvaluated;
            }
            return expression;
        }
    }
}
