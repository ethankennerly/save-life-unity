using UnityEngine;
using UnityEngine.UI;

namespace EthanKennerly.PoorLife
{
    public class PoorLifeMainAuthoring : MonoBehaviour, IPoorLifeMainAuthoring
    {
        [SerializeField]
        private Transform _parent;
        public Transform Parent => _parent;

        [SerializeField]
        private AgeUpAuthoring _ageUp;
        public IAgeUpAuthoring AgeUp => _ageUp;

        [SerializeField]
        private GameObject _deathPopupPrefab;
        public GameObject DeathPopupPrefab => _deathPopupPrefab;

        [SerializeField]
        TouchRecordingAuthoring _touchRecording;
        public ITouchRecordingAuthoring TouchRecording => _touchRecording;

        [SerializeField]
        TouchReplayAuthoring _touchReplay;
        public ITouchReplayAuthoring TouchReplay => _touchReplay;

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
