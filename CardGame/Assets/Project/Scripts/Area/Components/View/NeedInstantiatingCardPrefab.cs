using Project.Scripts.Area.Components.View.GameObjectComponent;
using Project.Scripts.Core.ECS.Component;
using Unity.Mathematics;
using UnityEngine;

namespace Project.Scripts.Area.Components.Logic
{
    public class NeedInstantiatingCardPrefab : IComponent
    {
        public PrefabTypesId PrefabTypeId;
        public int2 PositionWhereShouldBeInstantiated;
        public Sprite CardContent;

        public NeedInstantiatingCardPrefab(PrefabTypesId prefabTypeId, int2 positionWhereShouldBeInstantiated,
            Sprite cardContent)
        {
            PrefabTypeId = prefabTypeId;
            PositionWhereShouldBeInstantiated = positionWhereShouldBeInstantiated;
            CardContent = cardContent;
        }
    }
}