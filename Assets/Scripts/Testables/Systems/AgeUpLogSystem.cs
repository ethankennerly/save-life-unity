using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EthanKennerly.PoorLife
{
    public class AgeUpLogSystem : ISystem
    {
        public void Update(float deltaTime, List<IComponent> commands)
        {
            foreach (IComponent command in commands)
            {
                if (command is AgeUpComponent ageUp)
                {
                    if (!ageUp.WasBorn)
                    {
                        ageUp.WasBorn = true;
                        ageUp.Text = $@"<b>Age: {ageUp.Age} years</b>
I was born a female in Nasarawa, Nigeria. I was the result of a contraceptive-free marriage.

My birthday is July 21.

My name is Anika Ibrahim.
My mother is Ifamma Ibrahim, a farmer (age 17).
My father is Asha Ibrahim, a farmer (age 16).";

                        var onClick = ageUp.Authoring.AgeUpButton.onClick;
                        onClick.RemoveAllListeners();
                        onClick.AddListener(() => commands.Add(ageUp));
                    }
                    else
                    {
                        ageUp.Age++;
                        ageUp.Text += $"\n\n<b>Age: {ageUp.Age} years</b>";
                    }

                    ageUp.Authoring.Text.text = ageUp.Text;
                    ScrollToBottomIfContentExceedsViewport(ageUp.Authoring);
                }
            }
        }

        /// <remarks>
        /// Force a layout rebuild to get the most accurate sizes.
        /// </remarks>
        private void ScrollToBottomIfContentExceedsViewport(IAgeUpAuthoring ageUp)
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(ageUp.ContentRectTransform);
            float contentHeight = ageUp.ContentRectTransform.sizeDelta.y;
            float viewportHeight = ageUp.ViewportRectTransform.sizeDelta.y;

            if (contentHeight > viewportHeight)
            {
                ageUp.ScrollRect.verticalNormalizedPosition = 0f;
            }
        }
    }
}
