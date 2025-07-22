using System.Collections.Generic;

namespace EthanKennerly.PoorLife
{
    public class MainSystem : ISystem
    {
        private readonly SystemUpdater _updater;

        public MainSystem(IPoorLifeMainAuthoring authoring)
        {
            _updater = new SystemUpdater();
            _updater.AddSystem(new MalariaSystem());
            _updater.AddSystem(new HealthPerYearSystem());
            _updater.AddSystem(new AgeUpLogSystem());
            _updater.AddSystem(new DeathPopupSystem(authoring));
        }

        public void Update(float deltaTime, List<step_log> commands)
        {
            _updater.Update(deltaTime, commands);
        }
    }
}
