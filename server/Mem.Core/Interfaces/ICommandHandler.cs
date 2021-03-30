namespace Mem.Core
{
    public interface ICommandHandler
    {
        void Handle(Mobile actor, string comm, string args);
    }
}
