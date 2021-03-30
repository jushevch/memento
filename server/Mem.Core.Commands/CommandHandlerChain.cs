namespace Mem.Core.Commands
{
    public class CommandHandlerChain : ICommandHandler
    {
        private readonly CommandHandler chain;

        public CommandHandlerChain()
        {
            (this.chain = new MoveCommandHandler()).Then(new GeneralCommandHandler());
        }

        public void Handle(Mobile actor, string comm, string args)
        {
            this.chain.Handle(actor, comm, args);
        }
    }
}
