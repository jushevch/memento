using System;

namespace Mem.Core.Input
{
    internal class Launcher : InputHandler
    {
        private readonly IWorldKeeper worldKeeper;
        private readonly ICharService charService;
        private readonly ICommandHandler commandHandler;

        public Launcher(ICharService charService,
                        ICommandHandler commandHandler,
                        IWorldKeeper worldKeeper)
        {
            this.worldKeeper = worldKeeper ?? throw new ArgumentNullException(nameof(worldKeeper));
            this.charService = charService ?? throw new ArgumentNullException(nameof(charService));
            this.commandHandler = commandHandler ?? throw new ArgumentNullException(nameof(commandHandler));

            this.Action[UserStatus.Launch] = this.Launch;
        }

        private void Launch(User user, string input)
        {
            var character = this.charService.LoadChar(user.Credentials.Login);
            var mob = new Mobile(character.Mobile, user);

            user.Character = mob;
            user.Status = UserStatus.InGame;

            user.Output.Append("<br>Добро пожаловать!<br><br>".PaintWith(Color.Green));

            this.worldKeeper.World.Rooms[character.InRoomVnum].Add(user.Character);

            this.commandHandler.Handle(user.Character, "смотреть", string.Empty);
        }
    }
}
