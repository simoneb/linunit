using NUnit.Framework;
using System.Linq;

namespace LinUnit.Tests
{
    public class ExpressionTests
    {
        [Test]
        public void Equal()
        {
            1.Should(x => x == 1);
        }

        [Test, ExpectedException(ExpectedMessage = "2\r\n  But was:  1", MatchType = MessageMatch.Contains)]
        public void Equal_fails()
        {
            1.Should(x => x == 2);
        }

        [Test]
        public void Not_equal()
        {
            1.Should(x => x != 2);
        }

        [Test, ExpectedException(ExpectedMessage = "not 1\r\n  But was:  1", MatchType = MessageMatch.Contains)]
        public void Not_equal_fails()
        {
            1.Should(x => x != 1);
        }

        [Test]
        public void And()
        {
            1.Should(x => x == 1 && x != 2);
        }


        [Test, ExpectedException(ExpectedMessage = "1 and 2\r\n  But was:  1", MatchType = MessageMatch.Contains)]
        public void And_fails()
        {
            1.Should(x => x == 1 && x == 2);
        }

        [Test]
        public void Or()
        {
            1.Should(x => x == 1 || x == 2);
        }

        [Test, ExpectedException(ExpectedMessage = "2 or 3\r\n  But was:  1", MatchType = MessageMatch.Contains)]
        public void Or_fails()
        {
            1.Should(x => x == 2 || x == 3);
        }

        [Test]
        public void Greater()
        {
            1.Should(x => x > 0);
        }

        [Test, ExpectedException(ExpectedMessage = "greater than 2\r\n  But was:  1", MatchType = MessageMatch.Contains)]
        public void Greater_fails()
        {
            1.Should(x => x > 2);
        }

        [Test]
        public void Greater_or_equal()
        {
            1.Should(x => x >= 1);
        }

        [Test, ExpectedException(ExpectedMessage = "greater than or equal to 2\r\n  But was:  1", MatchType = MessageMatch.Contains)]
        public void Greater_or_equal_fails()
        {
            1.Should(x => x >= 2);
        }

        [Test]
        public void Less()
        {
            1.Should(x => x < 2);
        }

        [Test, ExpectedException(ExpectedMessage = "less than 1\r\n  But was:  1", MatchType = MessageMatch.Contains)]
        public void Less_fails()
        {
            1.Should(x => x < 1);
        }

        [Test]
        public void Less_or_equal()
        {
            1.Should(x => x <= 1);
        }

        [Test, ExpectedException(ExpectedMessage = "less than or equal to 0\r\n  But was:  1", MatchType = MessageMatch.Contains)]
        public void Less_or_equal_fails()
        {
            1.Should(x => x <= 0);
        }

        [Test]
        public void Not()
        {
            "abc".Should(x => !x.Contains("d"));
        }

        [Test, ExpectedException(ExpectedMessage = "not \"x.Contains(\"a\")\"\r\n  But was:  \"abc\"", MatchType = MessageMatch.Contains)]
        public void Notfails()
        {
            "abc".Should(x => !x.Contains("a"));
        }

        [Test]
        public void Operations_on_right_side()
        {
            var y = 2;
            var z = 4;

            4.Should(x => x == y*y);
            2.Should(x => x == z/y);
            4.Should(x => x == y+y);
            2.Should(x => x == z-y);
        }

        [Test]
        public void Static_method_called_on_parameter_with_no_arguments()
        {
            "a".Should(x => x.Any());
        }

        [Test, ExpectedException(ExpectedMessage = "\"x.Any()\"\r\n  But was:  <string.Empty>", MatchType = MessageMatch.Contains)]
        public void Static_method_called_on_parameter_with_no_arguments_fails()
        {
            "".Should(x => x.Any());
        }

        [Test]
        public void Static_method_called_on_parameter_with_simple_arguments()
        {
            new[] {1, 2}.Should(x => x.Contains(1));
        }

