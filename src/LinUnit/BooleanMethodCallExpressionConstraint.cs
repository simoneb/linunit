using System;
using System.Linq.Expressions;
using NUnit.Framework.Constraints;

namespace LinUnit
{
    internal class BooleanMethodCallExpressionConstraint<TActual> : Constraint
    {
        private readonly MethodCallExpression expression;
        private readonly ParameterExpression parameter;

        public BooleanMethodCallExpressionConstraint(MethodCallExpression expression, ParameterExpression parameter)
        {
            this.expression = expression;
            this.parameter = parameter;
        }

        public override bool Matches(object act)
        {
            actual = act;
            return Expression.Lambda<Func<TActual, bool>>(expression, parameter).Compile().Invoke((TActual) actual);
        }

        public override void WriteDescriptionTo(MessageWriter writer)
        {
            writer.WriteExpectedValue(expression.ToString());
        }
    }
}