using UnityEngine;
using UnityEngine.UI;

namespace EthanKennerly.PoorLife
{
    public interface IPoorLifeMainAuthoring
    {
        Transform Parent { get; }
        IAgeUpAuthoring AgeUp { get; }
        GameObject DeathPopupPrefab { get; }
        ITouchReplayAuthoring TouchReplay { get; }
    }
}
