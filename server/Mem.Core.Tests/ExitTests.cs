using System;
using Xunit;

namespace Mem.Core.Tests
{
    public class ExitTests
    {
        [Fact]
        public void InitializesCorrectly()
        {
            var proto = new ProtoExit
            {
                Direction = Direction.Somewhere,
                TargetVnum = 25300,
                Description = "Description"
            };

            var exit = new Exit(proto);

            Assert.Equal(proto.Direction, exit.Direction);
            Assert.Equal(proto.TargetVnum, exit.TargetVnum);
            Assert.Equal(proto.Description, exit.Description);
        }

        [Fact]
        public void ConstructorFailsOnNullPrototype()
        {
            Assert.Throws<ArgumentNullException>(() => new Exit(null));
        }
    }
}
