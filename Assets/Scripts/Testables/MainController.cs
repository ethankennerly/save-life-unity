using System;
using System.Collections.Generic;
using UnityEngine.UI;

namespace EthanKennerly.SaveLife
{
    public class MainController : IDisposable
    {
        private readonly IMainAuthoring _authoring;
        private readonly List<IComponent> _commands;
        private readonly MainSystem _mainSystem;
        private float _elapsedTime;
        private AgeUpComponent _ageUp;

        public MainController(IMainAuthoring authoring)
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
