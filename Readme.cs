using System.Collections.Generic;
using System.Linq;

namespace LinUnit.Tests
{
    public class Examples
    {
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

            var ints = new[] { 1, 2, 3 };

            ints.Should(x => x.SequenceEqual(new[] { 1, 2, 3 }));
            ints.Should(x => !x.SequenceEqual(new[] { 3, 2, 1 }));
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