using System;
using System.IO;

namespace Mem.Core.Moderator
{
    public class MudConfiguration : IMudConfiguration
    {
        public MudConfiguration(string areaDirectory, string charDirectory, int startRoomVnum)
        {
            this.ResolveDirectory(areaDirectory);
            this.ResolveDirectory(charDirectory);

            this.AreaDirectory = areaDirectory;
            this.CharDirectory = charDirectory;
            this.StartRoomVnum = startRoomVnum;
        }

        public string AreaDirectory { get; }

        public string CharDirectory { get; }

        public int StartRoomVnum { get; }

        private void ResolveDirectory(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException($"Empty directory path value.");
            }

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
