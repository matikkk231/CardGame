using System;
using System.Collections.Generic;
using Project.Scripts.Area.Components.Logic;
using Project.Scripts.Core.ECS.Entity;
using Project.Scripts.Core.ECS.System;

namespace Project.Scripts.Area.Systems.Logic
{
    public class InteractingWithMonsterSystem : ISystem
    {
        private readonly IEntityManager _entityManager;
        private readonly List<Type> _monsters;
        private readonly List<Type> _players;
        private readonly List<Type> _groupOfInteractableCards;
        private readonly List<Type> _fields;

        public InteractingWithMonsterSystem(IEntityManager entityManager)
        {
            _entityManager = entityManager;
            _monsters = new List<Type>();
            _monsters.Add(typeof(MonsterCardComponent));
            _monsters.Add(typeof(InteractProcessingComponent));
            _players = new List<Type>();
            _players.Add(typeof(PlayerCardComponent));
            _groupOfInteractableCards = new List<Type>();
            _groupOfInteractableCards.Add(typeof(InteractableComponent));
            _fields = new List<Type>();
            _fields.Add(typeof(FieldComponent));
        }

        public void Execute()
        {
            var monsters = _entityManager.GetEntitiesOfGroup(_monsters);
            var players = _entityManager.GetEntitiesOfGroup(_players);
            var interactableCards = _entityManager.GetEntitiesOfGroup(_groupOfInteractableCards);
            foreach (var interactableCard in interactableCards)
            {
                interactableCard.RemoveComponent(typeof(InteractableComponent));
            }

            foreach (var monster in monsters)
            {
                var healthComponent = (HealthComponent)monster.GetComponent(typeof(HealthComponent));

                foreach (var player in players)
                {
                    var healthPlayerComponent = (HealthComponent)player.GetComponent(typeof(HealthComponent));
                    if (healthPlayerComponent.CurrentHealth > healthComponent.CurrentHealth)
                    {
                        healthPlayerComponent.CurrentHealth -= healthComponent.CurrentHealth;
                        monster.RemoveComponent(typeof(InteractProcessingComponent));
                        monster.AddComponent(new NeedToBeDestroyedComponent());

                        var monsterPositionComponent =
                            (PositionRelativeFieldCenterComponent)monster.GetComponent(
                                typeof(PositionRelativeFieldCenterComponent));
                        player.AddComponent(new MovementOnFieldComponent(monsterPositionComponent.CurrentPosition));
                    }
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