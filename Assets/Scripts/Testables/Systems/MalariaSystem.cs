using System.Collections.Generic;
using UnityEngine;

namespace EthanKennerly.PoorLife
{
    public class MalariaSystem : ISystem
    {
        public void Update(float deltaTime, List<step_log> commands)
        {
            foreach (step_log command in commands)
            {
                if (command is AgeUpComponent ageUp && ageUp.Age > 0)
                {
                    HealthComponent health = ageUp.Health;
                    if (Random.value < 0.1f)
                    {
                        health.HealthPercent -= 10;
                        ageUp.Text += "\n\nI got malaria.";
                    }
                }
            }
        }
    }
}
