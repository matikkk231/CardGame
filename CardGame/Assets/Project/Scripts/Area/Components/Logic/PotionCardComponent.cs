using Project.Scripts.Core.ECS.Component;

namespace Project.Scripts.Area.Components.Logic
{
    public class PotionCardComponent : IComponent
    {
        public int ImpactForce;
        public int ImpactDuration;
        

        public PotionCardComponent(int impactDuration, int impactForce)
        {
            ImpactForce = impactForce;
            ImpactDuration = impactDuration;
           
        }
    }
}