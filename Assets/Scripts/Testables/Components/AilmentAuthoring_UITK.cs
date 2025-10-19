using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace EthanKennerly.SaveLife
{
    [Serializable]
    public class AilmentAuthoring_UITK : IAilmentAuthoring
    {
        [SerializeField]
        private UIDocument _uiDocument;
        private VisualElement _root;

        [SerializeField]
        private string _ailmentPopupName = "AilmentPopup";
        private VisualElement _ailmentPopup;

        [SerializeField]
        private string _okButtonName = "OkButton";

        public void SetUpPopup()
        {
            _root = _uiDocument.rootVisualElement;
            _ailmentPopup = _root.Q<VisualElement>(_ailmentPopupName);
            Button button = _ailmentPopup.Q<Button>(_okButtonName);
            button.clicked += HidePopup;

            HidePopup();
        }

        public void ShowPopup()
        {
            if (_ailmentPopup == null)
            {
                return;
            }

            _ailmentPopup.visible = true;
        }

        private void HidePopup()
        {
            if (_ailmentPopup == null)
            {
                return;
            }

            _ailmentPopup.visible = false;
        }
    }
}
