using System;
using System.Collections.Generic;
using System.Linq;

namespace Mem.Core.Commands
{
    internal static class EntityPicker
    {
        public static bool TryPickItem(this IEnumerable<Item> collection, string args, out Item item)
        {
            item = collection
                .Where(i => args.IsQuoted()
                    ? i.Name.Nom.StrictlyMatches(args[1..^1])
                    : i.Keywords.LooselyMatches(args))
                .FirstOrDefault();

            return item != null;
        }

        public static bool TryPickMobile(this IEnumerable<Mobile> collection, string args, out Mobile mob)
        {
            var arr = args.SplitArgs();

            var count = arr.Length > 1 && int.TryParse(arr[0], out var x) ? (x > 0 ? x : -1) : -1;
            var keyword = count > 0 ? arr[1] : arr[0];

            var query = collection
                .Where(m => keyword.IsQuoted()
                    ? m.Name.Nom.StrictlyMatches(keyword[1..^1])
                    : m.Keywords.LooselyMatches(keyword));

            if (count > 0)
            {
                mob = null;
                var i = 1;

                foreach (var m in query)
                {
                    if (count == i++)
                    {
                        mob = m;
                        break;
                    }
                }
            }
            else
            {
                mob = query.FirstOrDefault();
            }

            return mob != null;
        }

        public static bool TryPickExit(this IEnumerable<Exit> collection, string args, out Exit exit)
        {
            exit = collection.Where(e => e.Direction.GetName().Matches(args) ||
                (e.Direction == Direction.NorthEast && args.Equals("св", StringComparison.OrdinalIgnoreCase)) ||
                (e.Direction == Direction.SouthEast && args.Equals("юв", StringComparison.OrdinalIgnoreCase)) ||
                (e.Direction == Direction.SouthWest && args.Equals("юз", StringComparison.OrdinalIgnoreCase)) ||
                (e.Direction == Direction.NorthWest && args.Equals("сз", StringComparison.OrdinalIgnoreCase)))
                .FirstOrDefault();

            return exit != null;
        }
    }
}
