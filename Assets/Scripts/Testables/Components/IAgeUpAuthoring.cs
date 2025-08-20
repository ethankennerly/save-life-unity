using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EthanKennerly.SaveLife
{
    public interface IAgeUpAuthoring
    {
        IHealthAuthoring Health { get; }
        IAilmentAuthoring Ailment { get; }
        void AgeUpClicked(SimpleCallback onClick);
        void AppendToLifeLog(string logText);
    }
}
