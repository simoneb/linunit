using System;
using System.Linq.Expressions;

namespace LinUnit
{
    internal class ExpressionNotHandledException<TExpression> : Exception
    {
        public ExpressionNotHandledException(ExpressionType nodeType) : 
            base(string.Format("{0} of type \"{1}\" not handled", typeof (TExpression).Name, nodeType))
        {}
    }
}