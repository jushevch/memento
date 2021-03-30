using System;
using System.Collections.Generic;

namespace Mem.Core
{
    public class Area
    {
        public Area(ProtoAreaInfo info,
                    World inWorld,
                    IEnumerable<int> ownedRoomVnums,
                    IEnumerable<Reset> resets)
        {
            if (info is null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            this.Title = info.Title;
            this.Author = info.Author;
            this.Description = info.Description;

            this.InWorld = inWorld ?? throw new ArgumentNullException(nameof(inWorld));
            this.RoomVnums = ownedRoomVnums ?? throw new ArgumentNullException(nameof(ownedRoomVnums));
            this.Resets = resets ?? throw new ArgumentNullException(nameof(resets));
        }

        public string Title { get; }

        public string Author { get; }

        public string Description { get; }

        public World InWorld { get; }

        public IEnumerable<int> RoomVnums { get; }

        public IEnumerable<Reset> Resets { get; }
    }
}
