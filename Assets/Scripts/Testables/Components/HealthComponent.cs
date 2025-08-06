namespace EthanKennerly.SaveLife
{
    public class HealthComponent : IComponent
    {
        public int HealthPercent;
        public int HealthPerYear;
        public int PeakAge;
        public IHealthAuthoring Authoring;

        public HealthComponent(IHealthAuthoring authoring)
        {
            HealthPercent = 5;
            HealthPerYear = 4;
            PeakAge = 20;
            Authoring = authoring;
        }
    }
}
