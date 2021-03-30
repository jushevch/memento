using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Mem.Core.Moderator
{
    internal static class NewLineConverter
    {
        private const string target = "<br>";
        private static readonly Regex regex = new ("\n");

        public static IEnumerable<ProtoArea> ReplaceNewLines(this IEnumerable<ProtoArea> areas)
        {
            foreach (var area in areas)
            {
                yield return area.ReplaceNewLines();
            }
        }

        private static ProtoArea ReplaceNewLines(this ProtoArea protoArea)
        {
            protoArea.Info.Description = regex.Replace(protoArea.Info.Description, target);

            foreach (var protoRoom in protoArea.Rooms)
            {
                protoRoom.Look = regex.Replace(protoRoom.Look, target);
            }

            foreach (var protoItem in protoArea.Items)
            {
                if (!string.IsNullOrEmpty(protoItem.Description))
                {
                    protoItem.Description = regex.Replace(protoItem.Description, target);
                }
            }

            foreach (var protoMob in protoArea.Mobiles)
            {
                if (!string.IsNullOrEmpty(protoMob.Description))
                {
                    protoMob.Description = regex.Replace(protoMob.Description, target);
                }
            }

            return protoArea;
        }
    }
}
