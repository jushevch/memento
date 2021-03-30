using System;
using System.Collections.Generic;

namespace Mem.Core
{
    public class Mobile : IGender
    {
        private readonly HashSet<Item> itemsInside = new ();

        public Mobile(ProtoMobile proto, User user = null)
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
            this.User = user;
        }

        public int Vnum { get; }

        public string Keywords { get; }

        public Gender Gender { get; }

        public Name Name { get; }

        public string Look { get; }

        public string Description { get; }

        public Room InRoom { get; set; }

        public bool IsPc => this.User != null;

        public bool IsNpc => !this.IsPc;

        public User User { get; }

        public IEnumerable<Item> Items => this.itemsInside;

        public void Add(Item item)
        {
            if (item is null || !this.itemsInside.Add(item))
            {
                throw new InvalidOperationException("Failed to add an item into a mobile.");
            }

            item.InMobile = this;
        }

        public void Remove(Item item)
        {
            if (item is null || !this.itemsInside.Remove(item))
            {
                throw new InvalidOperationException("Failed to remove an item from a mobile.");
            }

            item.InMobile = null;
        }
    }
}
