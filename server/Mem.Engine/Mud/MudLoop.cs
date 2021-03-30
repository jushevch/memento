using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Mem.Core;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;

namespace Mem.Engine.Mud
{
    internal class MudLoop : BackgroundService
    {
        private readonly IHubContext<MudHub> hub;
        private readonly IWorldHandler worldHandler;
        private readonly IInputHandler inputHandler;
        private readonly IMudLogger log;

        private readonly Stopwatch timer = new ();

        public MudLoop(IHubContext<MudHub> hub,
                       IWorldHandler worldHandler,
                       IInputHandler inputHandler,
                       IMudLogger log)
        {
            this.hub = hub ?? throw new ArgumentNullException(nameof(hub));
            this.worldHandler = worldHandler ?? throw new ArgumentNullException(nameof(worldHandler));
            this.inputHandler = inputHandler ?? throw new ArgumentNullException(nameof(inputHandler));
            this.log = log ?? throw new ArgumentNullException(nameof(log));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                this.worldHandler.BuildWorld();
            }
            catch (Exception ex)
            {
                this.log.Fatal(ex.Message);
                this.log.Fatal("Failed to build the world, the engine cannot be run.");
                return;
            }

            this.log.Info("The engine is on.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await this.PerformOnePulseMotion();
                }
                catch (Exception ex)
                {
                    this.log.Fatal(ex.Message);
                    break;
                }
            }

            this.log.Warn("The engine is off.");
        }

        private async Task PerformOnePulseMotion()
        {
            this.timer.Restart();

            this.DisposeDisconnectedUsers();
            this.AdoptConnectedUsers();
            this.DeliverUserInputIntoGame();
            this.worldHandler.SpinWorld();
            this.DeliverGameOutputToUsers();

            this.timer.Stop();

            await MaintainPulse(this.timer.ElapsedMilliseconds);
        }

        private void DisposeDisconnectedUsers()
        {
            // Handle the disconnect signal from a client
            while (MudUsers.DisconnectedIds.TryDequeue(out var id))
            {
                var user = MudUsers.Get(id);
                user.Status = UserStatus.Disconnected;
                this.inputHandler.Handle(user, string.Empty);

                MudUsers.Remove(id);
            }

            // Handle the quit command
            foreach (var user in MudUsers.Active.Where(u => u.Status == UserStatus.Quit))
            {
                this.hub.Clients.Client(user.Id).SendAsync("Disconnect");
            }
        }

        private void AdoptConnectedUsers()
        {
            while (MudUsers.ConnectedIds.TryDequeue(out var id))
            {
                MudUsers.Add(id);
                this.inputHandler.Handle(MudUsers.Get(id), string.Empty);
            }
        }

        private void DeliverUserInputIntoGame()
        {
            foreach (var user in MudUsers.Active)
            {
                if (user.InputQueue.TryDequeue(out var input))
                {
                    this.inputHandler.Handle(user, input);
                }
            }
        }

        private void DeliverGameOutputToUsers()
        {
            foreach (var user in MudUsers.Active)
            {
                if (user.Output.Length > 0)
                {
                    user.Output.Append(user.Prompt);
                    var output = user.Output.ToString();
                    user.Output.Length = 0;

                    this.hub.Clients.Client(user.Id).SendAsync("ReceiveGameOutput", output);
                }
            }
        }

        private static async Task MaintainPulse(long elapsedTimeMilliseconds)
        {
            const int oneSecond = 1000;
            const int spinRatePerSecond = 4;

            var sleepTime = (oneSecond / spinRatePerSecond) - elapsedTimeMilliseconds;

            if (sleepTime > 0)
            {
                await Task.Delay((int)sleepTime);
            }
        }
    }
}
