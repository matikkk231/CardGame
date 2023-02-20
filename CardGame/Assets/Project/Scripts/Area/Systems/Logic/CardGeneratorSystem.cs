using System;
using System.Collections.Generic;
using Project.Scripts.Area.Components.Logic;
using Project.Scripts.Area.Components.View.GameObjectComponent;
using Project.Scripts.Core.ECS.Entity;
using Project.Scripts.Core.ECS.System;
using Unity.Mathematics;

namespace Project.Scripts.Area.Systems.Logic
{
    public class CardGeneratorSystem : ISystem
    {
        private readonly IEntityManager _entityManager;
        private readonly List<Type> _fieldComponentType;

        public CardGeneratorSystem(IEntityManager entityManager)
        {
            _entityManager = entityManager;

            _fieldComponentType = new List<Type>();
            _fieldComponentType.Add(typeof(FieldComponent));
        }

        public void Execute()
        {
            var fields = _entityManager.GetEntitiesOfGroup(_fieldComponentType);
            IEntity entityWithFieldComponent = null;
            foreach (var element in fields)
            {
                entityWithFieldComponent = element;
            }

            FieldComponent fieldComponent =
                (FieldComponent)entityWithFieldComponent.GetComponent(typeof(FieldComponent));
            for (int y = 0; y <= fieldComponent.MaxPositionY; y++)
            {
                for (int x = 0; x <= fieldComponent.MaxPositionX; x++)
                {
                    if (fieldComponent.IsThisPositionEmpty[x, y])
                    {
                        var card = _entityManager.CreateEntity();
                        int2 currentPositionRelativeFieldCenter = new int2(
                            x - Math.Abs(fieldComponent.MinRelativeCenterPositionX),
                            y - Math.Abs(fieldComponent.MinRelativeCenterPositionY));
                        card.AddComponent(new PositionRelativeFieldCenterComponent(currentPositionRelativeFieldCenter));
                        int2 positionWhereCardShouldBeInstantiated = new int2(currentPositionRelativeFieldCenter.x * 35,
                            currentPositionRelativeFieldCenter.y * 52);
                        card.AddComponent(new NeedInstantiatingPrefab(PrefabTypesId.Card,
                            positionWhereCardShouldBeInstantiated));
                        fieldComponent.IsThisPositionEmpty[x, y] = false;
                    }
                }
            }
        }
    }
}