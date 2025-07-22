namespace EthanKennerly.PoorLife
{
    public class HealthComponent : IComponent
    {
        public int HealthPercent;
        public int HealthPerYear;
        public int HealthPerYearPerYear;
        public int HealthPerYearPerYearPerYear;
        public IHealthAuthoring Authoring;

        public HealthComponent(IHealthAuthoring authoring)
        {
            HealthPercent = 5;
            HealthPerYearPerYear = 7;
            HealthPerYearPerYearPerYear = -1;
            Authoring = authoring;
        }
    }
}
