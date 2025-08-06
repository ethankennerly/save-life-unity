using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EthanKennerly.SaveLife
{
    public interface IAgeUpAuthoring
    {
        Button AgeUpButton { get; }
        TMP_Text Text { get; }
        ScrollRect ScrollRect { get; }
        RectTransform ContentRectTransform { get; }
        RectTransform ViewportRectTransform { get; }
        IHealthAuthoring Health { get; }
        IAilmentAuthoring Ailment { get; }
    }
}
