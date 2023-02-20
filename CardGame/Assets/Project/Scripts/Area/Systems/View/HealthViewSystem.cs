using System;
using System.Collections.Generic;
using Project.Scripts.Area.Components.Logic;
using Project.Scripts.Area.Components.View.GameObjectComponent;
using Project.Scripts.Core.ECS.Entity;
using Project.Scripts.Core.ECS.System;
using TMPro;

namespace Project.Scripts.Area.Systems.Logic.View
{
    public class HealthViewSystem : ISystem
    {
        private IEntityManager _entityManager;
        private List<Type> _groupOfComponents;

        public HealthViewSystem(IEntityManager entityManager)
        {
            _entityManager = entityManager;
            _groupOfComponents = new List<Type>();
            _groupOfComponents.Add(typeof(HealthComponent));
            _groupOfComponents.Add(typeof(GameObjectComponent));
        }

        public void Execute()
        {
            var appropriateEntities = _entityManager.GetEntitiesOfGroup(_groupOfComponents);
            foreach (var entity in appropriateEntities)
            {
                var healthLogic = (HealthComponent)entity.GetComponent(typeof(HealthComponent));
                var gameObjectComponent = (GameObjectComponent)entity.GetComponent(typeof(GameObjectComponent));
                var healthViewComponent =
                    (HealthViewComponent)gameObjectComponent.GameObject.GetComponent(typeof(HealthViewComponent));

                healthViewComponent.TextMeshProUGUI.text = healthLogic.CurrentHealth + "/" + healthLogic.MaxHealth;
            }
        }
    }
}