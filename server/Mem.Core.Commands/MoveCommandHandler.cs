namespace Mem.Core.Commands
{
    internal class MoveCommandHandler : CommandHandler
    {
        public MoveCommandHandler()
        {
            this.CommandTable['в'] = new ()
            {
                { "восток", this.East },
                { "вверх", this.Up },
                { "вниз", this.Down },
                { "войти", this.Somewhere },
            };

            this.CommandTable['з'] = new ()
            {
                { "запад", this.West },
            };

            this.CommandTable['с'] = new ()
            {
                { "север", this.North },
                { "северо-восток", this.NorthEast },
                { "северо-запад", this.NorthWest },
                { "св", this.NorthEast },
                { "сз", this.NorthWest },
            };

            this.CommandTable['ю'] = new ()
            {
                { "юг", this.South },
                { "юго-восток", this.SouthEast},
                { "юго-запад", this.SouthWest },
                { "юв", this.SouthEast },
                { "юз", this.SouthWest },
            };
        }

        private void North(Mobile actor, string args) => this.Move(actor, Direction.North);

        private void East(Mobile actor, string args) => this.Move(actor, Direction.East);

        private void South(Mobile actor, string args) => this.Move(actor, Direction.South);

        private void West(Mobile actor, string args) => this.Move(actor, Direction.West);

        private void Up(Mobile actor, string args) => this.Move(actor, Direction.Up);

        private void Down(Mobile actor, string args) => this.Move(actor, Direction.Down);

        private void Somewhere(Mobile actor, string args) => this.Move(actor, Direction.Somewhere);

        private void NorthEast(Mobile actor, string args) => this.Move(actor, Direction.NorthEast);

        private void SouthEast(Mobile actor, string args) => this.Move(actor, Direction.SouthEast);

        private void SouthWest(Mobile actor, string args) => this.Move(actor, Direction.SouthWest);

        private void NorthWest(Mobile actor, string args) => this.Move(actor, Direction.NorthWest);

        private void Move(Mobile actor, Direction dir)
        {
            if (actor.InRoom.Exits.TryGetValue(dir, out var exit))
            {
                var targetRoom = actor.InRoom.InArea.InWorld.Rooms[exit.TargetVnum];

                actor.ActLeaveTo(dir);
                actor.InRoom.Remove(actor);
                targetRoom.Add(actor);
                actor.ActEnterFrom(dir.GetOpposite());

                this.Next.Handle(actor, "смотреть", string.Empty);
            }
            else
            {
                actor.User?.Output.Append("Вы не можете идти в этом направлении.<br>");
            }
        }
    }
}
