namespace Mem.Core.Input
{
    internal class Disconnecter : InputHandler
    {
        public Disconnecter()
        {
            this.Action[UserStatus.Disconnected] = this.Disconnect;
        }

        private void Disconnect(User user, string input)
        {
            if (user.Character != null)
            {
                user.Character.InRoom.Remove(user.Character);
            }
        }
    }
}
