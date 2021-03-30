using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Mem.Core.Tests
{
    public class AreaTests
    {
        private readonly ProtoArea protoArea = new ProtoArea
        {
            Info = new ProtoAreaInfo
            {
                Author = "Author",
                Title = "Title",
                Description = "Description"
            },
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
            Items = new List<ProtoItem>(),
            Mobiles = new List<ProtoMobile>(),
            Resets = new List<string>
            {
                $"{nameof(ResetCommand.RepopItem)} 25300 25300",
                $"{nameof(ResetCommand.RepopMobile)} 25300 25300"
            }
        };

        [Fact]
        public void ConstructorInitializesCorrectly()
        {
            var world = new World(new ProtoArea[] { this.protoArea });
            var area = world.Areas.First();

            var expectedRoomVnums = this.protoArea.Rooms.Select(r => r.Vnum).OrderBy(v => v).ToList();
            var actualRoomVnums = area.RoomVnums.OrderBy(v => v).ToList();

            Assert.Equal(this.protoArea.Info.Author, area.Author);
            Assert.Equal(this.protoArea.Info.Title, area.Title);
            Assert.Equal(this.protoArea.Info.Description, area.Description);

            Assert.Equal(world, area.InWorld);
            Assert.Equal(expectedRoomVnums.Count, actualRoomVnums.Count);

            for (int i = 0; i < expectedRoomVnums.Count; i++)
            {
                Assert.Equal(expectedRoomVnums[i], actualRoomVnums[i]);
            }

            Assert.Equal(this.protoArea.Resets.Count, area.Resets.Count());
        }

        [Fact]
        public void ConstructorFailsOnNullArguments()
        {
            var world = new World(new ProtoArea[] { this.protoArea });
            var ownedRoomVnums = Array.Empty<int>();
            var resets = Array.Empty<Reset>();

            Assert.Throws<ArgumentNullException>(() => new Area(null, world, ownedRoomVnums, resets));
            Assert.Throws<ArgumentNullException>(() => new Area(this.protoArea.Info, null, ownedRoomVnums, resets));
            Assert.Throws<ArgumentNullException>(() => new Area(this.protoArea.Info, world, null, resets));
            Assert.Throws<ArgumentNullException>(() => new Area(this.protoArea.Info, world, ownedRoomVnums, null));
        }
    }
}
