using System;
using Xunit;

namespace Mem.Core.Tests
{
    public class ResetTests
    {
        [Theory]
        [InlineData("RepopItem 25300 25400", ResetCommand.RepopItem, 25300, 25400)]
        [InlineData("RepopMobile 25100 25200", ResetCommand.RepopMobile, 25100, 25200)]
        public void InitializesCorrectly(string instruction, ResetCommand expComm, int expSubj, int expObj)
        {
            var reset = new Reset(instruction);

            Assert.Equal(expComm, reset.Command);
            Assert.Equal(expSubj, reset.Subject);
            Assert.Equal(expObj, reset.Object);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("       ")]
        [InlineData("invalid")]
        [InlineData("7934275")]
        [InlineData("InvalidResetCommand 25300 25400")]
        [InlineData("RepopItem string 25400")]
        [InlineData("RepopMobile 25300 string")]
        [InlineData("RepopItem 25300")]
        [InlineData("RepopMobile")]
        public void ConstructorFailsOnInvalidInstructions(string instructions)
        {
            if (string.IsNullOrWhiteSpace(instructions))
            {
                Assert.Throws<ArgumentNullException>(() => new Reset(instructions));
            }
            else
            {
                Assert.Throws<ArgumentException>(() => new Reset(instructions));
            }
        }
    }
}
