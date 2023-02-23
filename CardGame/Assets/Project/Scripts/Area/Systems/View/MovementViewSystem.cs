using System;
using System.Collections.Generic;
using Project.Scripts.Area.Components.View;
using Project.Scripts.Area.Components.View.GameObjectComponent;
using Project.Scripts.Core.ECS.Entity;
using Project.Scripts.Core.ECS.System;
using Unity.Mathematics;
using UnityEngine;

namespace Project.Scripts.Area.Systems.Logic.View
{
    public class MovementViewSystem : ISystem
    {
        private readonly IEntityManager _entityManager;
        private readonly List<Type> _movingEntities;
        private readonly float _lerpTime;

        public MovementViewSystem(IEntityManager entityManager)
        {
            _entityManager = entityManager;
            _movingEntities = new List<Type>();
            _movingEntities.Add(typeof(NeedMovingViewComponent));
            _lerpTime = 0.2f;
        }

        public void Execute()
        {
            var movingEntities = _entityManager.GetEntitiesOfGroup(_movingEntities);
            foreach (var movingEntity in movingEntities)
            {
                var gameObjectComponent = (GameObjectComponent)movingEntity.GetComponent(typeof(GameObjectComponent));
                var needMovingViewComponent = (NeedMovingViewComponent)movingEntity.GetComponent(typeof(NeedMovingViewComponent));
                var positionNeedAchieve = needMovingViewComponent.WhereShouldBeMoved;

                needMovingViewComponent.PercentageOfWentDistance += _lerpTime * Time.deltaTime;
                if (needMovingViewComponent.PercentageOfWentDistance > 0.9f)
                {
                    needMovingViewComponent.PercentageOfWentDistance = 1f;
                }

                gameObjectComponent.GameObject.transform.position =
                    Vector3.Lerp(gameObjectComponent.GameObject.transform.position, positionNeedAchieve, needMovingViewComponent.PercentageOfWentDistance);

                if (positionNeedAchieve == gameObjectComponent.GameObject.transform.position)
                {
                    movingEntity.RemoveComponent(typeof(NeedMovingViewComponent));
                }
            }
        }
    }
}