using NUnit.Framework;
using System.Linq;

namespace LinUnit.Tests
{
    [TestFixture]
    public class ExpressionTests
    {
        [Test]
        public void Equal()
        {
            1.Should(x => x == 1);
        }

        [Test, ExpectedException(ExpectedMessage = "2\r\n  But was:  1", MatchType = MessageMatch.Contains)]
        public void EqualFails()
        {
            1.Should(x => x == 2);
        }

        [Test]
        public void NotEqual()
        {
            1.Should(x => x != 2);
        }

        [Test, ExpectedException(ExpectedMessage = "not 1\r\n  But was:  1", MatchType = MessageMatch.Contains)]
        public void NotEqualFails()
        {
            1.Should(x => x != 1);
        }

        [Test]
        public void And()
        {
            1.Should(x => x == 1 && x != 2);
        }


        [Test, ExpectedException(ExpectedMessage = "1 and 2\r\n  But was:  1", MatchType = MessageMatch.Contains)]
        public void AndFails()
        {
            1.Should(x => x == 1 && x == 2);
        }

        [Test]
        public void Or()
        {
            1.Should(x => x == 1 || x == 2);
        }

        [Test, ExpectedException(ExpectedMessage = "2 or 3\r\n  But was:  1", MatchType = MessageMatch.Contains)]
        public void OrFails()
        {
            1.Should(x => x == 2 || x == 3);
        }

        [Test]
        public void Greater()
        {
            1.Should(x => x > 0);
        }

        [Test, ExpectedException(ExpectedMessage = "greater than 2\r\n  But was:  1", MatchType = MessageMatch.Contains)]
        public void GreaterFails()
        {
            1.Should(x => x > 2);
        }

        [Test]
        public void GreaterOrEqual()
        {
            1.Should(x => x >= 1);
        }

        [Test, ExpectedException(ExpectedMessage = "greater than or equal to 2\r\n  But was:  1", MatchType = MessageMatch.Contains)]
        public void GreaterOrEqualFails()
        {
            1.Should(x => x >= 2);
        }

        [Test]
        public void Less()
        {
            1.Should(x => x < 2);
        }

        [Test, ExpectedException(ExpectedMessage = "less than 1\r\n  But was:  1", MatchType = MessageMatch.Contains)]
        public void LessFails()
        {
            1.Should(x => x < 1);
        }

        [Test]
        public void LessOrEqual()
        {
            1.Should(x => x <= 1);
        }

        [Test, ExpectedException(ExpectedMessage = "less than or equal to 0\r\n  But was:  1", MatchType = MessageMatch.Contains)]
        public void LessOrEqualFails()
        {
            1.Should(x => x <= 0);
        }

        [Test]
        public void OperationsOnRightSide()
        {
            var y = 2;
            var z = 4;

            4.Should(x => x == y*y);
            2.Should(x => x == z/y);
            4.Should(x => x == y+y);
            2.Should(x => x == z-y);
        }

        [Test]
        public void StaticMethodWithoutArgumentsOnParameter()
        {
            "a".Should(x => x.Any());
        }

        [Test, ExpectedException(ExpectedMessage = "\"x.Any()\"\r\n  But was:  <string.Empty>", MatchType = MessageMatch.Contains)]
        public void StaticMethodWithoutArgumentsOnParameterFailing()
        {
            "".Should(x => x.Any());
        }

        [Test]
        public void StaticMethodOnParameter()
        {
            new[] {1, 2}.Should(x => x.Contains(1));
        }

        [Test, ExpectedException(ExpectedMessage = "\"x.Contains(3)\"\r\n  But was:  < 1, 2 >", MatchType = MessageMatch.Contains)]
        public void StaticMethodOnParameterFailing()
        {
            new[] {1, 2}.Should(x => x.Contains(3));
        }

        [Test]
        public void InstanceMethodOnParameter()
        {
            "abc".Should(x => x.Contains("a"));
        }

        [Test, ExpectedException(ExpectedMessage = "\"x.Contains(\"d\")\"\r\n  But was:  \"abc\"", MatchType = MessageMatch.Contains)]
        public void InstanceMethodOnParameterFailing()
        {
            "abc".Should(x => x.Contains("d"));
        }

        [Test]
        public void Not()
        {
            "abc".Should(x => !x.Contains("d"));            
        }

