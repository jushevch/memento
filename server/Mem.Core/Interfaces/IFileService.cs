namespace Mem.Core
{
    public interface IFileService<T>
    {
        T LoadData(string path);

        void SaveData(T data, string path);
    }
}
