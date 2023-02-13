using System;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Area.Components;
using Project.Scripts.Core.ECS.Entity;
using Project.Scripts.Core.ECS.System;
using UnityEngine;

namespace Project.Scripts.Area.Systems
{
    public class NotifierSystem : ISystem
    {
        private List<Type> ComponentsOfGroup { get; }
        private readonly IEntityManager _entityManager;

        public NotifierSystem(IEntityManager entityManager)
        {
            ComponentsOfGroup = new List<Type>();
            _entityManager = entityManager;
            ComponentsOfGroup.Add(typeof(NotifierComponent));
        }

        public void Execute()
        {
            var needfulEntities = _entityManager.GetEntitiesOfGroup(ComponentsOfGroup);
            if (needfulEntities.Count != 0)
            {
                foreach (var entity in needfulEntities)
                {
                    foreach (var component in entity.Components.ToList())
                    {
                        if (component.GetType() == typeof(NotifierComponent))
                        {
                            var notifierComponent = (NotifierComponent)component;
                            if (!notifierComponent.IsNotified)
                            {
                                Debug.Log(notifierComponent.TextMessage);
                            }
                            notifierComponent.IsNotified = true;
                        }
                    }
                }
            }
        }
    }
}