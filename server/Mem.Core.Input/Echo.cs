namespace Mem.Core.Input
{
    internal class Echo : InputHandler
    {
        public override void Handle(User user, string input)
        {
            if (user.Status != UserStatus.Connected)
            {
                user.Output.Append($"{input.PaintWith(Color.Black)}<br>");
            }

            this.Next?.Handle(user, input);
        }
    }
}
