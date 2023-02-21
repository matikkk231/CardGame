using System;
using System.Collections.Generic;
using Project.Scripts.Area.Components.Logic;
using Project.Scripts.Core.ECS.Entity;
using Project.Scripts.Core.ECS.System;

namespace Project.Scripts.Area.Systems.Logic
{
    public class InteractingWithPotionCardSystem : ISystem
    {
        private readonly IEntityManager _entityManager;
        private readonly List<Type> _potions;
        private readonly List<Type> _players;
        private readonly List<Type> _groupOfInteractableCards;
        private readonly List<Type> _fields;

        public InteractingWithPotionCardSystem(IEntityManager entityManager)
        {
            _entityManager = entityManager;
            _players = new List<Type>();
            _players.Add(typeof(PlayerCardComponent));

            _potions = new List<Type>();
            _potions.Add(typeof(PotionCardComponent));
            _potions.Add(typeof(InteractProcessingComponent));

            _groupOfInteractableCards = new List<Type>();
            _groupOfInteractableCards.Add(typeof(InteractableComponent));

            _fields = new List<Type>();
            _fields.Add(typeof(FieldComponent));
        }

        public void Execute()
        {
            var potions = _entityManager.GetEntitiesOfGroup(_potions);
            var players = _entityManager.GetEntitiesOfGroup(_players);
            var interactableCards = _entityManager.GetEntitiesOfGroup(_groupOfInteractableCards);

            foreach (var potion in potions)
            {
                var potionComponent = (PotionCardComponent)potion.GetComponent(typeof(PotionCardComponent));
                if (potionComponent.ProvidingTypeEffect == "Healing")
                {
                    foreach (var player in players)
                    {
                        var healingComponent = (HealingStatusComponent)player.GetComponent(typeof(HealingStatusComponent));
                        if (healingComponent != null)
                        {
                            player.RemoveComponent(typeof(HealingStatusComponent));
                        }

                        player.AddComponent(new HealingStatusComponent(potionComponent.ImpactDuration, potionComponent.ImpactForce));
                    }
                }
            }

            foreach (var potion in potions)
            {
                potion.RemoveComponent(typeof(InteractProcessingComponent));
                var potionPositionComponent = (PositionRelativeFieldCenterComponent)potion.GetComponent(typeof(PositionRelativeFieldCenterComponent));
                potion.AddComponent(new NeedToBeDestroyedComponent());
                foreach (var player in players)
                {
                    player.AddComponent(new MovementOnFieldComponent(potionPositionComponent.CurrentPosition));
                }
            }

            if (potions.Count != 0)
            {
                var fields = _entityManager.GetEntitiesOfGroup(_fields);

                foreach (var field in fields)
                {
                    field.AddComponent(new TurnDoneComponent());
                }

                foreach (var interactableCard in interactableCards)
                {
                    interactableCard.RemoveComponent(typeof(InteractableComponent));
                }
            }
        }
    }
}