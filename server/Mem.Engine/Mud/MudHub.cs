using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Mem.Engine.Mud
{
    internal class MudHub : Hub
    {
        public override Task OnDisconnectedAsync(Exception exception)
        {
            MudUsers.DisconnectedIds.Enqueue(this.Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        public void AcceptNewUser()
        {
            MudUsers.ConnectedIds.Enqueue(this.Context.ConnectionId);
        }

        public void ProcessUserInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return;
            }

            MudUsers.Get(this.Context.ConnectionId).InputQueue.Enqueue(input);
        }
    }
}
