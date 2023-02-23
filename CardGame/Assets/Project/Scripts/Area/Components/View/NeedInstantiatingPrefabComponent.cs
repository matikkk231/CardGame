using Project.Scripts.Area.Systems.View.PrefabInstantiator;
using UnityEngine;
using IComponent = Project.Scripts.Core.ECS.Component.IComponent;

namespace Project.Scripts.Area.Components.View
{
    public class NeedInstantiatingPrefabComponent : IComponent
    {
        public PrefabTypeId PrefabTypeId;
        public Vector2 PositionWhereInstantiated;

        public NeedInstantiatingPrefabComponent(PrefabTypeId prefabTypeId, Vector2 positionWhereInstantiated)
        {
            PrefabTypeId = prefabTypeId;
            PositionWhereInstantiated = positionWhereInstantiated;
        }
    }
}