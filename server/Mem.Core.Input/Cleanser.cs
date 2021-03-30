using System;

namespace Mem.Core.Input
{
    internal class Cleanser : InputHandler
    {
        public override void Handle(User user, string input)
        {
            this.Next?.Handle(user, this.Cleanse(input));
        }

        private string Cleanse(string input) => input
            .Trim()
            .Replace("<", "&lt;", StringComparison.Ordinal)
            .Replace(">", "&gt;", StringComparison.Ordinal);
    }
}
