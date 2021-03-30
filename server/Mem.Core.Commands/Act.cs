using System.Linq;

namespace Mem.Core.Commands
{
    internal static class Act
    {
        public static void ActLeaveTo(this Mobile actor, Direction toDir)
        {
            var direction =
                toDir == Direction.Up ? "наверх" :
                toDir == Direction.Down ? "вниз" :
                toDir == Direction.Somewhere ? "куда-то" :
                $"на {toDir.GetName()}";

            actor.Perform($"{actor.Name.Nom} уходит {direction}.<br>");
        }

        public static void ActEnterFrom(this Mobile actor, Direction fromDir)
        {
            var direction =
                fromDir == Direction.Up ? "сверху" :
                fromDir == Direction.Down ? "снизу" :
                fromDir == Direction.Somewhere ? "откуда-то" :
                $"с {fromDir.GetName()}а";

            actor.Perform($"{actor.Name.Nom} приш{actor.Affix("ел|ла|ло")} {direction}.<br>");
        }

        public static void ActTake(this Mobile actor, Item item)
        {
            actor.Report($"Вы взяли {item.Name.Acc}.<br>");
            actor.Perform($"{actor.Name.Nom} взя{actor.Affix("л|ла|ло")} {item.Name.Acc}.<br>");
        }

        public static void ActDrop(this Mobile actor, Item item)
        {
            actor.Report($"Вы бросили {item.Name.Acc}.<br>");
            actor.Perform($"{actor.Name.Nom} броси{actor.Affix("л|ла|ло")} {item.Name.Acc}.<br>");
        }

        private static void Perform(this Mobile actor, string action)
        {
            foreach (var mob in actor.InRoom.Mobiles.Where(m => m != actor))
            {
                mob.User?.Output.Append($"<br>{action}");
            }
        }

        private static void Report(this Mobile actor, string action)
        {
            actor.User?.Output.Append($"{action}");
        }
    }
}
