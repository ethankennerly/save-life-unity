using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

using Button = UnityEngine.UIElements.Button;

namespace EthanKennerly.SaveLife
{
    public class AgeUpAuthoring_UITK : MonoBehaviour, IAgeUpAuthoring
    {
        [SerializeField]
        private UIDocument _uiDocument;
        [SerializeField]
        private string _ageUpButtonName = "AgeUpButton";

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
        private AilmentAuthoring_UITK _ailment;
        public IAilmentAuthoring Ailment => _ailment;

        public void AgeUpClicked(SimpleCallback onClick)
        {
            VisualElement root = _uiDocument.rootVisualElement;
            Button button = root.Q<Button>(_ageUpButtonName);
            button.clicked += onClick.Invoke;
        }

        public void AppendToLifeLog(string logText)
        {
            VisualElement root = _uiDocument.rootVisualElement;

            ScrollView scroll = root.Q<ScrollView>("LifeLogScroll");
            if (scroll == null)
            {
                Debug.LogError("LifeLogScroll not found");
                return;
            }

            Label label = new Label(logText)
            {
                pickingMode = PickingMode.Ignore
            };
            label.style.whiteSpace = WhiteSpace.Normal;
            label.style.marginBottom = 4;

            scroll.contentContainer.Add(label);

            // Ensure bottom of the new (possibly tall) label is fully in view.
            // Do this on the next layout pass so sizes are updated.
            scroll.schedule.Execute(() =>
            {
                ScrollToBottomIfContentTooTall(scroll);
            });
        }

        private void ScrollToBottomIfContentTooTall(ScrollView scroll)
        {
            float contentHeight  = scroll.contentContainer.resolvedStyle.height;
            float viewportHeight = scroll.contentViewport.resolvedStyle.height;
            if (contentHeight <= viewportHeight)
            {
                return;
            }

            var vs = scroll.verticalScroller;
            if (vs == null)
            {
                return;
            }
            vs.value = vs.highValue;
        }
    }
}