        [Test, ExpectedException(ExpectedMessage = "not \"x.Contains(\"a\")\"\r\n  But was:  \"abc\"", MatchType = MessageMatch.Contains)]
        public void NotFailing()
        {
            "abc".Should(x => !x.Contains("a"));
        }

        [Test]
        public void ShouldReturnsBoolean()
        {
            new[] {1, 2, 3}.All(x => x.Should(y => y > 0));
        }

        [Test]
        public void StaticMethodOnLiteral()
        {
            1.Should(x => x == int.Parse("1"));
        }

        [Test, ExpectedException(ExpectedMessage = "2\r\n  But was:  1", MatchType = MessageMatch.Contains)]
        public void StaticMethodOnLiteralFailing()
        {
            1.Should(x => x == int.Parse("2"));
        }

        [Test]
        public void InstanceMethodOnLiteral()
        {
            "1".Should(x => x == 1.ToString());
        }

        [Test, ExpectedException(ExpectedMessage = "\"2\"\r\n  But was:  \"1\"", MatchType = MessageMatch.Contains)]
        public void InstanceMethodOnLiteralFailing()
        {
            "1".Should(x => x == 2.ToString());
        }

        [Test]
        public void ExpressionParameterOnRightSide()
        {
            1.Should(x => x == x);
        }

        [Test, ExpectedException(ExpectedMessage = "not 1\r\n  But was:  1", MatchType = MessageMatch.Contains)]
        public void ExpressionParameterOnRightSideFailing()
        {
            1.Should(x => x != x);
        }

        [Test]
        public void ExpressionParameterMethodCallOnRightSide()
        {
            1.Should(x => x == int.Parse(x.ToString()));
        }

        [Test, ExpectedException(ExpectedMessage = "11\r\n  But was:  1", MatchType = MessageMatch.Contains)]
        public void ExpressionParameterMethodCallOnRightSideFailing()
        {
            1.Should(x => x == int.Parse(x.ToString() + 1));
        }


        [Test, ExpectedException(ExpectedMessage = "Expression 1 invalid on left side of binary expression")]
        public void InvalidLeftSideOperand1()
        {
            1.Should(x => 1 == x);
        }

        [Test, ExpectedException(ExpectedMessage = "Expression x.ToString() invalid on left side of binary expression")]
        public void InvalidLeftSideOperand2()
        {
            1.Should(x => x.ToString() == "1");
        }

        [Test, ExpectedException(ExpectedMessage = "y invalid on left side of binary expression", MatchType = MessageMatch.Contains)]
        public void InvalidLeftSideOperand3()
        {
            var y = 1;
            1.Should(x => y == x);
        }

        [Test, ExpectedException(ExpectedMessage = "Expression x.Length invalid on left side of binary expression")]
        public void InvalidLeftSideOperand4()
        {
            "".Should(x => x.Length == 0);
        }

        [Test, ExpectedException(ExpectedMessage = "invalid on left side of binary expression", MatchType = MessageMatch.Contains)]
        public void InvalidLeftSideOperand5()
        {
            "".Should(x => x[0] == 0);
        }

        [Test]
        public void CaptureExternalVariable()
        {
            var y = 1;
            1.Should(x => x == y);
        }

        [Test, ExpectedException(ExpectedMessage = "2\r\n  But was:  1", MatchType = MessageMatch.Contains)]
        public void CaptureExternalVariableFails()
        {
            var y = 2;
            1.Should(x => x == y);
        }

        [Test]
        public void CaptureExternalVariableArray()
        {
            var y = new[]{1, 2};
            var z = new[]{2, 1};

            1.Should(x => x == y[0] && x == z[1]);
        }

        [Test, ExpectedException(ExpectedMessage = "1 and 2\r\n  But was:  1", MatchType = MessageMatch.Contains)]
        public void CaptureExternalVariableArrayFails()
        {
            var y = new[]{1, 2};
            var z = new[]{2, 1};

            1.Should(x => x == y[0] && x == z[0]);
        }

        [Test]
        public void MethodCallWithCapturedVariableArgument()
        {
            var y = 2;
            new[] {1, 2}.Should(x => x.Contains(y));
        }

        [Test, ExpectedException(ExpectedMessage = "y)\"\r\n  But was:  < 1, 2 >", MatchType = MessageMatch.Contains)]
        public void MethodCallWithCapturedVariableArgumentFails()
        {
            var y = 3;
            new[]{1, 2}.Should(x => x.Contains(y));
        }
    }
}