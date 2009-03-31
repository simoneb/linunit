using System;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework.Constraints;

namespace LinUnit
{
    public class ExpressionVisitor<T>
    {
        private readonly T actualValue;
        private ParameterExpression actualParameter;
        private readonly ConstraintBuilder builder = new ConstraintBuilder();

        public ExpressionVisitor(T actualValue)
        {
            this.actualValue = actualValue;
        }

        public bool Visit(Expression<Func<T, bool>> expression)
        {
            actualParameter = expression.Parameters.Single();
            Visit(expression.Body);
            NUnit.Framework.Assert.That(actualValue, builder.Resolve());
            return true;
        }

        private void Visit(Expression expression)
        {
            switch(expression.NodeType)
            {
                case ExpressionType.Equal:
                case ExpressionType.NotEqual:
                case ExpressionType.GreaterThan:
                case ExpressionType.LessThan:
                case ExpressionType.GreaterThanOrEqual:
                case ExpressionType.LessThanOrEqual:
                case ExpressionType.OrElse:
                case ExpressionType.AndAlso:
                    Visit(expression as BinaryExpression);
                    break;
                case ExpressionType.Call:
                    Visit(expression as MethodCallExpression);
                    break;
                case ExpressionType.Not:
                    Visit(expression as UnaryExpression);
                    break;
                default: throw new Exception(string.Format("Expression of type \"{0}\" not handled", expression.NodeType));
            }
        }

        private void Visit(UnaryExpression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.Not:
                    builder.Append(new NotOperator());
                    break;
                default: throw new Exception(string.Format("UnaryExpression of type \"{0}\" not handled", expression.NodeType));
            }

            Visit(expression.Operand);
        }

        private void Visit(MethodCallExpression expression)
        {
            builder.Append(new BooleanMethodCallExpressionConstraint<T>(expression, actualParameter));
        }

        private void Visit(BinaryExpression expression)
        {
            switch (expression.NodeType)
            {
                case ExpressionType.Equal:
                    VisitComparisonExpression<EqualConstraint>(expression);
                    break;
                case ExpressionType.NotEqual:
                    VisitNotExpression();
                    VisitComparisonExpression<EqualConstraint>(expression);
                    break;
                case ExpressionType.GreaterThan:
                    VisitComparisonExpression<GreaterThanConstraint>(expression);
                    break;
                case ExpressionType.LessThan:
                    VisitComparisonExpression<LessThanConstraint>(expression);
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    VisitComparisonExpression<GreaterThanOrEqualConstraint>(expression);
                    break;
                case ExpressionType.LessThanOrEqual:
                    VisitComparisonExpression<LessThanOrEqualConstraint>(expression);
                    break;
                case ExpressionType.OrElse:
                    Visit(expression.Left);
                    VisitOrExpression();
                    Visit(expression.Right);
                    break;
                case ExpressionType.AndAlso:
                    Visit(expression.Left);
                    VisitAndExpression();
                    Visit(expression.Right);
                    break;
                default: throw new Exception(string.Format("BinaryExpression of type \"{0}\" not handled", expression.NodeType));
            }
        }

        private void VisitComparisonExpression<TConstraint>(BinaryExpression expression) where TConstraint : Constraint
        {
            if (expression.Left != actualParameter)
                throw new InvalidOperationException(string.Format("Expression {0} invalid on left side of binary expression", expression.Left));

            builder.Append(Activator.CreateInstance(typeof(TConstraint), EvaluateExpression(expression.Right)) as TConstraint);
        }

        private object EvaluateExpression(Expression expression)
        {
            switch (expression.NodeType)
            {   
                case ExpressionType.Constant:
                    return ((ConstantExpression) expression).Value;
                case ExpressionType.Call:
                case ExpressionType.MemberAccess:
                case ExpressionType.ArrayIndex:
                case ExpressionType.Multiply:
                case ExpressionType.Divide:
                case ExpressionType.Add:
                case ExpressionType.Subtract:
                    return Expression.Lambda<Func<T, object>>(Expression.Convert(expression, typeof(object)), 
                                                              actualParameter).Compile().Invoke(actualValue);
                case ExpressionType.Parameter:
                    return actualValue;
                default: throw new Exception(string.Format("Expression of type \"{0}\" not handled", expression.NodeType));
            }
        }

        private void VisitAndExpression()
        {
            builder.Append(new AndOperator());
        }

        private void VisitOrExpression()
        {
            builder.Append(new OrOperator());
        }

        private void VisitNotExpression()
        {
            builder.Append(new NotOperator());
        }
    }

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