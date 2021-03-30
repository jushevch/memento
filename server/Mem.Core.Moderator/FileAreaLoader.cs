using System;
using System.Collections.Generic;
using System.IO;

namespace Mem.Core.Moderator
{
    public class FileAreaLoader : IAreaLoader
    {
        private readonly IFileService<ProtoArea> fileService;
        private readonly IMudConfiguration config;
        private readonly IMudLogger log;

        public FileAreaLoader(IMudConfiguration config, IFileService<ProtoArea> fileService, IMudLogger log)
        {
            this.fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
            this.config = config ?? throw new ArgumentNullException(nameof(config));
            this.log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public IEnumerable<ProtoArea> LoadAreas()
        {
            var areas = new HashSet<ProtoArea>();
            var filePaths = Directory.EnumerateFiles(this.config.AreaDirectory);

            this.log.Info("Loading area files...");

            foreach (var path in filePaths)
            {
                var fileName = path.Replace(this.config.AreaDirectory, string.Empty);

                try
                {
                    areas.Add(this.fileService.LoadData(path));
                    this.log.Info("{FileName}", fileName);
                }
                catch (FileServiceException)
                {
                    this.log.Warn("Failed to load {FileName}, skipping.", fileName);
                    continue;
                }
            }

            return areas;
        }
    }
}
