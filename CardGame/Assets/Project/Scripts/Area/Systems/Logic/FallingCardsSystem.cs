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
            bool isCardsMovingVertical = false;
            bool isCardsMovingHorizontal = false;

            var fields = _entityManager.GetEntitiesOfGroup(_fields);
            foreach (var field in fields)
            {
                var fieldComponent = (FieldComponent)field.GetComponent(typeof(FieldComponent));

                MoveCardsRight(ref isCardsMovingHorizontal, fieldComponent);
                if (!isCardsMovingHorizontal)
                {
                    MoveCardsLeft(ref isCardsMovingHorizontal, fieldComponent);
                }

                if (!isCardsMovingHorizontal)
                {
                    MoveCardsDown(ref isCardsMovingVertical, fieldComponent);
                }

                if (!isCardsMovingHorizontal & !isCardsMovingVertical)
                {
                    MoveCardsUp(ref isCardsMovingVertical, fieldComponent);
                }
            }
        }

        private void MoveCardsDown(ref bool isCardsMovingDown, FieldComponent fieldComponent)
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

        private void MoveCardsRight(ref bool isCardsMovingRight, FieldComponent fieldComponent)
        {
            for (int y = 0; y <= fieldComponent.MaxPositionY; y++)
            {
                bool shouldCardsMoveRight = false;
                for (int x = fieldComponent.MaxPositionX - 1; x >= 0; x--)
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

        private void MoveCardsLeft(ref bool isCardsMovingLeft, FieldComponent fieldComponent)
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

        private void MoveCardsUp(ref bool isCardsMovingDown, FieldComponent fieldComponent)
        {
            for (int x = 0; x <= fieldComponent.MaxPositionX; x++)
            {
                bool shouldCardsAboveFall = false;
                for (int y = fieldComponent.MaxPositionY - 1; y >= 0; y--)
                {
                    if (fieldComponent.PositionsWithCard[x, y] != null)
                    {
                        var fallingCardComponent = (FallingComponent)fieldComponent.PositionsWithCard[x, y]
                            .GetComponent(typeof(FallingComponent));
                        if (fallingCardComponent != null)
                        {
                            if (fieldComponent.PositionsWithCard[x, y + 1] == null)
                            {
                                shouldCardsAboveFall = true;
                                isCardsMovingDown = true;
                            }

                            if (shouldCardsAboveFall)
                            {
                                int2 nextCardPosition =
                                    new int2(x - math.abs(fieldComponent.MinRelativeCenterPositionX),
                                        y - math.abs(fieldComponent.MinRelativeCenterPositionY) + 1);
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