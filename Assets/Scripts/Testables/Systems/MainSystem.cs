using System.Collections.Generic;
using TouchReplaying;

namespace EthanKennerly.SaveLife
{
    public class MainSystem : ISystem
    {
        private readonly SystemUpdater _updater;

        public MainSystem(IMainAuthoring authoring)
        {
            _updater = new SystemUpdater();
            _updater.AddSystem(new TouchReplaySystem(authoring.TouchReplay));
            _updater.AddSystem(new MalariaSystem());
            _updater.AddSystem(new HealthPerYearSystem());
            _updater.AddSystem(new AgeUpLogSystem());
            _updater.AddSystem(new DeathPopupSystem(authoring));
            _updater.AddSystem(new TouchRecordingSystem(authoring.TouchRecording, new TouchInputManager()));
        }

        public void Update(float deltaTime, List<IComponent> commands)
        {
            _updater.Update(deltaTime, commands);
        }
    }
}
