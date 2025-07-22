using UnityEngine;
using UnityEngine.UI;

namespace EthanKennerly.PoorLife
{
    public interface IAilmentAuthoring
    {
        GameObject PopupInstance { get; }
        Button CloseButton { get; }
    }
}
