namespace EthanKennerly.SaveLife
{
    public class AgeUpComponent : IComponent
    {
        public readonly IAgeUpAuthoring Authoring;
        public readonly HealthComponent Health;
        public bool WasBorn;
        public int Age;
        public string TextToAppend;

        public AgeUpComponent(IAgeUpAuthoring authoring)
        {
            Authoring = authoring;
            Health = new HealthComponent(authoring.Health);
        }
    }
}
