using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace EthanKennerly.SaveLife
{
    public class AgeUpAuthoring_uGUI : MonoBehaviour, IAgeUpAuthoring
    {
        [SerializeField]
        private Button _ageUpButton;

        [SerializeField]
        private TMP_Text _text;

        [SerializeField]
        private ScrollRect _scrollRect;

        [SerializeField]
        private RectTransform _contentRectTransform;

        [SerializeField]
        private RectTransform _viewportRectTransform;

        [SerializeField]
        private HealthAuthoring _health;
        public IHealthAuthoring Health => _health;

        [SerializeField]
        private AilmentAuthoring _ailment;
        public IAilmentAuthoring Ailment => _ailment;

        private void Awake()
        {
            _text.text = string.Empty;
        }

        public void AgeUpClicked(SimpleCallback onClick)
        {
            _ageUpButton.onClick.AddListener(() => onClick.Invoke());
        }

        public void AppendToLifeLog(string logText)
        {
            if (_text == null)
            {
                return;
            }

            _text.text += logText;
            ScrollToBottomIfContentExceedsViewport();
        }

        /// <remarks>
        /// Force a layout rebuild to get the most accurate sizes.
        /// </remarks>
        private void ScrollToBottomIfContentExceedsViewport()
        {
            LayoutRebuilder.ForceRebuildLayoutImmediate(_contentRectTransform);
            float contentHeight = _contentRectTransform.sizeDelta.y;
            float viewportHeight = _viewportRectTransform.sizeDelta.y;

            if (contentHeight > viewportHeight)
            {
                _scrollRect.verticalNormalizedPosition = 0f;
            }
        }
    }
}
