using UnityEngine;
using UnityEngine.UI;

namespace EthanKennerly.SaveLife
{
    public interface IAilmentAuthoring
    {
        GameObject PopupInstance { get; }
        Button CloseButton { get; }
    }
}
