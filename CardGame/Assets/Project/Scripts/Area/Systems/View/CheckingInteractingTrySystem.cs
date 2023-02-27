using System;
using System.Collections.Generic;
using Project.Scripts.Area.Components.Logic;
using Project.Scripts.Area.Components.View;
using Project.Scripts.Area.Components.View.GameObjectComponent;
using Project.Scripts.Core.ECS.Entity;
using Project.Scripts.Core.ECS.System;
using UnityEngine;

namespace Project.Scripts.Area.Systems.Logic.View
{
    public class CheckingInteractingTrySystem : ISystem
    {
        private readonly IEntityManager _entityManager;
        private readonly List<Type> _groupOfComponents;
        private readonly List<Type> _movingCards;

        public CheckingInteractingTrySystem(IEntityManager entityManager)
        {
            _entityManager = entityManager;
            _groupOfComponents = new List<Type>();
            _groupOfComponents.Add(typeof(GameObjectComponent));
            _groupOfComponents.Add(typeof(CardComponent));

            _movingCards = new List<Type>();
            _movingCards.Add(typeof(NeedMovingViewComponent));
        }

        public void Execute()
        {
            var movingCards = _entityManager.GetEntitiesOfGroup(_movingCards);

            if (movingCards.Count == 0)
            {
                var cards = _entityManager.GetEntitiesOfGroup(_groupOfComponents);
                foreach (var card in cards)
                {
                    var gameObjectComponent = (GameObjectComponent)card.GetComponent(typeof(GameObjectComponent));
                    var cardClicked = (CardClicked)gameObjectComponent.GameObject.GetComponent(typeof(CardClicked));
                    if (cardClicked.IsCardClicked)
                    {
                        var interactableComponent = (InteractableComponent)card.GetComponent(typeof(InteractableComponent));
                        if (interactableComponent != null)
                        {
                            card.AddComponent(new InteractProcessingComponent());
                            card.RemoveComponent(typeof(InteractableComponent));
                            cardClicked.IsCardClicked = false;
                            break;
                        }

                        cardClicked.IsCardClicked = false;
                    }
                }
            }
        }
    }
}