using System;
using System.Collections.Generic;
using Project.Scripts.Area.Components.Logic;
using Project.Scripts.Core.ECS.Entity;
using Project.Scripts.Core.ECS.System;

namespace Project.Scripts.Area.Systems.Logic
{
    public class HealingStatusProcessingSystem : ISystem
    {
        private IEntityManager _entityManager;
        private List<Type> _cardsWhichNeedHealing;

        public HealingStatusProcessingSystem(IEntityManager entityManager)
        {
            _entityManager = entityManager;
            _cardsWhichNeedHealing = new List<Type>();
            _cardsWhichNeedHealing.Add(typeof(HealingStatusComponent));
            _cardsWhichNeedHealing.Add(typeof(TurnDoneComponent));
        }

        public void Execute()
        {
            var cardsWhichNeedHealing = _entityManager.GetEntitiesOfGroup(_cardsWhichNeedHealing);
            foreach (var cardWhichNeedHealing in cardsWhichNeedHealing)
            {
                var healthComponent = (HealthComponent)cardWhichNeedHealing.GetComponent(typeof(HealthComponent));
                var healingStatusComponent = (HealingStatusComponent)cardWhichNeedHealing.GetComponent(typeof(HealingStatusComponent));

                bool isCardGettingMaxHealthAfterHealing = healthComponent.CurrentHealth + healingStatusComponent.HealingValue >= healthComponent.MaxHealth;

                if (isCardGettingMaxHealthAfterHealing)
                {
                    healthComponent.CurrentHealth = healthComponent.MaxHealth;
                }
                else
                {
                    healthComponent.CurrentHealth += healingStatusComponent.HealingValue;
                }

                healingStatusComponent.DurationOfHealing--;

                if (healingStatusComponent.DurationOfHealing == 0)
                {
                    cardWhichNeedHealing.RemoveComponent(typeof(HealingStatusComponent));
                }
            }
        }
    }
}