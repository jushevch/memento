using System.Collections.Generic;

namespace Mem.Core
{
    public class ProtoArea
    {
        public ProtoAreaInfo Info { get; set; }

        public List<ProtoRoom> Rooms { get; set; }

        public List<ProtoItem> Items { get; set; }

        public List<ProtoMobile> Mobiles { get; set; }

        public List<string> Resets { get; set; }
    }
}
