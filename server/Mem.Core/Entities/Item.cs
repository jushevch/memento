using System;

namespace Mem.Core
{
    public class Item
    {
        public Item(ProtoItem proto)
        {
            if (proto is null)
            {
                throw new ArgumentNullException(nameof(proto));
            }

            this.Vnum = proto.Vnum;
            this.Keywords = proto.Keywords;
            this.Gender = proto.Gender;
            this.Name = proto.Name;
            this.Look = proto.Look;
            this.Description = proto.Description;
        }

        public int Vnum { get; }

        public string Keywords { get; }

        public Gender Gender { get; }

        public Name Name { get; }

        public string Look { get; }

        public string Description { get; }

        public Room InRoom { get; set; }

        public Mobile InMobile { get; set; }
    }
}
