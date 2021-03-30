using System;
using System.Collections.Generic;

namespace Mem.Core.Input
{
    internal abstract class InputHandler : IInputHandler
    {
        protected Dictionary<UserStatus, Action<User, string>> Action { get; } = new ();

        protected Dictionary<UserStatus, string> Prompt { get; } = new ();

        protected InputHandler Next { get; private set; }

        public InputHandler Then(InputHandler handler)
        {
            return this.Next = handler ?? throw new ArgumentNullException(nameof(handler));
        }

        public virtual void Handle(User user, string input)
        {
            if (this.Action.ContainsKey(user.Status))
            {
                this.Action[user.Status].Invoke(user, input);
            }
            else
            {
                this.Next?.Handle(user, input);
            }
        }
    }
}
