namespace Mem.Core
{
    public interface ICharService
    {
        bool CharExists(string name);

        bool TryLoadCredentials(string name, out Credentials creds);

        ProtoChar LoadChar(string name);

        void SaveChar(ProtoChar character);
    }
}
