using System;
using System.Collections.Generic;
using Project.Scripts.Area.Components.Logic;
using Project.Scripts.Core.ECS.Entity;
using Project.Scripts.Core.ECS.System;
using Unity.Mathematics;

namespace Project.Scripts.Area.Systems.Logic
{
    public class FieldManagerSystem : ISystem
    {
        private readonly IEntityManager _entityManager;
        private readonly List<Type> _componentsOfField;
        private readonly List<Type> _positionRelativeCenterField;

        public FieldManagerSystem(IEntityManager entityManager)
        {
            _entityManager = entityManager;
            _componentsOfField = new List<Type> { typeof(FieldComponent) };
            _positionRelativeCenterField = new List<Type>();
            _positionRelativeCenterField.Add(typeof(PositionRelativeFieldCenterComponent));
        }

        public void Execute()
        {
            var fields = _entityManager.GetEntitiesOfGroup(_componentsOfField);
            var cards = _entityManager.GetEntitiesOfGroup(_positionRelativeCenterField);
            foreach (var field in fields)
            {
                var fieldComponent = (FieldComponent)field.GetComponent(typeof(FieldComponent));
                for (int y = 0; y <= fieldComponent.MaxPositionY; y++)
                {
                    for (int x = 0; x <= fieldComponent.MaxPositionX; x++)
                    {
                        fieldComponent.PositionsWithCard[x, y] = null;
                    }
                }


                foreach (var card in cards)
                {
                    var cardPosition =
                        (PositionRelativeFieldCenterComponent)card.GetComponent(
                            typeof(PositionRelativeFieldCenterComponent));

                    fieldComponent.PositionsWithCard[
                        cardPosition.CurrentPosition.x + Math.Abs(fieldComponent.MinRelativeCenterPositionX),
                        cardPosition.CurrentPosition.y + Math.Abs(fieldComponent.MinRelativeCenterPositionY)] = card;
                }
            }
        }
    }
}