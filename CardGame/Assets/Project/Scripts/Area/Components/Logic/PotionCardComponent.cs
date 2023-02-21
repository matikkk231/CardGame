using Project.Scripts.Core.ECS.Component;

namespace Project.Scripts.Area.Components.Logic
{
    public class PotionCardComponent : IComponent
    {
        public int ImpactForce;
        public int ImpactDuration;

        public string TypeOfPotion;

        public PotionCardComponent(int impactDuration, int impactForce, string typeOfPotion)
        {
            ImpactForce = impactForce;
            ImpactDuration = impactDuration;
            TypeOfPotion = typeOfPotion;
        }
    }
}