        [Test, ExpectedException(ExpectedMessage = "\"x.Contains(3)\"\r\n  But was:  < 1, 2 >", MatchType = MessageMatch.Contains)]
        public void Static_method_called_on_parameter_with_simple_arguments_fails()
        {
            new[] {1, 2}.Should(x => x.Contains(3));
        }

        [Test]
        public void Static_method_called_on_parameter_with_lambda_argument()
        {
            new[] {1, 2, 3}.Should(x => x.All(y => y > 0));
        }

        [Test, ExpectedException(ExpectedMessage = "\"x.All(y => (y > 1))\"\r\n  But was:  < 1, 2, 3 >", MatchType = MessageMatch.Contains)]
        public void Static_method_called_on_parameter_with_lambda_argument_fails()
        {
            new[] {1, 2, 3}.Should(x => x.All(y => y > 1));
        }

        [Test]
        public void Instance_method_called_on_parameter_with_simple_arguments()
        {
            "abc".Should(x => x.Contains("a"));
        }

        [Test, ExpectedException(ExpectedMessage = "\"x.Contains(\"d\")\"\r\n  But was:  \"abc\"", MatchType = MessageMatch.Contains)]
        public void Instance_method_called_on_parameter_with_simple_arguments_fails()
        {
            "abc".Should(x => x.Contains("d"));
        }

        [Test]
        public void Static_method_call_on_right_side()
        {
            1.Should(x => x == int.Parse("1"));
        }

        [Test, ExpectedException(ExpectedMessage = "2\r\n  But was:  1", MatchType = MessageMatch.Contains)]
        public void Static_method_call_on_right_side_fails()
        {
            1.Should(x => x == int.Parse("2"));
        }

        [Test]
        public void Instance_method_call_on_right_side()
        {
            "1".Should(x => x == 1.ToString());
        }

        [Test, ExpectedException(ExpectedMessage = "\"2\"\r\n  But was:  \"1\"", MatchType = MessageMatch.Contains)]
        public void Instance_method_call_on_right_side_fails()
        {
            "1".Should(x => x == 2.ToString());
        }

        [Test]
        public void Parameter_on_right_side()
        {
            1.Should(x => x == x);
        }

        [Test, ExpectedException(ExpectedMessage = "not 1\r\n  But was:  1", MatchType = MessageMatch.Contains)]
        public void Parameter_on_right_side_fails()
        {
            1.Should(x => x != x);
        }

        [Test]
        public void Method_call_on_parameter_on_right_side()
        {
            1.Should(x => x == int.Parse(x.ToString()));
        }

        [Test, ExpectedException(ExpectedMessage = "11\r\n  But was:  1", MatchType = MessageMatch.Contains)]
        public void Method_call_on_parameter_on_right_side_fails()
        {
            1.Should(x => x == int.Parse(x.ToString() + 1));
        }


        [Test, ExpectedException(ExpectedMessage = "Expression 1 invalid on left side of binary expression")]
        public void Invalid_left_side_operand_1()
        {
            1.Should(x => 1 == x);
        }

        [Test, ExpectedException(ExpectedMessage = "Expression x.ToString() invalid on left side of binary expression")]
        public void Invalid_left_side_operand_2()
        {
            1.Should(x => x.ToString() == "1");
        }

        [Test, ExpectedException(ExpectedMessage = "y invalid on left side of binary expression", MatchType = MessageMatch.Contains)]
        public void Invalid_left_side_operand_3()
        {
            var y = 1;
            1.Should(x => y == x);
        }

        [Test, ExpectedException(ExpectedMessage = "Expression x.Length invalid on left side of binary expression")]
        public void Invalid_left_side_operand_4()
        {
            "".Should(x => x.Length == 0);
        }

        [Test, ExpectedException(ExpectedMessage = "invalid on left side of binary expression", MatchType = MessageMatch.Contains)]
        public void Invalid_left_side_operand_5()
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