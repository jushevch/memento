namespace Mem.Core.Input
{
    public class NewChar : Credentials
    {
        public Name Name { get; } = new ();

        public Gender Gender { get; set; }
    }
}
