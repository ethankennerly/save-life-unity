using UnityEngine;
using UnityEngine.UI;

namespace EthanKennerly.PoorLife
{
    public class PoorLifeMainAuthoring : MonoBehaviour, IPoorLifeMainAuthoring
    {
        [SerializeField]
        private Button _ageUpButton;
        public Button AgeUpButton { get { return _ageUpButton; } }

        private PoorLifeMain _poorLife;

        private void Awake()
        {
            _poorLife = new PoorLifeMain(this);
        }

        private void LateUpdate()
        {
            _poorLife.Update(Time.deltaTime);
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
