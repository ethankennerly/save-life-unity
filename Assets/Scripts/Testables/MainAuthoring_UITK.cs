using UnityEngine;
using TouchReplaying;

namespace EthanKennerly.SaveLife
{
    public class MainAuthoring_UITK : MonoBehaviour, IMainAuthoring
    {
        [SerializeField]
        private Transform _parent;
        public Transform Parent => _parent;

        [SerializeField]
        private AgeUpAuthoring_UITK _ageUp;
        public IAgeUpAuthoring AgeUp => _ageUp;

        [SerializeField]
        private GameObject _deathPopupPrefab;
        public GameObject DeathPopupPrefab => _deathPopupPrefab;

        [SerializeField]
        TouchRecordingAuthoring _touchRecording;
        public ITouchRecordingAuthoring TouchRecording => _touchRecording;

        [SerializeField]
        TouchReplayAuthoring_UITK _touchReplay;
        public ITouchReplayAuthoring TouchReplay => _touchReplay;

        private MainController _saveLife;

        private void Awake()
        {
            _saveLife = new MainController(this);
        }

        private void LateUpdate()
        {
            if (_saveLife != null)
            {
                _saveLife.Update(Time.deltaTime);
            }
        }

        private void OnDestroy()
        {
            if (_saveLife != null)
            {
                _saveLife.Dispose();
                _saveLife = null;
            }
        }
    }
}
