using Project.Scripts.Core.ECS.Component;
using Vector3 = UnityEngine.Vector3;

namespace Project.Scripts.Area.Components.View
{
    public class NeedMovingViewComponent : IComponent
    {
        public Vector3 WhereShouldBeMoved;
        public float PercentageOfWentDistance;

        public NeedMovingViewComponent(Vector3 whereShouldBeMoved)
        {
            WhereShouldBeMoved = whereShouldBeMoved;

        }
    }
}