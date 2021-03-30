using System;
using System.IO;

namespace Mem.Core.Moderator
{
    public class FileCharService : ICharService
    {
        private readonly IMudConfiguration config;
        private readonly ITranslitConverter converter;
        private readonly IFileService<ProtoChar> fileService;

        public FileCharService(IMudConfiguration config,
                               ITranslitConverter converter,
                               IFileService<ProtoChar> fileService)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));
            this.converter = converter ?? throw new ArgumentNullException(nameof(converter));
            this.fileService = fileService ?? throw new ArgumentNullException(nameof(fileService));
        }

        public bool CharExists(string name) => File.Exists(this.GetPath(name));

        public bool TryLoadCredentials(string name, out Credentials creds)
        {
            var charExists = this.CharExists(name);
            creds = charExists ? this.fileService.LoadData(this.GetPath(name)).Credentials : null;
            return charExists;
        }

        public ProtoChar LoadChar(string name)
        {
            return this.fileService.LoadData(this.GetPath(name));
        }

        public void SaveChar(ProtoChar charData)
        {
            this.fileService.SaveData(charData, this.GetPath(charData?.Credentials.Login));
        }

        private string GetPath(string name)
        {
            return $"{this.config.CharDirectory}{this.converter.Convert(name)}";
        }
    }
}
