using Serilog;

namespace Mem.Core.Moderator
{
    public class SerilogMudLogger : IMudLogger
    {
        private readonly ILogger log;

        public SerilogMudLogger()
        {
            this.log = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        }

        public void Info(string message, params object[] values) => this.log.Information(message, values);

        public void Warn(string message, params object[] values) => this.log.Warning(message, values);

        public void Error(string message, params object[] values) => this.log.Error(message, values);

        public void Fatal(string message, params object[] values) => this.log.Fatal(message, values);
    }
}
