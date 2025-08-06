using System.Collections.Generic;

namespace EthanKennerly.SaveLife
{
    public class SystemUpdater : ISystem
    {
        private readonly List<ISystem> _systems;

        public SystemUpdater()
        {
            _systems = new List<ISystem>();
        }

        public void AddSystem(ISystem system)
        {
            _systems.Add(system);
        }

        public void Update(float deltaTime, List<IComponent> commands)
        {
            foreach (ISystem system in _systems)
            {
                system.Update(deltaTime, commands);
            }
        }
    }
}
