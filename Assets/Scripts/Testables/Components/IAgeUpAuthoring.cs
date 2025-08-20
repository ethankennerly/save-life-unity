using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EthanKennerly.SaveLife
{
    public interface IAgeUpAuthoring
    {
        void AgeUpClicked(SimpleCallback onClick);
        TMP_Text Text { get; }
        ScrollRect ScrollRect { get; }
        RectTransform ContentRectTransform { get; }
        RectTransform ViewportRectTransform { get; }
        IHealthAuthoring Health { get; }
        IAilmentAuthoring Ailment { get; }
    }
}
