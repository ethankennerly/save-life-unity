using System.Collections.Generic;
using UnityEngine;

namespace EthanKennerly.PoorLife
{
    public class MalariaSystem : ISystem
    {
        public void Update(float deltaTime, List<IComponent> commands)
        {
            foreach (IComponent command in commands)
            {
                if (command is AgeUpComponent ageUp)
                {
                    if (!ageUp.WasBorn)
                    {
                        SetUpPopup(ageUp.Authoring.Ailment);
                        continue;
                    }

                    HealthComponent health = ageUp.Health;
                    if (Random.value < 0.125f)
                    {
                        health.HealthPercent -= 10;
                        ageUp.Text += "\n\nWhile I was sleeping, a mosquito bit me. I am suffering from malaria.";
                        ShowPopup(ageUp.Authoring.Ailment);
                    }
                }
            }
        }

        private void SetUpPopup(IAilmentAuthoring authoring)
        {
            HidePopup(authoring);
            var onClick = authoring.CloseButton.onClick;
            onClick.RemoveAllListeners();
            onClick.AddListener(() => HidePopup(authoring));
        }

        private void HidePopup(IAilmentAuthoring authoring)
        {
            authoring.PopupInstance.SetActive(false);
        }

        private void ShowPopup(IAilmentAuthoring authoring)
        {
            authoring.PopupInstance.SetActive(true);
        }
    }
}
