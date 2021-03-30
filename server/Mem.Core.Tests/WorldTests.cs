using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Mem.Core.Tests
{
    public class WorldTests
    {
        private readonly ProtoArea protoAreaA = new ProtoArea
        {
            Info = new ProtoAreaInfo(),
            Rooms = new List<ProtoRoom>
            {
                new ProtoRoom
                {
                    Vnum = 1,
                    Exits = new List<ProtoExit>()
                },
                new ProtoRoom
                {
                    Vnum = 2,
                    Exits = new List<ProtoExit>()
                },
                new ProtoRoom
                {
                    Vnum = 3,
                    Exits = new List<ProtoExit>()
                }
            },
            Items = new List<ProtoItem>
            {
                new ProtoItem { Vnum = 1 },
                new ProtoItem { Vnum = 2 }
            },
            Mobiles = new List<ProtoMobile>
            {
                new ProtoMobile { Vnum = 1 },
                new ProtoMobile { Vnum = 2 }
            },
            Resets = new List<string>
            {
                $"{nameof(ResetCommand.RepopItem)} 1 2",
                $"{nameof(ResetCommand.RepopMobile)} 2 3"
            }
        };

        private readonly ProtoArea protoAreaB = new ProtoArea
        {
            Info = new ProtoAreaInfo(),
            Rooms = new List<ProtoRoom>
            {
                new ProtoRoom
                {
                    Vnum = 4,
                    Exits = new List<ProtoExit>()
                },
                new ProtoRoom
                {
                    Vnum = 5,
                    Exits = new List<ProtoExit>()
                }
            },
            Items = new List<ProtoItem>
            {
                new ProtoItem { Vnum = 4 }
            },
            Mobiles = new List<ProtoMobile>
            {
                new ProtoMobile { Vnum = 4 }
            },
            Resets = new List<string>
            {
                $"{nameof(ResetCommand.RepopItem)} 4 5"
            }
        };

        [Fact]
        public void InitializesCorrectly()
        {
            var protoAreas = new ProtoArea[] { this.protoAreaA, this.protoAreaB };
            var world = new World(protoAreas);

            var protoRooms = protoAreas.SelectMany(proto => proto.Rooms).OrderBy(r => r.Vnum).ToList();
            var protoItems = protoAreas.SelectMany(proto => proto.Items).OrderBy(i => i.Vnum).ToList();
            var protoMobs = protoAreas.SelectMany(proto => proto.Mobiles).OrderBy(m => m.Vnum).ToList();

            var worldRooms = world.Rooms.Values.OrderBy(r => r.Vnum).ToList();
            var worldProtoItems = world.ProtoItems.Values.OrderBy(i => i.Vnum).ToList();
            var worldProtoMobs = world.ProtoMobiles.Values.OrderBy(m => m.Vnum).ToList();

            var protoResetsCount = protoAreas.SelectMany(proto => proto.Resets).Count();
            var worldResetsCount = world.Areas.SelectMany(area => area.Resets).Count();

            Assert.Equal(protoAreas.Length, world.Areas.Count);
            Assert.Equal(protoRooms.Count, worldRooms.Count);
            Assert.Equal(protoItems.Count, worldProtoItems.Count);
            Assert.Equal(protoMobs.Count, worldProtoMobs.Count);
            Assert.Equal(protoResetsCount, worldResetsCount);

            for (int i = 0; i < protoRooms.Count; i++)
            {
                Assert.Equal(protoRooms[i].Vnum, worldRooms[i].Vnum);
            }

            for (int i = 0; i < protoItems.Count; i++)
            {
                Assert.Equal(protoItems[i], worldProtoItems[i]);
            }

            for (int i = 0; i < protoMobs.Count; i++)
            {
                Assert.Equal(protoMobs[i], worldProtoMobs[i]);
            }

            foreach (var vnum in world.ProtoMobiles.Keys)
            {
                Assert.Equal(0, world.MobileCount[vnum]);
            }
        }

        [Fact]
        public void ConstructorFailsOnNullOrEmptyPrototypes()
        {
            Assert.Throws<ArgumentNullException>(() => new World(null));
            Assert.Throws<ArgumentException>(() => new World(Array.Empty<ProtoArea>()));
        }
    }
}
