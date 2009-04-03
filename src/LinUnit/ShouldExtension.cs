using System;
using System.Linq.Expressions;
using NUnit.Framework;

namespace LinUnit
{
    public static class ShouldExtension
    {
        public delegate object mydel();

        public static void Should<T>(this T actual, Expression<Func<T, bool>> assertion)
        {
            new ExpressionVisitor<T>(actual).Visit(assertion);
        }

        public static Exception ShouldThrow<T>(this Func<T> func)
        {
            return new Action(() => func()).ShouldThrow();    
        }

        public static Exception ShouldThrow(this Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                return e;
            }

            throw new AssertionException("No exception was thrown");
        }

        public static T ShouldThrow<T>(this Action action) where T : Exception
        {
            try
            {
                action();
            }
            catch (T e)
            {
                return e;
            }
            catch(Exception e)
            {
                throw new AssertionException(string.Format("Expected exception of type {0} but exception of type {1} was thrown", typeof(T), e.GetType()));
            }

            throw new AssertionException(string.Format("Expected exception of type {0} but no exception was thrown", typeof(T)));
        }

        public static void ShouldNotThrow<T>(this Func<T> func)
        {
            new Action(() => func()).ShouldNotThrow();    
        }

        public static void ShouldNotThrow(this Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                throw new AssertionException(string.Format("Expected no exception to be thrown but exception of type {0} was thrown", e.GetType()));
            }
        }
    }
}