using System.Collections.Generic;

namespace Mem.Core
{
    public interface IAreaLoader
    {
        IEnumerable<ProtoArea> LoadAreas();
    }
}
