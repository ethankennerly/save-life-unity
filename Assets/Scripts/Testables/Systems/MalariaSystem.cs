using System.Collections.Generic;
using UnityEngine;

namespace EthanKennerly.SaveLife
{
    public class MalariaSystem : ISystem
    {
        public void Update(float deltaTime, List<IComponent> commands)
        {
            foreach (IComponent command in commands)
            {
                if (command is AgeUpComponent ageUp &&
                    ageUp.Authoring != null &&
                    ageUp.Authoring.Ailment != null)
                {
                    if (!ageUp.WasBorn)
                    {
                        ageUp.Authoring.Ailment.SetUpPopup();
                        continue;
                    }

                    HealthComponent health = ageUp.Health;
                    if (Random.value >= 0.125f)
                    {
                        continue;
                    }

                    ageUp.TextToAppend += "\n\nWhile I was sleeping, a mosquito bit me. I am suffering from malaria.";
                    health.HealthPercent -= 10;
                    if (health.HealthPercent <= 0)
                    {
                        continue;
                    }

                    ageUp.Authoring.Ailment.ShowPopup();
                }
            }
        }
    }
}
