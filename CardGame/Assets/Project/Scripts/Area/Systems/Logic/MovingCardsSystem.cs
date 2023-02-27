using System;
using System.Collections.Generic;
using Project.Scripts.Area.Components.Logic;
using Project.Scripts.Area.Components.Logic.DirectionsOfMovement;
using Project.Scripts.Area.Components.View;
using Project.Scripts.Core.ECS.Entity;
using Project.Scripts.Core.ECS.System;
using UnityEngine;

namespace Project.Scripts.Area.Systems.Logic
{
    public class MovingCardsSystem : ISystem
    {
        private readonly IEntityManager _entityManager;
        private readonly List<Type> _groupOfComponents;

        public MovingCardsSystem(IEntityManager entityManager)
        {
            _entityManager = entityManager;
            _groupOfComponents = new List<Type>();
            _groupOfComponents.Add(typeof(MovementOnFieldComponent));
        }

        public void Execute()
        {
            var movingCards = _entityManager.GetEntitiesOfGroup(_groupOfComponents);
            foreach (var movingCard in movingCards)
            {
                var movementComponent =
                    (MovementOnFieldComponent)movingCard.GetComponent(typeof(MovementOnFieldComponent));
                var cardsPosition =
                    (PositionRelativeFieldCenterComponent)movingCard.GetComponent(
                        typeof(PositionRelativeFieldCenterComponent));

                bool isCardMovingRight = cardsPosition.CurrentPosition.x < movementComponent.PositionRelativeCenterToMove.x;
                bool isCardMovingLeft = cardsPosition.CurrentPosition.x > movementComponent.PositionRelativeCenterToMove.x;
                bool isCardMovingUp = cardsPosition.CurrentPosition.y < movementComponent.PositionRelativeCenterToMove.y;
                bool isCardMovingDown = cardsPosition.CurrentPosition.y > movementComponent.PositionRelativeCenterToMove.y;
                if (isCardMovingDown)
                {
                    movingCard.AddComponent(new MoveCardVerticalComponent());
                }

                if (isCardMovingUp)
                {
                    movingCard.AddComponent(new MoveCardVerticalComponent());
                }

                if (isCardMovingLeft)
                {
                    movingCard.AddComponent(new MoveCardHorizontalComponent());
                }

                if (isCardMovingRight)
                {
                    movingCard.AddComponent(new MoveCardHorizontalComponent());
                }


                cardsPosition.CurrentPosition = movementComponent.PositionRelativeCenterToMove;


                movingCard.AddComponent(new NeedMovingViewComponent(new Vector3(
                    cardsPosition.CurrentPosition.x * 35, cardsPosition.CurrentPosition.y * 52, 0)));

                movingCard.RemoveComponent(typeof(MovementOnFieldComponent));
            }
        }
    }
}