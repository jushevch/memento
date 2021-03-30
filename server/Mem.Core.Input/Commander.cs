using System;

namespace Mem.Core.Input
{
    internal class Commander : InputHandler
    {
        private readonly ICommandHandler handler;

        public Commander(ICommandHandler handler)
        {
            this.handler = handler ?? throw new ArgumentNullException(nameof(handler));

            this.Action[UserStatus.InGame] = this.HandleCommand;
        }

        private void HandleCommand(User user, string input)
        {
            SplitInput(input, out var comm, out var args);
            this.handler.Handle(user.Character, comm.ToLowerInvariant(), args);
        }

        private static void SplitInput(string input, out string comm, out string args)
        {
            var i = input.IndexOf(' ', StringComparison.Ordinal);

            comm = i < 0 ? input : input.Substring(0, i);
            args = i < 0 ? string.Empty : input[(i + 1) ..];
        }
    }
}
