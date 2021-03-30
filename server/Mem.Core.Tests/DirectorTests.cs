using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Mem.Core.Tests
{
    public class DirectorTests
    {
        [Fact]
        public void GetsDifferentNames()
        {
            var hash = new HashSet<string>();

            foreach (var dir in Enum.GetValues<Direction>())
            {
                Assert.True(hash.Add(dir.GetName()));
            }
        }

        [Fact]
        public void GetsDirections()
        {
            var names = Enum.GetValues<Direction>().Select(dir => dir.GetName());

            foreach (var name in names)
            {
                Director.GetDirection(name);
            }
        }

        [Fact]
        public void GetsOpposites()
        {
            foreach (var dir in Enum.GetValues<Direction>())
            {
                dir.GetOpposite();
            }
        }

        [Fact]
        public void FailsToGetNameOnInvalidDirection()
        {
            Assert.Throws<ArgumentException>(() => ((Direction)(-1)).GetName());
        }

        [Fact]
        public void FailsToGetDirectionOnInvalidName()
        {
            Assert.Throws<ArgumentException>(() => Director.GetDirection((-1).ToString()));
        }

        [Fact]
        public void FailsToGetOppositeOnInvalidDirection()
        {
            Assert.Throws<ArgumentException>(() => ((Direction)(-1)).GetOpposite());
        }
    }
}
