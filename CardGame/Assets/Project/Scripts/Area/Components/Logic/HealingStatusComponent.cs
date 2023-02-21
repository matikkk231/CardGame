using Project.Scripts.Core.ECS.Component;

namespace Project.Scripts.Area.Components.Logic
{
    public class HealingStatusComponent : IComponent
    {
        public int DurationOfHealing;
        public int HealingValue;

        public HealingStatusComponent(int durationOfHealing, int healingValue)
        {
            HealingValue = healingValue;
            DurationOfHealing = durationOfHealing;
        }
    }
}