using TMPro;
using UnityEngine.UI;

namespace EthanKennerly.SaveLife
{
    public interface IHealthAuthoring
    {
        TMP_Text HealthPercentText { get; }
        Image FillImage { get; }
    }
}
