using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EthanKennerly.SaveLife
{
    public class HealthPerYearSystem : ISystem
    {
        public void Update(float deltaTime, List<IComponent> commands)
        {
            foreach (IComponent command in commands)
            {
                if (command is AgeUpComponent ageUp)
                {
                    HealthComponent health = ageUp.Health;
                    health.HealthPercent += health.HealthPerYear;
                    health.HealthPercent = Mathf.Clamp(health.HealthPercent, 0, 100);
                    if (ageUp.Age > health.PeakAge)
                    {
                        health.HealthPerYear = -1;
                    }

                    RefreshHealthBar(health);
                }
            }
        }

        private void RefreshHealthBar(HealthComponent health)
        {
            if (health.Authoring == null)
            {
                return;
            }

            IHealthAuthoring authoring = health.Authoring;
            authoring.HealthPercentText.text = $"{health.HealthPercent}%";
            authoring.FillImage.fillAmount = health.HealthPercent / 100f;
        }
    }
}
