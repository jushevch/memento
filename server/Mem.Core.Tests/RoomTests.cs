using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Mem.Core.Tests
{
    public class RoomTests
    {
        private readonly ProtoArea protoArea = new ()
        {
            Info = new ProtoAreaInfo(),
            Rooms = new List<ProtoRoom>
            {
                new ProtoRoom
                {
                    Vnum = 1,
                    Title = "Room Title.",
                    Look = "Room Look.",
                    Exits = new List<ProtoExit>
                    {
                        new ProtoExit
                        {
                            Direction = Direction.North,
                            TargetVnum = 2,
                            Description = "North Exit Description."
                        },
                        new ProtoExit
                        {
                            Direction = Direction.South,
                            TargetVnum = 3,
                            Description = "South Exit Description."
                        },
                        new ProtoExit
                        {
                            Direction = Direction.Somewhere,
                            TargetVnum = 4,
                            Description = "Somewhere Exit Description."
                        }
                    }
                }
            },
            Items = new List<ProtoItem>()
            {
                new ProtoItem
                {
                    Vnum = 1,
                    Look = "Item Look.",
                    Description = "Item Description."
                }
            },
            Mobiles = new List<ProtoMobile>()
            {
                new ProtoMobile
                {
                    Vnum = 1,
                    Look = "Mobile One Look.",
                    Description = "Mobile One Description."
                },
                new ProtoMobile
                {
                    Vnum = 2,
                    Look = "Mobile Two Look.",
                    Description = "Mobile Two Description."
                }
            },
            Resets = new List<string>()
        };

        [Fact]
        public void InitializesCorrectly()
        {
            var world = new World(new ProtoArea[] { this.protoArea });
            var area = world.Areas.First();
            var protoRoom = this.protoArea.Rooms.First();
            var room = world.Rooms.Values.First();

            Assert.Equal(area, room.InArea);
            Assert.Equal(protoRoom.Vnum, room.Vnum);
            Assert.Equal(protoRoom.Title, room.Title);
            Assert.Equal(protoRoom.Look, room.Look);
            Assert.Equal(protoRoom.Exits.Count, room.Exits.Count);

            foreach (var protoExit in protoRoom.Exits)
            {
                var exit = room.Exits[protoExit.Direction];

                Assert.Equal(protoExit.Direction, exit.Direction);
                Assert.Equal(protoExit.TargetVnum, exit.TargetVnum);
                Assert.Equal(protoExit.Description, exit.Description);
            }
        }

        [Fact]
        public void ConstructorFailsOnNullArguments()
        {
            var world = new World(new ProtoArea[] { this.protoArea });
            var protoRoom = this.protoArea.Rooms.First();
            var area = world.Areas.First();

            Assert.Throws<ArgumentNullException>(() => new Room(null, area, i => { }, i => { }));
            Assert.Throws<ArgumentNullException>(() => new Room(protoRoom, null, i => { }, i => { }));
            Assert.Throws<ArgumentNullException>(() => new Room(protoRoom, area, null, i => { }));
            Assert.Throws<ArgumentNullException>(() => new Room(protoRoom, area, i => { }, null));
        }

        [Fact]
        public void AddsAndRemovesMobiles()
        {
            var world = new World(new ProtoArea[] { this.protoArea });
            var room = world.Rooms.Values.First();
            var protoMobA = world.ProtoMobiles.Values.First();
            var protoMobB = world.ProtoMobiles.Values.Skip(1).First();

            var ma1 = new Mobile(protoMobA);
            var mb1 = new Mobile(protoMobB);
            var mb2 = new Mobile(protoMobB);

            Assert.True(ma1.Vnum != mb1.Vnum);
            Assert.True(mb1.Vnum == mb2.Vnum);
            Assert.True(mb1 != mb2);

            room.Add(ma1);
            room.Add(mb1);
            room.Add(mb2);

            Assert.True(world.MobileCount[ma1.Vnum] == 1);
            Assert.True(world.MobileCount[mb1.Vnum] == 2);
            Assert.True(room.Mobiles.Count() == 3);

            room.Remove(ma1);
            room.Remove(mb1);
            room.Remove(mb2);

            Assert.True(world.MobileCount[ma1.Vnum] == 0);
            Assert.True(world.MobileCount[mb1.Vnum] == 0);
            Assert.False(room.Mobiles.Any());
        }

        [Fact]
        public void FailsToAddSameMobileInstance()
        {
            var world = new World(new ProtoArea[] { this.protoArea });
            var room = world.Rooms.Values.First();
            var mob = new Mobile(world.ProtoMobiles.Values.First());

            Assert.False(room.Mobiles.Any());

            room.Add(mob);

            Assert.True(room.Mobiles.Count() == 1);
            Assert.Equal(mob, room.Mobiles.First());

            Assert.Throws<InvalidOperationException>(() => room.Add(mob));
        }

        [Fact]
        public void FailsToAddAndRemoveNullMobile()
        {
            var world = new World(new ProtoArea[] { this.protoArea });
            var room = world.Rooms.Values.First();

            Assert.Throws<InvalidOperationException>(() => room.Add(null as Mobile));
            Assert.Throws<InvalidOperationException>(() => room.Remove(null as Mobile));
        }

        [Fact]
        public void AddsItems()
        {
            var world = new World(new ProtoArea[] { this.protoArea });
            var room = world.Rooms.Values.First();
            var protoItem = world.ProtoItems.Values.First();

            var i1 = new Item(protoItem);
            var i2 = new Item(protoItem);

            room.Add(i1);
            room.Add(i2);

            Assert.True(i1.Vnum == i2.Vnum);
            Assert.True(i1 != i2);
            Assert.True(room.Items.Count() == 2);
        }

        [Fact]
        public void FailsToAddSameItemInstance()
        {
            var world = new World(new ProtoArea[] { this.protoArea });
            var room = world.Rooms.Values.First();
            var item = new Item(world.ProtoItems.Values.First());

            Assert.False(room.Items.Any());

            room.Add(item);

            Assert.True(room.Items.Count() == 1);
            Assert.Equal(item, room.Items.First());

            Assert.Throws<InvalidOperationException>(() => room.Add(item));
        }

        [Fact]
        public void FailsToAddNullItem()
        {
            var world = new World(new ProtoArea[] { this.protoArea });
            var room = world.Rooms.Values.First();

            Assert.Throws<InvalidOperationException>(() => room.Add((Item)null));
        }
    }
}
