using System;

namespace Mem.Core
{
    public enum ResetCommand
    {
        None,
        RepopItem,
        RepopMobile,
    }

    public class Reset
    {
        public Reset(string instruction)
        {
            if (string.IsNullOrWhiteSpace(instruction))
            {
                throw new ArgumentNullException(nameof(instruction));
            }

            try
            {
                var args = instruction.Split(' ');

                this.Command = Enum.Parse<ResetCommand>(args[0]);
                this.Subject = int.Parse(args[1]);
                this.Object = int.Parse(args[2]);
            }
            catch
            {
                throw new ArgumentException(
                    $"Failed to parse a reset instruction: {instruction}", nameof(instruction));
            }
        }

        public ResetCommand Command { get; }

        public int Subject { get; }

        public int Object { get; }
    }
}
