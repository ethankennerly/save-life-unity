using UnityEngine;
using UnityEngine.UI;

namespace EthanKennerly.SaveLife
{
    public class AilmentAuthoring_uGUI : MonoBehaviour, IAilmentAuthoring
    {
        [SerializeField]
        private GameObject _PopupInstance;

        [SerializeField]
        private Button _closeButton;

        public void SetUpPopup()
        {
            HidePopup();
            var onClick = _closeButton.onClick;
            onClick.RemoveAllListeners();
            onClick.AddListener(HidePopup);
        }

        public void ShowPopup()
        {
            if (_PopupInstance == null)
            {
                return;
            }
            
            _PopupInstance.SetActive(true);
        }

        private void HidePopup()
        {
            if (_PopupInstance == null)
            {
                return;
            }

            _PopupInstance.SetActive(false);
        }
    }
}
