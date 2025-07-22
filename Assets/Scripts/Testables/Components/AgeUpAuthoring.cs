using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EthanKennerly.PoorLife
{
    public class AgeUpAuthoring : MonoBehaviour, IAgeUpAuthoring
    {
        [SerializeField]
        private Button _ageUpButton;
        public Button AgeUpButton { get { return _ageUpButton; } }

        [SerializeField]
        private TMP_Text _text;
        public TMP_Text Text { get { return _text; } }

        [SerializeField]
        private ScrollRect _scrollRect;
        public ScrollRect ScrollRect { get { return _scrollRect; } }

        [SerializeField]
        private RectTransform _contentRectTransform;
        public RectTransform ContentRectTransform { get { return _contentRectTransform; } }

        [SerializeField]
        private RectTransform _viewportRectTransform;
        public RectTransform ViewportRectTransform { get { return _viewportRectTransform; } }

        [SerializeField]
        private HealthAuthoring _health;
        public IHealthAuthoring Health { get { return _health; } }

        [SerializeField]
        private AilmentAuthoring _ailment;
        public IAilmentAuthoring Ailment { get { return _ailment; } }
    }
}
