using System;
using Xunit;

namespace Mem.Core.Tests
{
    public class AffixPickerTests
    {
        [Theory]
        [InlineData(Gender.Male, "ел|ла|ло|ли", "ел")]
        [InlineData(Gender.Female, "ел|ла|ло|ли", "ла")]
        [InlineData(Gender.Neutral, "ел|ла|ло|ли", "ло")]
        [InlineData(Gender.Plural, "ел|ла|ло|ли", "ли")]
        [InlineData(Gender.Neutral, "еллалоли", "еллалоли")]
        [InlineData(Gender.Neutral, null, null)]
        [InlineData(Gender.Plural, "ел|ла|ло", "ел|ла|ло")]
        [InlineData(Gender.Plural, "", "")]
        [InlineData((Gender)(-1), "ел|ла|ло", "ел|ла|ло")]
        public void ReturnsCorrectValue(Gender gender, string values, string expected)
        {
            var entity = new Mobile(new ProtoMobile() { Gender = gender });
            Assert.Equal(expected, entity.Affix(values));
        }

        [Fact]
        public void ThrowsExceptionOnNullEntity()
        {
            Assert.Throws<ArgumentNullException>(() => (null as Mobile).Affix(string.Empty));
        }
    }
}
