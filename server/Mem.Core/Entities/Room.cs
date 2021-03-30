using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Mem.Core
{
    public class Room
    {
        private readonly HashSet<Item> itemsInside = new ();
        private readonly HashSet<Mobile> mobsInside = new ();
        private readonly Action<int> incrMobCount;
        private readonly Action<int> decrMobCount;

        public Room(ProtoRoom proto,
                    Area inArea,
                    Action<int> incrMobCount,
                    Action<int> decrMobCount)
        {
            if (proto is null)
            {
                throw new ArgumentNullException(nameof(proto));
            }

            this.Vnum = proto.Vnum;
            this.Title = proto.Title;
            this.Look = proto.Look;
            this.Exits =
                new ReadOnlyDictionary<Direction, Exit>(
                    proto.Exits
                        .Select(e => new Exit(e))
                        .ToDictionary(e => e.Direction));

            this.InArea = inArea ?? throw new ArgumentNullException(nameof(inArea));
            this.incrMobCount = incrMobCount ?? throw new ArgumentNullException(nameof(incrMobCount));
            this.decrMobCount = decrMobCount ?? throw new ArgumentNullException(nameof(decrMobCount));
        }

        public int Vnum { get; }

        public string Title { get; }

        public string Look { get; }

        public ReadOnlyDictionary<Direction, Exit> Exits { get; }

        public Area InArea { get; }

        public IEnumerable<Item> Items => this.itemsInside;

        public IEnumerable<Mobile> Mobiles => this.mobsInside;

        public void Add(Item item)
        {
            if (item is null || !this.itemsInside.Add(item))
            {
                throw new InvalidOperationException("Failed to add an item into a room.");
            }

            item.InRoom = this;
        }

        public void Remove(Item item)
        {
            if (item is null || !this.itemsInside.Remove(item))
            {
                throw new InvalidOperationException("Failed to remove an item from a room.");
            }

            item.InRoom = null;
        }

        public void Add(Mobile mob)
        {
            if (mob is null || !this.mobsInside.Add(mob))
            {
                throw new InvalidOperationException("Failed to add a mobile into a room.");
            }

            mob.InRoom = this;

            if (mob.IsNpc)
            {
                this.incrMobCount.Invoke(mob.Vnum);
            }
        }

        public void Remove(Mobile mob)
        {
            if (mob is null || !this.mobsInside.Remove(mob))
            {
                throw new InvalidOperationException("Failed to remove a mobile from a room.");
            }

            mob.InRoom = null;

            if (mob.IsNpc)
            {
                this.decrMobCount.Invoke(mob.Vnum);
            }
        }
    }
}
