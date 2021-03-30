using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Mem.Core
{
    public class World
    {
        private readonly Dictionary<int, int> mobCount = new ();

        public World(IEnumerable<ProtoArea> protoAreas)
        {
            if (protoAreas is null)
            {
                throw new ArgumentNullException(nameof(protoAreas));
            }

            if (!protoAreas.Any())
            {
                throw new ArgumentException("No areas to build the world.", nameof(protoAreas));
            }

            var areas = new List<Area>();
            var rooms = new Dictionary<int, Room>();
            var protoItems = new Dictionary<int, ProtoItem>();
            var protoMobs = new Dictionary<int, ProtoMobile>();

            foreach (var protoArea in protoAreas)
            {
                var area = new Area(
                    protoArea.Info,
                    this,
                    protoArea.Rooms.Select(r => r.Vnum).ToList(),
                    protoArea.Resets.Select(i => new Reset(i)).ToList());

                foreach (var room in protoArea.Rooms
                    .Select(r => new Room(r, area, this.IncreaseMobCount, this.DecreaseMobCount)))
                {
                    rooms.Add(room.Vnum, room);
                }

                foreach (var protoItem in protoArea.Items)
                {
                    protoItems.Add(protoItem.Vnum, protoItem);
                }

                foreach (var protoMob in protoArea.Mobiles)
                {
                    protoMobs.Add(protoMob.Vnum, protoMob);
                    this.mobCount[protoMob.Vnum] = 0;
                }

                areas.Add(area);
            }

            this.Areas = new ReadOnlyCollection<Area>(areas);
            this.Rooms = new ReadOnlyDictionary<int, Room>(rooms);
            this.ProtoItems = new ReadOnlyDictionary<int, ProtoItem>(protoItems);
            this.ProtoMobiles = new ReadOnlyDictionary<int, ProtoMobile>(protoMobs);
            this.MobileCount = new ReadOnlyDictionary<int, int>(this.mobCount);
        }

        public ReadOnlyCollection<Area> Areas { get; }

        public ReadOnlyDictionary<int, Room> Rooms { get; }

        public ReadOnlyDictionary<int, ProtoItem> ProtoItems { get; }

        public ReadOnlyDictionary<int, ProtoMobile> ProtoMobiles { get; }

        public ReadOnlyDictionary<int, int> MobileCount { get; }

        protected void IncreaseMobCount(int vnum)
        {
            checked
            {
                this.mobCount[vnum]++;
            }
        }

        protected void DecreaseMobCount(int vnum)
        {
            this.mobCount[vnum]--;

            if (this.mobCount[vnum] < 0)
            {
                throw new InvalidOperationException("Negative mobile count.");
            }
        }
    }
}
