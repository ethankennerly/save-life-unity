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
                    if (Random.value < 0.05f)
                    {
                        HealthComponent health = ageUp.Health;
                        health.HealthPercent -= 10;
                        ageUp.Text += "\n\nI got malaria.";
                    }
                }
            }
        }
    }
}
