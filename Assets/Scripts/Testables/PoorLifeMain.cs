using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace EthanKennerly.PoorLife
{
    public class PoorLifeMain : IDisposable
    {
        private readonly IPoorLifeMainAuthoring _authoring;
        private readonly List<IComponent> _commands;
        private readonly MainSystem _mainSystem;
        private float _elapsedTime;
        private AgeUpComponent _ageUp;

        public PoorLifeMain(IPoorLifeMainAuthoring authoring)
        {
            _authoring = authoring;
            _commands = new List<IComponent>();
            _mainSystem = new MainSystem(authoring);
        }

        public void Update(float deltaTime)
        {
            _elapsedTime += deltaTime;

            if (_ageUp == null)
            {
                _ageUp = new AgeUpComponent(_authoring.AgeUp);
                _commands.Add(_ageUp);
            }

            _mainSystem.Update(deltaTime, _commands);

            _commands.Clear();
        }

        public void Dispose()
        {
            _ageUp = null;
        }
    }
}
