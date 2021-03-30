using System;

namespace Mem.Core
{
    public class Exit
    {
        public Exit(ProtoExit proto)
        {
            if (proto is null)
            {
                throw new ArgumentNullException(nameof(proto));
            }

            this.Direction = proto.Direction;
            this.TargetVnum = proto.TargetVnum;
            this.Description = proto.Description;
        }

        public Direction Direction { get; }

        public int TargetVnum { get; }

        public string Description { get; }
    }
}
