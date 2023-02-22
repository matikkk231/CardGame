using Project.Scripts.Area.Components.View.GameObjectComponent;
using Project.Scripts.Core.ECS.Component;
using Unity.Mathematics;
using UnityEngine;

namespace Project.Scripts.Area.Components.Logic
{
    public class NeedInstantiatingCardPrefabComponent : IComponent
    {
        public CardPrefabTypesId CardPrefabTypeId;
        public int2 PositionWhereShouldBeInstantiated;
        public Sprite CardContent;

        public NeedInstantiatingCardPrefabComponent(CardPrefabTypesId cardPrefabTypeId, int2 positionWhereShouldBeInstantiated,
            Sprite cardContent)
        {
            CardPrefabTypeId = cardPrefabTypeId;
            PositionWhereShouldBeInstantiated = positionWhereShouldBeInstantiated;
            CardContent = cardContent;
        }
    }
}