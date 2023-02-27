using Project.Scripts.Core.ECS.Component;

namespace Project.Scripts.Area.Components.Logic
{
    public class ToxicStatusComponent : IComponent
    {
        public int PoisoningDuration;
        public int PoisoningForce;

        public ToxicStatusComponent(int duration, int poisoningForce)
        {
            PoisoningDuration = duration;
            PoisoningForce = poisoningForce;
        }
    }
}