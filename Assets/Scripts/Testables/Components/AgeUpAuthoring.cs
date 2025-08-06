using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EthanKennerly.SaveLife
{
    public class AgeUpAuthoring : MonoBehaviour, IAgeUpAuthoring
    {
        [SerializeField]
        private Button _ageUpButton;
        public Button AgeUpButton => _ageUpButton;

        [SerializeField]
        private TMP_Text _text;
        public TMP_Text Text => _text;

        [SerializeField]
        private ScrollRect _scrollRect;
        public ScrollRect ScrollRect => _scrollRect;

        [SerializeField]
        private RectTransform _contentRectTransform;
        public RectTransform ContentRectTransform => _contentRectTransform;

        [SerializeField]
        private RectTransform _viewportRectTransform;
        public RectTransform ViewportRectTransform => _viewportRectTransform;

        [SerializeField]
        private HealthAuthoring _health;
        public IHealthAuthoring Health => _health;

        [SerializeField]
        private AilmentAuthoring _ailment;
        public IAilmentAuthoring Ailment => _ailment;
    }
}
