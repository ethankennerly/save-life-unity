namespace EthanKennerly.PoorLife
{
    public class AgeUpComponent : step_log, IComponent
    {
        public readonly IAgeUpAuthoring Authoring;
        public readonly HealthComponent Health;
        public bool WasBorn;
        public int Age;
        public string Text;

        public AgeUpComponent(IAgeUpAuthoring authoring)
        {
            log_step = LogStep.AgeUpClicked;
            Authoring = authoring;
            Health = new HealthComponent(authoring.Health);
        }
    }
}
