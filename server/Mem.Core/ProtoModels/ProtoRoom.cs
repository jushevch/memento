using System.Collections.Generic;

namespace Mem.Core
{
    public class ProtoRoom
    {
        public int Vnum { get; set; }

        public string Title { get; set; }

        public string Look { get; set; }

        public List<ProtoExit> Exits { get; set; }
    }
}
