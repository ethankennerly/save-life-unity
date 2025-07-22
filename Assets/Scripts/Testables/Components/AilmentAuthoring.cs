using UnityEngine;
using UnityEngine.UI;

namespace EthanKennerly.PoorLife
{
    public class AilmentAuthoring : MonoBehaviour, IAilmentAuthoring
    {
        [SerializeField]
        private GameObject _PopupInstance;
        public GameObject PopupInstance { get { return _PopupInstance; } }

        [SerializeField]
        private Button _closeButton;
        public Button CloseButton { get { return _closeButton; } }
    }
}
