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
        public void Action_throws()
        {
            action_which_throws.ShouldThrow();
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void Action_throws_fails()
        {
            action_which_does_not_throw.ShouldThrow();
        }

        [Test]
        public void Func_throws()
        {
            func_which_throws.ShouldThrow();
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void Func_throws_fails()
        {
            func_which_does_not_throw.ShouldThrow();
        }

        [Test]
        public void Action_throws_specific_exception()
        {
            action_throwing_invalid_operation_exception.ShouldThrow<InvalidOperationException>();
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void Action_throws_specific_exception_fails()
        {
            action_throwing_invalid_operation_exception.ShouldThrow<OutOfMemoryException>();
        }

        [Test]
        public void Func_throws_specific_exception()
        {
            lambda(func_throwing_invalid_operation_exception).ShouldThrow<InvalidOperationException>();
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void Func_throws_specific_exception_fails()
        {
            lambda(func_throwing_invalid_operation_exception).ShouldThrow<OutOfMemoryException>();
        }

        [Test]
        public void Action_throws_and_applies_constraint_to_exception()
        {
            action_which_throws.ShouldThrow().Message.Should(x => x == "Exception");
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void Action_throws_and_applies_constraint_to_exception_fails()
        {
            action_which_throws.ShouldThrow().Message.Should(x => x == "wrong message");
        }

        [Test]
        public void Func_throws_and_applies_constraint_to_exception()
        {
            func_which_throws.ShouldThrow().Message.Should(x => x == "Exception");
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void Func_throws_and_applies_constraint_to_exception_fails()
        {
            func_which_throws.ShouldThrow().Message.Should(x => x == "wrong message");
        }

        [Test]
        public void Action_throws_specific_exception_and_applies_constraint_to_exception()
        {
            action_throwing_invalid_operation_exception.ShouldThrow<InvalidOperationException>().Message.Should(x => x == "Exception");
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void Action_throws_specific_exception_and_applies_constraint_to_exception_fails()
        {
            action_throwing_invalid_operation_exception.ShouldThrow<InvalidOperationException>()
                .Message.Should(x => x == "wrong message");
        }

        [Test]
        public void Func_throws_specific_exception_and_applies_constraint_to_exception()
        {
            lambda(func_throwing_invalid_operation_exception).ShouldThrow<InvalidOperationException>()
                .Message.Should(x => x == "Exception");
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void Func_throws_specific_exception_and_applies_constraint_to_exception_fails()
        {
            lambda(func_throwing_invalid_operation_exception).ShouldThrow<InvalidOperationException>()
                .Message.Should(x => x == "wrong message");
        }

        [Test]
        public void Action_should_not_throw()
        {
            action_which_does_not_throw.ShouldNotThrow();
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void Action_should_not_throw_fails()
        {
            action_which_throws.ShouldNotThrow();
        }

        [Test]
        public void Func_should_not_throw()
        {
            func_which_does_not_throw.ShouldNotThrow();
        }

        [Test, ExpectedException(typeof(AssertionException))]
        public void Func_should_not_throw_fails()
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