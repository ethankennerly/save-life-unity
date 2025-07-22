using System.Collections.Generic;
using UnityEngine;

namespace EthanKennerly.PoorLife
{
    public class DeathPopupSystem : ISystem
    {
        private readonly Transform _parent;
        private readonly GameObject _popupPrefab;
        private GameObject _popup;

        public DeathPopupSystem(IPoorLifeMainAuthoring authoring)
        {
            _parent = authoring.Parent;
            _popupPrefab = authoring.DeathPopupPrefab;
        }

        public void Update(float deltaTime, List<IComponent> commands)
        {
            foreach (IComponent command in commands)
            {
                if (command is AgeUpComponent ageUp && ageUp.Health.HealthPercent <= 0)
                {
                    ShowDeathPopupOnce();
                }
            }
        }

        private void ShowDeathPopupOnce()
        {
            if (_popup == null)
            {
                _popup = Object.Instantiate(_popupPrefab, _parent);
            }
        }
    }
}
