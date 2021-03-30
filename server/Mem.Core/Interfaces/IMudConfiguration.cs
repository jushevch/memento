namespace Mem.Core
{
    public interface IMudConfiguration
    {
        string AreaDirectory { get; }

        string CharDirectory { get; }

        int StartRoomVnum { get; }
    }
}
