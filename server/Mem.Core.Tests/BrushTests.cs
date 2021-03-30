using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Xunit;

namespace Mem.Core.Tests
{
    public class BrushTests
    {
        [Fact]
        public void GetsColorCodes()
        {
            foreach (var color in Enum.GetValues<Color>())
            {
                Assert.False(string.IsNullOrWhiteSpace(color.GetCode()));
            }
        }

        [Fact]
        public void GetsDifferentColorCodes()
        {
            var hash = new HashSet<string>();

            foreach (var color in Enum.GetValues<Color>())
            {
                Assert.True(hash.Add(color.GetCode()));
            }
        }

        [Fact]
        public void GetsRegularCodeOnInvalidValue()
        {
            var expected = Color.Regular.GetCode();
            var actual = ((Color)(-1)).GetCode();

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("paint me with black", Color.Black)]
        [InlineData("lady in red", Color.Red)]
        [InlineData("we all live in a yellow submarine", Color.Yellow)]
        public void PaintsCorrectly(string str, Color color)
        {
            var regex = new Regex($"^<span style=\"color:{color.GetCode()}\">{str}</span>$");
            Assert.Matches(regex, str.PaintWith(color));
        }
    }
}
