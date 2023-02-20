using Project.Scripts.Core.ECS.Component;
using Unity.Mathematics;

namespace Project.Scripts.Area.Components.Logic
{
    public class MovementOnFieldComponent : IComponent
    {
        public int2 PositionRelativeCenterToMove;

        public MovementOnFieldComponent(int2 positionRelativeCenterToMove)
        {
            PositionRelativeCenterToMove = positionRelativeCenterToMove;
        }
    }
}