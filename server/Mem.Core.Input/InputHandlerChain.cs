namespace Mem.Core.Input
{
    public class InputHandlerChain : IInputHandler
    {
        private readonly InputHandler chain;

        public InputHandlerChain(ICommandHandler commandHandler,
                                 ICharService charService,
                                 IMudConfiguration config,
                                 IWorldKeeper worldKeeper)
        {
            (this.chain = new Disconnecter())
                .Then(new Cleanser())
                .Then(new Echo())
                .Then(new Commander(commandHandler))
                .Then(new Authenticator(charService))
                .Then(new Creator(charService, config))
                .Then(new Launcher(charService, commandHandler, worldKeeper));
        }

        public void Handle(User user, string input) => this.chain.Handle(user, input);
    }
}
