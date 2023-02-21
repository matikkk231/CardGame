using System;
using System.Collections.Generic;
using Project.Scripts.Area.Components.Logic;
using Project.Scripts.Core.ECS.Entity;
using Project.Scripts.Core.ECS.System;

namespace Project.Scripts.Area.Systems.Logic
{
    public class InteractingWithEmptyCardsSystem : ISystem
    {
        private readonly IEntityManager _entityManager;
        private readonly List<Type> _groupOfEmptyCardComponents;
        private readonly List<Type> _groupOfPlayerCardComponents;
        private readonly List<Type> _groupOfInteractableCards;
        private readonly List<Type> _fields;

        public InteractingWithEmptyCardsSystem(IEntityManager entityManager)
        {
            _entityManager = entityManager;
            _groupOfEmptyCardComponents = new List<Type>();
            _groupOfEmptyCardComponents.Add(typeof(PositionRelativeFieldCenterComponent));
            _groupOfEmptyCardComponents.Add(typeof(EmptyCardComponent));
            _groupOfEmptyCardComponents.Add(typeof(InteractProcessingComponent));
            _groupOfPlayerCardComponents = new List<Type>();
            _groupOfPlayerCardComponents.Add(typeof(PlayerCardComponent));
            _groupOfInteractableCards = new List<Type>();
            _groupOfInteractableCards.Add(typeof(InteractableComponent));
            _fields = new List<Type>();
            _fields.Add(typeof(FieldComponent));
        }

        public void Execute()
        {
            var emptyCards = _entityManager.GetEntitiesOfGroup(_groupOfEmptyCardComponents);
            var playerCards = _entityManager.GetEntitiesOfGroup(_groupOfPlayerCardComponents);
            var interactableCards = _entityManager.GetEntitiesOfGroup(_groupOfInteractableCards);

            foreach (var emptyCard in emptyCards)
            {
                emptyCard.AddComponent(new NeedToBeDestroyedComponent());
                var nextPositionOfPlayerCard =
                    (PositionRelativeFieldCenterComponent)emptyCard.GetComponent(
                        typeof(PositionRelativeFieldCenterComponent));
                emptyCard.RemoveComponent(typeof(InteractProcessingComponent));
                emptyCard.AddComponent(new NeedToBeDestroyedComponent());
                foreach (var playerCard in playerCards)
                {
                    playerCard.AddComponent(new MovementOnFieldComponent(nextPositionOfPlayerCard.CurrentPosition));
                }

                foreach (var interactableCard in interactableCards)
                {
                    interactableCard.RemoveComponent(typeof(InteractableComponent));
                }
                
            }

            var fields = _entityManager.GetEntitiesOfGroup(_fields);
            
            foreach (var field in fields)
            {
                field.AddComponent(new TurnDoneComponent());
            }
        }
    }
}