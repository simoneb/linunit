using System;

namespace LinUnit
{
    public static class lambda
    {
        public static Action For(Action action)
        {
            return action;
        }

        public static Action For<T>(Func<T> action)
        {
            return () => action();
        }
    }
}