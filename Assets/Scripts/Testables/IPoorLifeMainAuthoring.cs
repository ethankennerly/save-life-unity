using UnityEngine;
using UnityEngine.UI;

namespace EthanKennerly.PoorLife
{
    public interface IPoorLifeMainAuthoring
    {
        Transform Parent { get; }
        Button AgeUpButton { get; }
        GameObject DeathPopupPrefab { get; }
    }
}
