using System;
using Xunit;

namespace Mem.Core.Tests
{
    public class UserTests
    {
        [Fact]
        public void InitializesCorrectly()
        {
            var id = new Guid().ToString();
            var user = new User(id);

            Assert.Equal(id, user.Id);
            Assert.False(user.Prompt is null);
            Assert.False(user.InputQueue is null);
            Assert.False(user.Output is null);
        }

        [Fact]
        public void ConstructorFailsOnInvalidId()
        {
            Assert.Throws<ArgumentException>(() => new User(null));
            Assert.Throws<ArgumentException>(() => new User(string.Empty));
            Assert.Throws<ArgumentException>(() => new User("     "));
        }
    }
}
