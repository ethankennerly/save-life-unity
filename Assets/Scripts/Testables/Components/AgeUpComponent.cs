using TMPro;

namespace EthanKennerly.PoorLife
{
    public class AgeUpComponent : step_log, IComponent
    {
        public bool WasBorn;
        public int Age;
        public string Text;
        public IAgeUpAuthoring Authoring;

        public AgeUpComponent()
        {
            log_step = LogStep.AgeUpClicked;
        }
    }
}
