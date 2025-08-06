using UnityEngine;
using UnityEngine.UI;

namespace EthanKennerly.SaveLife
{
    public class AilmentAuthoring : MonoBehaviour, IAilmentAuthoring
    {
        [SerializeField]
        private GameObject _PopupInstance;
        public GameObject PopupInstance => _PopupInstance;

        [SerializeField]
        private Button _closeButton;
        public Button CloseButton => _closeButton;
    }
}
