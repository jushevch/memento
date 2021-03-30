using System;
using System.Collections.Generic;

namespace Mem.Core.Commands
{
    internal abstract class CommandHandler : ICommandHandler
    {
        protected Dictionary<char, Dictionary<string, Action<Mobile, string>>> CommandTable { get; } = new ();

        protected CommandHandler Next { get; private set; }

        public CommandHandler Then(CommandHandler handler)
        {
            return this.Next = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        public void Handle(Mobile actor, string comm, string args)
        {
            var firstLetter = comm[0];

            if (this.CommandTable.ContainsKey(firstLetter))
            {
                foreach (var kvp in this.CommandTable[firstLetter])
                {
                    if (kvp.Key.StartsWith(comm, StringComparison.Ordinal))
                    {
                        kvp.Value.Invoke(actor, args);
                        return;
                    }
                }
            }

            if (this.Next is null)
            {
                actor.User?.Output.Append("Что-что?<br>");
            }
            else
            {
                this.Next.Handle(actor, comm, args);
            }
        }
    }
}
