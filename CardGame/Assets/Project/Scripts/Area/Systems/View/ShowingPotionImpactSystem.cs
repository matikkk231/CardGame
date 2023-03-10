using System;
using System.Collections.Generic;
using Project.Scripts.Area.Components.Logic;
using Project.Scripts.Area.Components.View.GameObjectComponent;
using Project.Scripts.Core.ECS.Entity;
using Project.Scripts.Core.ECS.System;

namespace Project.Scripts.Area.Systems.Logic.View
{
    public class ShowingPotionImpactSystem : ISystem
    {
        private readonly IEntityManager _entityManager;
        private readonly List<Type> _potions;

        public ShowingPotionImpactSystem(IEntityManager entityManager)
        {
            _entityManager = entityManager;
            _potions = new List<Type>();
            _potions.Add(typeof(GameObjectComponent));
            _potions.Add(typeof(PotionCardComponent));
            
        }

        public void Execute()
        {
            var potions = _entityManager.GetEntitiesOfGroup(_potions);
            foreach (var potion in potions)
            {
                var potionCardComponent = (PotionCardComponent)potion.GetComponent(typeof(PotionCardComponent));
                var gameObjectComponent = (GameObjectComponent)potion.GetComponent(typeof(GameObjectComponent));
                var impactTextVew = (CounterView)gameObjectComponent.GameObject.GetComponent(typeof(CounterView));
                impactTextVew.ImpactText.text = potionCardComponent.ImpactForce.ToString();
            }
        }
    }
}