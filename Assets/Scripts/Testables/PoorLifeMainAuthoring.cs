using UnityEngine;
using UnityEngine.UI;

namespace EthanKennerly.PoorLife
{
    public class PoorLifeMainAuthoring : MonoBehaviour, IPoorLifeMainAuthoring
    {
        [SerializeField]
        private Transform _parent;
        public Transform Parent { get { return _parent; } }

        [SerializeField]
        private AgeUpAuthoring _ageUp;
        public IAgeUpAuthoring AgeUp { get { return _ageUp; } }

        [SerializeField]
        private GameObject _deathPopupPrefab;
        public GameObject DeathPopupPrefab { get { return _deathPopupPrefab; } }

        private PoorLifeMain _poorLife;

        private void Awake()
        {
            _poorLife = new PoorLifeMain(this);
        }

        private void LateUpdate()
        {
            if (_poorLife != null)
            {
                _poorLife.Update(Time.deltaTime);
            }
        }

        private void OnDestroy()
        {
            if (_poorLife != null)
            {
                _poorLife.Dispose();
                _poorLife = null;
            }
        }
    }
}
