using System;
using NUnit.Framework;

#pragma warning disable 168
#pragma warning disable 162

namespace LinUnit.Tests
{
    [TestFixture]
    public class ThrowsTests
    {
        [Test]
        public void ActionThrows()
        {
            var e = new Exception();
            lambda.For(() => { throw e; }).ShouldThrow();
        }

        [Test]
        public void ActionThrowsFails()
        {
            lambda.For(() => lambda.For(() => { var i = 1; }).ShouldThrow()).ShouldThrow<AssertionException>();
        }

        [Test]
        public void FuncThrows()
        {
            var y = 0;
            lambda.For(() => 1 / y).ShouldThrow();
        }

        [Test]
        public void FuncThrowsFails()
        {
            lambda.For(() => lambda.For(() => 1).ShouldThrow()).ShouldThrow<AssertionException>();
        }

        [Test]
        public void ActionThrowsSpecificException()
        {
            var e = new InvalidOperationException();
            lambda.For(() => { throw e; }).ShouldThrow<InvalidOperationException>();
        }

        [Test]
        public void ActionThrowsSpecificExceptionFails()
        {
            var e = new InvalidOperationException();
            lambda.For(() => lambda.For(() => { throw e; }).ShouldThrow<OutOfMemoryException>()).ShouldThrow<AssertionException>();
        }

        [Test]
        public void FuncThrowsSpecificException()
        {
            var e = new InvalidOperationException();
            lambda.For(() => { throw e; return 1; }).ShouldThrow<InvalidOperationException>();
        }

        [Test]
        public void FuncThrowsSpecificExceptionFails()
        {
            lambda.For(() => lambda.For(() => 1).ShouldThrow<InvalidOperationException>()).ShouldThrow<AssertionException>();
        }

        [Test]
        public void ActionThrowsAndAppliesConstraintToException()
        {
            var e = new Exception("Exception");
            lambda.For(() => { throw e; }).ShouldThrow().Message.Should(x => x == "Exception");
        }

        [Test]
        public void ActionThrowsAndAppliesConstraintToExceptionFails()
        {
            var e = new Exception("Exception");
            lambda.For(() => lambda.For(() => { throw e; }).ShouldThrow().Message.Should(x => x == "wrong message")).ShouldThrow<AssertionException>();
        }

        [Test]
        public void FuncThrowsAndAppliesConstraintToException()
        {
            var e = new Exception("Exception");
            lambda.For(() => { throw e; return 1; }).ShouldThrow().Message.Should(x => x == "Exception");
        }

        [Test]
        public void FuncThrowsAndAppliesConstraintToExceptionFails()
        {
            var e = new Exception("Exception");
            lambda.For(() => lambda.For(() => { throw e; return 1; }).ShouldThrow().Message.Should(x => x == "wrong message")).ShouldThrow<AssertionException>();
        }

        [Test]
        public void ActionThrowsSpecificExceptionAndAppliesConstraintToException()
        {
            var e = new InvalidOperationException("Exception");
            lambda.For(() => { throw e; }).ShouldThrow<InvalidOperationException>().Message.Should(x => x == "Exception");
        }

        [Test]
        public void ActionThrowsSpecificExceptionAndAppliesConstraintToExceptionFails()
        {
            var e = new Exception("Exception");
            lambda.For(() => lambda.For(() => { throw e; }).ShouldThrow<Exception>().Message.Should(x => x == "wrong message")).ShouldThrow<AssertionException>();
        }

        [Test]
        public void FuncThrowsSpecificExceptionAndAppliesConstraintToException()
        {
            var e = new Exception("Exception");
            lambda.For(() => { throw e; return 1; }).ShouldThrow<Exception>().Message.Should(x => x == "Exception");
        }

        [Test]
        public void FuncThrowsSpecificExceptionAndAppliesConstraintToExceptionFails()
        {
            var e = new Exception("Exception");
            lambda.For(() => lambda.For(() => { throw e; return 1; }).ShouldThrow<Exception>().Message.Should(x => x == "wrong message")).ShouldThrow<AssertionException>();
        }

        [Test]
        public void ActionShouldNotThrow()
        {
            lambda.For(() => { var i = 1; }).ShouldNotThrow();
        }

        [Test]
        public void ActionShouldNotThrowFails()
        {
            lambda.For(() => lambda.For(() => { var i = 1; throw new Exception(); }).ShouldNotThrow()).ShouldThrow<AssertionException>();
        }

        [Test]
        public void FuncShouldNotThrow()
        {
            lambda.For(() => 1).ShouldNotThrow();
        }

        [Test]
        public void FuncShouldNotThrowFails()
        {
            var i = 0;
            lambda.For(() => lambda.For(() => 1/i).ShouldNotThrow()).ShouldThrow<AssertionException>();
        }
    }
}
#pragma warning restore 162
#pragma warning restore 168