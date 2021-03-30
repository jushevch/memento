using System.Linq;

namespace Mem.Core.Commands
{
    internal class GeneralCommandHandler : CommandHandler
    {
        public GeneralCommandHandler()
        {
            this.CommandTable['б'] = new ()
            {
                { "бросить", this.Drop },
            };

            this.CommandTable['в'] = new ()
            {
                { "взять", this.Take },
                { "выходы", this.Exits },
            };

            this.CommandTable['и'] = new ()
            {
                { "инвентарь", this.Inventory },
            };

            this.CommandTable['к'] = new ()
            {
                { "конец", this.Quit },
            };

            this.CommandTable['с'] = new ()
            {
                { "смотреть", this.Look },
            };
        }

        private void Exits(Mobile actor, string args)
        {
            foreach (var exit in actor.InRoom.Exits.Values.OrderBy(e => e.Direction))
            {
                exit.DescribeTo(actor);
            }
        }

        private void Quit(Mobile actor, string args)
        {
            if (actor.User != null)
            {
                actor.User.Status = UserStatus.Quit;
            }
        }

        private void Look(Mobile actor, string args)
        {
            if (string.IsNullOrEmpty(args))
            {
                actor.InRoom.DescribeTo(actor);
            }
            else if (actor.Items.TryPickItem(args, out var inventoryItem))
            {
                inventoryItem.DescribeTo(actor);
            }
            else if (actor.InRoom.Items.TryPickItem(args, out var roomItem))
            {
                roomItem.DescribeTo(actor);
            }
            else if (actor.InRoom.Mobiles.TryPickMobile(args, out var mob))
            {
                mob.DescribeTo(actor);
            }
            else if (actor.InRoom.Exits.Values.TryPickExit(args, out var exit))
            {
                exit.DescribeTo(actor);
            }
            else
            {
                actor.User?.Output.Append("Этого здесь нет.<br>");
            }
        }

        private void Take(Mobile actor, string args)
        {
            if (string.IsNullOrEmpty(args))
            {
                actor.User?.Output.Append("Взять что?<br>");
            }
            else if (actor.InRoom.Items.TryPickItem(args, out var item))
            {
                actor.InRoom.Remove(item);
                actor.Add(item);
                actor.ActTake(item);
            }
            else
            {
                actor.User?.Output.Append("Этого здесь нет.<br>");
            }
        }

        private void Drop(Mobile actor, string args)
        {
            if (string.IsNullOrEmpty(args))
            {
                actor.User?.Output.Append("Бросить что?<br>");
            }
            else if (actor.Items.TryPickItem(args, out var item))
            {
                actor.Remove(item);
                actor.InRoom.Add(item);
                actor.ActDrop(item);
            }
            else
            {
                actor.User?.Output.Append("У вас этого нет.<br>");
            }
        }

        private void Inventory(Mobile actor, string args)
        {
            if (!actor.Items.Any())
            {
                actor.User?.Output.Append("Ваш инвентарь пуст.<br>");
            }
            else
            {
                actor.User?.Output.Append("Вы несете:<br>".PaintWith(Color.White));

                foreach (var item in actor.Items)
                {
                    actor.User?.Output.Append($"{item.Name.Nom}<br>");
                }
            }
        }
    }
}
