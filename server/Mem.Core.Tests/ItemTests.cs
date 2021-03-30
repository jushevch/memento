using System;
using Xunit;

namespace Mem.Core.Tests
{
    public class ItemTests
    {
        [Fact]
        public void InitializesCorrectly()
        {
            var proto = new ProtoItem
            {
                Vnum = 25300,
                Keywords = "several keywords",
                Gender = Gender.Male,
                Name = new Name(),
                Look = "Look",
                Description = "Description"
            };

            var item = new Item(proto);

            Assert.Equal(proto.Vnum, item.Vnum);
            Assert.Equal(proto.Keywords, item.Keywords);
            Assert.Equal(proto.Gender, item.Gender);
            Assert.Equal(proto.Name, item.Name);
            Assert.Equal(proto.Look, item.Look);
            Assert.Equal(proto.Description, item.Description);
        }

        [Fact]
        public void ConstructorFailsOnNullPrototype()
        {
            Assert.Throws<ArgumentNullException>(() => new Item(null));
        }
    }
}
