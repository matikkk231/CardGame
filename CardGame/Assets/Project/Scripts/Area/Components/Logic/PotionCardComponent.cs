using Project.Scripts.Core.ECS.Component;

namespace Project.Scripts.Area.Components.Logic
{
    public class PotionCardComponent : IComponent
    {
        public int ImpactForce;
        public int ImpactDuration;

        public string ProvidingTypeEffect;

        public PotionCardComponent(int impactDuration, int impactForce, string providingTypeEffect)
        {
            ImpactForce = impactForce;
            ImpactDuration = impactDuration;
            ProvidingTypeEffect = providingTypeEffect;
        }
    }
}