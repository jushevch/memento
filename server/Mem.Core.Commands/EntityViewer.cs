using System.Linq;

namespace Mem.Core.Commands
{
    internal static class EntityViewer
    {
        public static void DescribeTo(this Room room, Mobile actor)
        {
            actor.User?.Output.Append($"{room.Title.PaintWith(Color.White)}<br>");
            actor.User?.Output.Append($"{room.Look}<br>");

            var exitNames = room.Exits.Keys.OrderBy(dir => dir).Select(dir => dir.GetName());
            var render = exitNames.Any() ? string.Join(' ', exitNames) : "нет";

            actor.User?.Output.Append($"Выходы: {render}.<br>".PaintWith(Color.White));

            foreach (var item in room.Items)
            {
                actor.User?.Output.Append($"{item.Look.PaintWith(Color.Yellow)}<br>");
            }

            foreach (var mob in room.Mobiles.Where(m => m != actor))
            {
                var look = string.IsNullOrEmpty(mob.Look) ? $"{mob.Name.Nom} стоит тут." : mob.Look;
                actor.User?.Output.Append($"{look.PaintWith(Color.Magenta)}<br>");
            }
        }

        public static void DescribeTo(this Item item, Mobile actor)
        {
            var desc = string.IsNullOrEmpty(item.Description)
                ? $"{item.Name.Nom} выглядит вполне обычно."
                : item.Description;

            actor.User?.Output.Append($"{desc}<br>");
        }

        public static void DescribeTo(this Mobile mob, Mobile actor)
        {
            var desc = string.IsNullOrEmpty(mob.Description)
                ? $"{mob.Name.Nom} выглядит вполне обычно."
                : mob.Description;

            actor.User?.Output.Append($"{desc}<br>");
        }

        public static void DescribeTo(this Exit exit, Mobile actor)
        {
            var desc = $"{exit.Direction.GetName().PaintWith(Color.White)} &middot; {exit.Description}<br>";
            actor.User?.Output.Append(desc);

            var room = actor.InRoom.InArea.InWorld.Rooms[exit.TargetVnum];

            foreach (var mob in room.Mobiles)
            {
                actor.User?.Output.Append("Там кто-то есть!<br>".PaintWith(Color.Magenta));
            }
        }
    }
}
