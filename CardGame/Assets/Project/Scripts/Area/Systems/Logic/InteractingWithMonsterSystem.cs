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

        public InteractingWithMonsterSystem(IEntityManager entityManager)
        {
            _entityManager = entityManager;
            _monsters = new List<Type>();
            _monsters.Add(typeof(MonsterCardComponent));
            _monsters.Add(typeof(InteractProcessingComponent));
            _players = new List<Type>();
            _players.Add(typeof(PlayerCardComponent));
        }

        public void Execute()
        {
            var monsters = _entityManager.GetEntitiesOfGroup(_monsters);
            var players = _entityManager.GetEntitiesOfGroup(_players);
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
        }
    }
}