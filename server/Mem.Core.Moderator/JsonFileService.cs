using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace Mem.Core.Moderator
{
    public class JsonFileService<T> : IFileService<T>
    {
        private readonly JsonSerializerOptions options;

        public JsonFileService()
        {
            this.options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            };

            this.options.Converters.Add(new JsonStringEnumConverter());
        }

        public T LoadData(string path)
        {
            try
            {
                return JsonSerializer.Deserialize<T>(File.ReadAllText(path), this.options);
            }
            catch
            {
                throw new FileServiceException();
            }
        }

        public void SaveData(T data, string path)
        {
            try
            {
                File.WriteAllText(path, JsonSerializer.Serialize(data, this.options));
            }
            catch
            {
                throw new FileServiceException();
            }
        }
    }
}
