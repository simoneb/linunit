using System;
using System.Linq.Expressions;

namespace LinUnit
{
    public static class ShouldExtension
    {
        public static bool Should<T>(this T actual, Expression<Func<T, bool>> assertion)
        {
            return new ExpressionVisitor<T>(actual).Visit(assertion);
        }
    }
}