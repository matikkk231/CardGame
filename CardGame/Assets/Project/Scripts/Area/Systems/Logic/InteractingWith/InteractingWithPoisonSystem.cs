using System;
using System.Collections.Generic;
using Project.Scripts.Area.Components.Logic;
using Project.Scripts.Core.ECS.Entity;
using Project.Scripts.Core.ECS.System;

namespace Project.Scripts.Area.Systems.Logic
{
    public class InteractingWithPoisonSystem : ISystem
    {
        public readonly IEntityManager _entityManager;
        public readonly List<Type> _poisons;
        public readonly List<Type> _players;
        private readonly List<Type> _groupOfInteractableCards;
        private readonly List<Type> _fields;

        public InteractingWithPoisonSystem(IEntityManager entityManager)
        {
            _entityManager = entityManager;
            _poisons = new List<Type>();
            _poisons.Add(typeof(ToxicPotionComponent));
            _poisons.Add(typeof(InteractProcessingComponent));
            _players = new List<Type>();
            _players.Add(typeof(PlayerCardComponent));
            _groupOfInteractableCards = new List<Type>();
            _groupOfInteractableCards.Add(typeof(InteractableComponent));
            _fields = new List<Type>();
            _fields.Add(typeof(FieldComponent));
        }

        public void Execute()
        {
            var poisons = _entityManager.GetEntitiesOfGroup(_poisons);
            var players = _entityManager.GetEntitiesOfGroup(_players);


            foreach (var poison in poisons)
            {
                var potionComponent = (PotionCardComponent)poison.GetComponent(typeof(PotionCardComponent));

                foreach (var player in players)
                {
                    var healingStatusComponent = (HealingStatusComponent)player.GetComponent(typeof(HealingStatusComponent));
                    if (healingStatusComponent != null)
                    {
                        player.RemoveComponent(typeof(HealingStatusComponent));
                    }

                    var toxicStatusComponent = (ToxicStatusComponent)player.GetComponent(typeof(ToxicStatusComponent));

                    if (toxicStatusComponent != null)
                    {
                        player.RemoveComponent(typeof(ToxicStatusComponent));
                    }

                    player.AddComponent(new ToxicStatusComponent(potionComponent.ImpactDuration, potionComponent.ImpactForce));
                }
            }

            foreach (var potion in poisons)
            {
                potion.RemoveComponent(typeof(InteractProcessingComponent));
                var potionPositionComponent = (PositionRelativeFieldCenterComponent)potion.GetComponent(typeof(PositionRelativeFieldCenterComponent));
                potion.AddComponent(new NeedToBeDestroyedComponent());
                foreach (var player in players)
                {
                    player.AddComponent(new MovementOnFieldComponent(potionPositionComponent.CurrentPosition));
                }
            }

            var interactableCards = _entityManager.GetEntitiesOfGroup(_groupOfInteractableCards);
            if (poisons.Count != 0)
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