using System;
using System.Collections.Generic;
using System.Linq;

namespace Mem.Core.Moderator
{
    internal static class WorldResetter
    {
        private static readonly Dictionary<ResetCommand, Action<int, int>> actions = new ()
        {
            [ResetCommand.RepopItem] = RepopItem,
            [ResetCommand.RepopMobile] = RepopMobile,
        };

        private static World world;

        public static void Reset(this World world, IEnumerable<Reset> resetList)
        {
            WorldResetter.world = world ?? throw new ArgumentNullException(nameof(world));

            if (resetList is null)
            {
                throw new ArgumentNullException(nameof(resetList));
            }

            foreach (var reset in resetList)
            {
                if (actions.TryGetValue(reset.Command, out var action))
                {
                    action.Invoke(reset.Subject, reset.Object);
                }
            }
        }

        private static void RepopItem(int roomVnum, int itemVnum)
        {
            var room = world.Rooms[roomVnum];
            var prototype = world.ProtoItems[itemVnum];
            var query = room.Items.Where(i => i.Vnum == itemVnum);

            if (!query.Any())
            {
                room.Add(new Item(prototype));
            }
        }

        private static void RepopMobile(int roomVnum, int mobVnum)
        {
            var room = world.Rooms[roomVnum];
            var prototype = world.ProtoMobiles[mobVnum];

            if (world.MobileCount[mobVnum] < prototype.MaxPerWorld)
            {
                room.Add(new Mobile(prototype));
            }
        }
    }
}
