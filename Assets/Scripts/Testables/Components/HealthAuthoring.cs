using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EthanKennerly.PoorLife
{
    public class HealthAuthoring : MonoBehaviour, IHealthAuthoring
    {
        [SerializeField]
        private TMP_Text _healthPercentText;
        public TMP_Text HealthPercentText => _healthPercentText;

        [SerializeField]
        private Image _fillImage;
        public Image FillImage => _fillImage;
    }
}
