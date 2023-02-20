using Project.Scripts.Core.ECS.Component;
using Unity.Mathematics;

namespace Project.Scripts.Area.Components.Logic
{
    public class PositionRelativeFieldCenterComponent : IComponent
    {
        public int2 CurrentPosition;

        public PositionRelativeFieldCenterComponent(int2 currentPosition)
        {
            CurrentPosition = currentPosition;
        }
    }
}