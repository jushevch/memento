using Mem.Core;
using Mem.Core.Commands;
using Mem.Core.Input;
using Mem.Core.Moderator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mem.Engine.Mud
{
    internal static class MudInitializer
    {
        public static void AddMud(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<IMudConfiguration>(
                new MudConfiguration(
                    config["Mud:AreaDirectory"],
                    config["Mud:CharDirectory"],
                    int.Parse(config["Mud:StartRoomVnum"])));

            services.AddSingleton<IMudLogger, SerilogMudLogger>();

            services.AddSingleton<IFileService<ProtoArea>, JsonFileService<ProtoArea>>();
            services.AddSingleton<IFileService<ProtoChar>, JsonFileService<ProtoChar>>();

            services.AddSingleton<ITranslitConverter, CyrillicTranslitConverter>();
            services.AddSingleton<ICharService, FileCharService>();

            services.AddSingleton<IAreaLoader, FileAreaLoader>();
            services.AddSingleton<IWorldKeeper, WorldKeeper>();
            services.AddSingleton<IWorldHandler, WorldHandler>();

            services.AddSingleton<IInputHandler, InputHandlerChain>();
            services.AddSingleton<ICommandHandler, CommandHandlerChain>();

            services.AddHostedService<MudLoop>();
        }
    }
}
