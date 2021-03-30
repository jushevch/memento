using System;

namespace Mem.Core.Moderator
{
    public class WorldHandler : IWorldHandler
    {
        private readonly IWorldKeeper worldKeeper;
        private readonly IAreaLoader areaLoader;
        private readonly IMudLogger log;

        private bool isInitialResetPerformed;

        public WorldHandler(IWorldKeeper worldKeeper, IAreaLoader areaLoader, IMudLogger log)
        {
            this.worldKeeper = worldKeeper ?? throw new ArgumentNullException(nameof(worldKeeper));
            this.areaLoader = areaLoader ?? throw new ArgumentNullException(nameof(areaLoader));
            this.log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public void BuildWorld()
        {
            this.worldKeeper.World = new World(this.areaLoader.LoadAreas().ReplaceNewLines());
        }

        public void SpinWorld()
        {
            var world = this.worldKeeper.World;

            if (!this.isInitialResetPerformed)
            {
                foreach (var area in world.Areas)
                {
                    this.log.Info("Resetting: {Title}", area.Title);

                    world.Reset(area.Resets);
                }

                this.isInitialResetPerformed = true;
            }
        }
    }
}
