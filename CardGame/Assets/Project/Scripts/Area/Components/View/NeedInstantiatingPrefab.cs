using Project.Scripts.Area.Components.View.GameObjectComponent;
using Project.Scripts.Core.ECS.Component;
using Unity.Mathematics;

namespace Project.Scripts.Area.Components.Logic
{
    public class NeedInstantiatingPrefab : IComponent
    {
        public PrefabTypesId PrefabTypeId;
        public int2 PositionWhereShouldBeInstantiated;

        public NeedInstantiatingPrefab(PrefabTypesId prefabTypeId, int2 positionWhereShouldBeInstantiated)
        {
            PrefabTypeId = prefabTypeId;
            PositionWhereShouldBeInstantiated = positionWhereShouldBeInstantiated;
        }
    }
}