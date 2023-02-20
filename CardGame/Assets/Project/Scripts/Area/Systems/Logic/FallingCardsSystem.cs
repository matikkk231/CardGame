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
            var fields = _entityManager.GetEntitiesOfGroup(_fields);
            foreach (var field in fields)
            {
                var fieldComponent = (FieldComponent)field.GetComponent(typeof(FieldComponent));

                for (int x = 0; x <= fieldComponent.MaxPositionX; x++)
                {
                    bool shouldCardsAboveFall = false;
                    for (int y = 1; y <= fieldComponent.MaxPositionY; y++)
                    {
                        FallingComponent fallingCardComponent = null;
                        if (fieldComponent.PositionsWithCard[x, y] != null)
                        {
                            fallingCardComponent = (FallingComponent)fieldComponent.PositionsWithCard[x, y]
                                .GetComponent(typeof(FallingComponent));
                            if (fallingCardComponent != null)
                            {
                                if (fieldComponent.PositionsWithCard[x, y - 1] == null)
                                {
                                    shouldCardsAboveFall = true;
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
    }
}