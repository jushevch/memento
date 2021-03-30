using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Mem.Core.Commands
{
    internal static class ArgumentParser
    {
        private static readonly Regex quoted = new ("^\"[^\"]+\"$", RegexOptions.Compiled);

        private static readonly Regex quotedTailed = new ("^\"[^\"]+\"\\s.+$", RegexOptions.Compiled);

        private static readonly Regex headOfQuoted = new ("^\"[^\"]+\"\\s", RegexOptions.Compiled);

        private static readonly Regex tailOfQuoted = new ("(?<=^\"[^\"]+\")\\s.+$", RegexOptions.Compiled);

        private static readonly Regex any = new ("^\\S+$", RegexOptions.Compiled);

        private static readonly Regex anyTailed = new ("^\\S+\\s.+$", RegexOptions.Compiled);

        private static readonly Regex headOfAny = new ("^\\S+\\s", RegexOptions.Compiled);

        private static readonly Regex tailOfAny = new ("(?<=^\\S+)\\s.+$", RegexOptions.Compiled);

        public static bool IsQuoted(this string value) => quoted.IsMatch(value);

        public static string[] SplitArgs(this string args) => args.Iterate().ToArray();

        public static bool Matches(this string value, string arg)
        {
            return quoted.IsMatch(arg) ? value.StrictlyMatches(arg[1..^1]) : value.LooselyMatches(arg);
        }

        public static bool StrictlyMatches(this string value, string arg)
        {
            return value.Equals(arg, StringComparison.OrdinalIgnoreCase);
        }

        public static bool LooselyMatches(this string value, string arg)
        {
            return value.Split(' ').Where(word => word.StartsWith(arg, StringComparison.OrdinalIgnoreCase)).Any();
        }

        private static IEnumerable<string> Iterate(this string args)
        {
            args = args.Trim();

            if (quoted.IsMatch(args) || any.IsMatch(args))
            {
                yield return args;
            }
            else
            {
                args.Detach(out var head, out var tail);

                yield return head;

                foreach (var arg in tail.Iterate())
                {
                    yield return arg;
                }
            }
        }

        private static void Detach(this string args, out string head, out string tail)
        {
            if (quotedTailed.IsMatch(args))
            {
                head = tailOfQuoted.Replace(args, string.Empty);
                tail = headOfQuoted.Replace(args, string.Empty);
            }
            else if (anyTailed.IsMatch(args))
            {
                head = tailOfAny.Replace(args, string.Empty);
                tail = headOfAny.Replace(args, string.Empty);
            }
            else
            {
                throw new InvalidOperationException($"Unable to parse arguments: {args}.");
            }
        }
    }
}
