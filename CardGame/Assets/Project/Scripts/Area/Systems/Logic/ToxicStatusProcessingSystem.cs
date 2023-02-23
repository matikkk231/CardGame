using System;
using System.Collections.Generic;
using Project.Scripts.Area.Components.Logic;
using Project.Scripts.Core.ECS.Entity;
using Project.Scripts.Core.ECS.System;

namespace Project.Scripts.Area.Systems.Logic
{
    public class ToxicStatusProcessingSystem : ISystem
    {
        private readonly IEntityManager _entityManager;
        private readonly List<Type> _cardsWhichNeedPoisoning;

        public ToxicStatusProcessingSystem(IEntityManager entityManager)
        {
            _entityManager = entityManager;
            _cardsWhichNeedPoisoning = new List<Type>();
            _cardsWhichNeedPoisoning.Add(typeof(ToxicStatusComponent));
            _cardsWhichNeedPoisoning.Add(typeof(TurnDoneComponent));
        }

        public void Execute()
        {
            var cardsWhichNeedPoisoning = _entityManager.GetEntitiesOfGroup(_cardsWhichNeedPoisoning);

            foreach (var cardWhichNeedPoisoning in cardsWhichNeedPoisoning)
            {
                var toxicStatusComponent = (ToxicStatusComponent)cardWhichNeedPoisoning.GetComponent(typeof(ToxicStatusComponent));
                var healthComponent = (HealthComponent)cardWhichNeedPoisoning.GetComponent(typeof(HealthComponent));


                bool isHealthPointsReachedMinimum = healthComponent.CurrentHealth - toxicStatusComponent.PoisoningForce <= 1;
                if (isHealthPointsReachedMinimum)
                {
                    healthComponent.CurrentHealth = 1;
                    cardWhichNeedPoisoning.RemoveComponent(typeof(ToxicStatusComponent));
                }
                else
                {
                    healthComponent.CurrentHealth -= toxicStatusComponent.PoisoningForce;
                    toxicStatusComponent.PoisoningDuration--;
                }

                if (toxicStatusComponent.PoisoningDuration == 0)
                {
                    cardWhichNeedPoisoning.RemoveComponent(typeof(ToxicStatusComponent));
                }
            }
        }
    }
}