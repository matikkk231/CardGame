using System;
using System.Collections.Generic;
using Project.Scripts.Area.Components.Logic;
using Project.Scripts.Core.ECS.Entity;
using Project.Scripts.Core.ECS.System;
using Unity.Mathematics;

namespace Project.Scripts.Area.Systems.Logic
{
    public class FallingCardsSystem : ISystem
    {
        private IEntityManager _entityManager;
        private List<Type> _fields;

        public FallingCardsSystem(IEntityManager entityManager)
        {
            _entityManager = entityManager;
            _fields = new List<Type>();
            _fields.Add(typeof(FieldComponent));
        }

        public void Execute()
        {
            bool isCardsMovingDown = false;
            bool isCardsMovingRight = false;
            bool isCardsMovingLeft = false;

            var fields = _entityManager.GetEntitiesOfGroup(_fields);
            foreach (var field in fields)
            {
                var fieldComponent = (FieldComponent)field.GetComponent(typeof(FieldComponent));

                MoveCardsRight(ref isCardsMovingRight, fieldComponent);
                MoveCardsDown(ref isCardsMovingRight, ref isCardsMovingLeft, fieldComponent);
                MoveCardsLeft(ref isCardsMovingRight, ref isCardsMovingLeft, ref isCardsMovingDown, fieldComponent);
            }
        }

        private void MoveCardsLeft(ref bool isCardsMovingRight, ref bool isCardsMovingLeft, ref bool isCardsMovingDown, FieldComponent fieldComponent)
        {
            if (isCardsMovingRight == false & isCardsMovingLeft == false)
            {
                for (int x = 0; x <= fieldComponent.MaxPositionX; x++)
                {
                    bool shouldCardsAboveFall = false;
                    for (int y = 1; y <= fieldComponent.MaxPositionY; y++)
                    {
                        if (fieldComponent.PositionsWithCard[x, y] != null)
                        {
                            var fallingCardComponent = (FallingComponent)fieldComponent.PositionsWithCard[x, y]
                                .GetComponent(typeof(FallingComponent));
                            if (fallingCardComponent != null)
                            {
                                if (fieldComponent.PositionsWithCard[x, y - 1] == null)
                                {
                                    shouldCardsAboveFall = true;
                                    isCardsMovingDown = true;
                                }

                                if (shouldCardsAboveFall)
                                {
                                    int2 nextCardPosition =
                                        new int2(x - math.abs(fieldComponent.MinRelativeCenterPositionX),
                                            y - math.abs(fieldComponent.MinRelativeCenterPositionY) - 1);
                                    fieldComponent.PositionsWithCard[x, y]
                                        .AddComponent(new MovementOnFieldComponent(nextCardPosition));
                                }
                            }
                        }
                    }
                }
            }
        }

        private void MoveCardsRight(ref bool isCardsMovingRight, FieldComponent fieldComponent)
        {
            for (int y = 0; y <= fieldComponent.MaxPositionY; y++)
            {
                bool shouldCardsMoveRight = false;
                for (int x = 0; x < fieldComponent.MaxPositionX; x++)
                {
                    if (fieldComponent.PositionsWithCard[x, y] != null)
                    {
                        var fallingCardComponent = (FallingComponent)fieldComponent.PositionsWithCard[x, y]
                            .GetComponent(typeof(FallingComponent));
                        if (fallingCardComponent != null)
                        {
                            if (fieldComponent.PositionsWithCard[x + 1, y] == null)
                            {
                                shouldCardsMoveRight = true;
                                isCardsMovingRight = true;
                            }

                            if (shouldCardsMoveRight)
                            {
                                int2 nextCardPosition =
                                    new int2(x - math.abs(fieldComponent.MinRelativeCenterPositionX) + 1,
                                        y - math.abs(fieldComponent.MinRelativeCenterPositionY));
                                fieldComponent.PositionsWithCard[x, y]
                                    .AddComponent(new MovementOnFieldComponent(nextCardPosition));
                            }
                        }
                    }
                }
            }
        }

        private void MoveCardsDown(ref bool isCardsMovingRight, ref bool isCardsMovingLeft, FieldComponent fieldComponent)
        {
            if (isCardsMovingRight == false)
            {
                for (int y = 0; y <= fieldComponent.MaxPositionY; y++)
                {
                    bool shouldCardsMoveLeft = false;
                    for (int x = 1; x <= fieldComponent.MaxPositionX; x++)
                    {
                        if (fieldComponent.PositionsWithCard[x, y] != null)
                        {
                            var fallingCardComponent = (FallingComponent)fieldComponent.PositionsWithCard[x, y]
                                .GetComponent(typeof(FallingComponent));
                            if (fallingCardComponent != null)
                            {
                                if (fieldComponent.PositionsWithCard[x - 1, y] == null)
                                {
                                    shouldCardsMoveLeft = true;
                                    isCardsMovingLeft = true;
                                }

                                if (shouldCardsMoveLeft)
                                {
                                    int2 nextCardPosition =
                                        new int2(x - math.abs(fieldComponent.MinRelativeCenterPositionX) - 1,
                                            y - math.abs(fieldComponent.MinRelativeCenterPositionY));
                                    fieldComponent.PositionsWithCard[x, y]
                                        .AddComponent(new MovementOnFieldComponent(nextCardPosition));
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}