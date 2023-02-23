using System;
using System.Collections.Generic;
using Project.Scripts.Area.Components.Logic;
using Project.Scripts.Area.Components.Logic.DirectionsOfMovement;
using Project.Scripts.Area.Components.View;
using Project.Scripts.Area.Components.View.GameObjectComponent;
using Project.Scripts.Core.ECS.Component;
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
        private readonly List<Type> _movingCardsHorizontal;
        private readonly List<Type> _movingCardsVertical;
        private readonly float _lerpTime;
        private readonly List<Type> _movingPlayersVertical;
        private readonly List<Type> _movingPlayersHorizontal;

        public MovementViewSystem(IEntityManager entityManager)
        {
            _entityManager = entityManager;
            _movingEntities = new List<Type>();
            _movingEntities.Add(typeof(NeedMovingViewComponent));

            _movingCardsHorizontal = new List<Type>();
            _movingCardsHorizontal.Add(typeof(MoveCardHorizontalComponent));
            _movingCardsVertical = new List<Type>();
            _movingCardsVertical.Add(typeof(MoveCardVerticalComponent));

            _movingPlayersVertical = new List<Type>();
            _movingPlayersVertical.Add(typeof(PlayerCardComponent));
            _movingPlayersVertical.Add(typeof(MoveCardVerticalComponent));
            _movingPlayersHorizontal = new List<Type>();
            _movingPlayersHorizontal.Add(typeof(PlayerCardComponent));
            _movingPlayersHorizontal.Add(typeof(MoveCardHorizontalComponent));


            _lerpTime = 0.2f;
        }

        public void Execute()
        {
            var movingPlayersVertical = _entityManager.GetEntitiesOfGroup(_movingPlayersVertical);
            bool isPlayersMovingVertical = movingPlayersVertical.Count != 0;
            var movingPlayersHorizontal = _entityManager.GetEntitiesOfGroup(_movingPlayersHorizontal);
            bool isPlayersMovingHorizontal = movingPlayersHorizontal.Count != 0;


            if (isPlayersMovingHorizontal | isPlayersMovingVertical)
            {
                MoveCards(movingPlayersHorizontal);
                MoveCards(movingPlayersVertical);
            }

            var cardsMovingVertical = _entityManager.GetEntitiesOfGroup(_movingCardsVertical);
            bool isCardsMovingVertical = cardsMovingVertical.Count != 0;
            if (isCardsMovingVertical & !isPlayersMovingHorizontal)
            {
                MoveCards(cardsMovingVertical);
            }

            var cardsMovingHorizontal = _entityManager.GetEntitiesOfGroup(_movingCardsHorizontal);
            bool isCardsMovingHorizontal = cardsMovingHorizontal.Count != 0;
            if (isCardsMovingHorizontal & !isCardsMovingVertical)
            {
                MoveCards(cardsMovingHorizontal);
            }
        }

        private void MoveCards(List<IEntity> movingEntities)
        {
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
                    movingEntity.RemoveComponent(typeof(MoveCardVerticalComponent));
                    movingEntity.RemoveComponent(typeof(MoveCardHorizontalComponent));
                }
            }
        }
    }
}