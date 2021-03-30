namespace Mem.Core
{
    public interface IMudLogger
    {
        void Info(string message, params object[] values);

        void Warn(string message, params object[] values);

        void Error(string message, params object[] values);

        void Fatal(string message, params object[] values);
    }
}
