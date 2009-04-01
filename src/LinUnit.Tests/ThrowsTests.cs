using System;
using NUnit.Framework;

#pragma warning disable 168
#pragma warning disable 162

namespace LinUnit.Tests
{
    public class ThrowsTests
    {
        private readonly Action action_which_throws;
        private readonly Action action_which_does_not_throw;
        private readonly Func<int> func_which_throws;
        private readonly Func<int> func_which_does_not_throw;
        private readonly Action action_throwing_invalid_operation_exception;
        private readonly Func<int> func_throwing_invalid_operation_exception;

        public ThrowsTests()
        {
            action_which_throws = () => { throw new Exception("Exception"); };
            action_which_does_not_throw = () => { var i = 1; };
            action_throwing_invalid_operation_exception = () => { throw new InvalidOperationException("Exception"); };
            func_which_throws = () => { throw new Exception("Exception"); return 1; };
            func_which_does_not_throw = () => 1;
            func_throwing_invalid_operation_exception = () => { throw new InvalidOperationException("Exception"); return 1; };
        }

        [Test]
        public void ActionThrows()
        {
            action_which_throws.ShouldThrow();
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void ActionThrowsFails()
        {
            action_which_does_not_throw.ShouldThrow();
        }

        [Test]
        public void FuncThrows()
        {
            func_which_throws.ShouldThrow();
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void FuncThrowsFails()
        {
            func_which_does_not_throw.ShouldThrow();
        }

        [Test]
        public void ActionThrowsSpecificException()
        {
            action_throwing_invalid_operation_exception.ShouldThrow<InvalidOperationException>();
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void ActionThrowsSpecificExceptionFails()
        {
            action_throwing_invalid_operation_exception.ShouldThrow<OutOfMemoryException>();
        }

        [Test]
        public void FuncThrowsSpecificException()
        {
            lambda(func_throwing_invalid_operation_exception).ShouldThrow<InvalidOperationException>();
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void FuncThrowsSpecificExceptionFails()
        {
            lambda(func_throwing_invalid_operation_exception).ShouldThrow<OutOfMemoryException>();
        }

        [Test]
        public void ActionThrowsAndAppliesConstraintToException()
        {
            action_which_throws.ShouldThrow().Message.Should(x => x == "Exception");
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void ActionThrowsAndAppliesConstraintToExceptionFails()
        {
            action_which_throws.ShouldThrow().Message.Should(x => x == "wrong message");
        }

        [Test]
        public void FuncThrowsAndAppliesConstraintToException()
        {
            func_which_throws.ShouldThrow().Message.Should(x => x == "Exception");
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void FuncThrowsAndAppliesConstraintToExceptionFails()
        {
            func_which_throws.ShouldThrow().Message.Should(x => x == "wrong message");
        }

        [Test]
        public void ActionThrowsSpecificExceptionAndAppliesConstraintToException()
        {
            action_throwing_invalid_operation_exception.ShouldThrow<InvalidOperationException>().Message.Should(x => x == "Exception");
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void ActionThrowsSpecificExceptionAndAppliesConstraintToExceptionFails()
        {
            action_throwing_invalid_operation_exception.ShouldThrow<InvalidOperationException>()
                .Message.Should(x => x == "wrong message");
        }

        [Test]
        public void FuncThrowsSpecificExceptionAndAppliesConstraintToException()
        {
            lambda(func_throwing_invalid_operation_exception).ShouldThrow<InvalidOperationException>()
                .Message.Should(x => x == "Exception");
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void FuncThrowsSpecificExceptionAndAppliesConstraintToExceptionFails()
        {
            lambda(func_throwing_invalid_operation_exception).ShouldThrow<InvalidOperationException>()
                .Message.Should(x => x == "wrong message");
        }

        [Test]
        public void ActionShouldNotThrow()
        {
            action_which_does_not_throw.ShouldNotThrow();
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void ActionShouldNotThrowFails()
        {
            action_which_throws.ShouldNotThrow();
        }

        [Test]
        public void FuncShouldNotThrow()
        {
            func_which_does_not_throw.ShouldNotThrow();
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void FuncShouldNotThrowFails()
        {
            func_which_throws.ShouldNotThrow();
        }

        private static Action lambda<T>(Func<T> func)
        {
            return () => func();
        }
    }
}
#pragma warning restore 162
#pragma warning restore 168