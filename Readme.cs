using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace LinUnit.Tests
{
    /* Here's an example of what you can do with LinUnit
     * You can write expressions in the C# language and let LinUnit evaluate them
     * The difference with the classic constraint-based syntax is that extending the range of assertions you can
     * perform no longer requires to modify the source code of the NUnit framework, but it's a matter of creating
     * an extension method and call it. You can see it in action with Empty() method.
    */
    public class Examples
    {
        [Test]
        public void Example()
        {
            true.Should(x => x == true);
            false.Should(x => x == false);

            const string somethig = "something";

            somethig.Should(x => x.Contains("some"));
            somethig.Should(x => !x.Contains("also"));
            somethig.ToUpperInvariant().Should(x => !x.Contains("some"));
            somethig.Should(x => x.StartsWith("so") && x.EndsWith("ing") && x.Contains("meth"));
            somethig.Should(x => !x.StartsWith("ing") && !x.EndsWith("so") && !x.Contains("body"));

            var ints = new[] {1, 2, 3};

            ints.Should(x => x.SequenceEqual(new[] {1, 2, 3}));
            ints.Should(x => !x.SequenceEqual(new[] {3, 2, 1}));
            ints.Should(x => x != null);
            ints.Should(x => !x.Empty());
            ints.Should(x => x.Contains(2) && !x.Contains(4));

            "".Should(x => x.Empty());
        }
    }

    public static class Enumerable
    {
        public static bool Empty<T>(this IEnumerable<T> value)
        {
            return !value.Any();
        }
    }
}