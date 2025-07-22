using TMPro;
using UnityEngine.UI;

namespace EthanKennerly.PoorLife
{
    public interface IHealthAuthoring
    {
        TMP_Text HealthPercentText { get; }
        Image FillImage { get; }
    }
}
