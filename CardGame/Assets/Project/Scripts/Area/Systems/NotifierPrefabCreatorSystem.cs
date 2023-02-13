using System;
using System.Collections.Generic;
using System.Linq;
using Project.Scripts.Area.Components;
using Project.Scripts.Core.ECS.Entity;
using Project.Scripts.Core.ECS.System;
using TMPro;
using UnityEngine;

namespace Project.Scripts.Area.Systems
{
    public class NotifierPrefabCreatorSystem : ISystem
    {
        private List<Type> ComponentsOfGroup { get; }
        private readonly IEntityManager _entityManager;
        private bool instatiatePrefabFirstTime;

        private readonly GameObject _prefab;
        private readonly GameObject _parentPrefab;
        private GameObject _canvas;


        public NotifierPrefabCreatorSystem(IEntityManager entityManager, GameObject prefab, GameObject parentPrefab)
        {
            ComponentsOfGroup = new List<Type>();
            ComponentsOfGroup.Add(typeof(NotifierPrefabComponent));
            _entityManager = entityManager;
            _prefab = prefab;
            _parentPrefab = parentPrefab;
        }

        public void Execute()
        {
            var entities = _entityManager.GetEntitiesOfGroup(ComponentsOfGroup);
            if (entities.Count != 0)
            {
                foreach (var entity in entities)
                {
                    foreach (var component in entity.Components.ToList())
                    {
                        bool isComponentNotifierPrefab = component.GetType() == typeof(NotifierPrefabComponent);
                        if (isComponentNotifierPrefab)
                        {
                            var notifierPrefabComponent = (NotifierPrefabComponent)component;
                            var notifierObject = GameObject.Instantiate(_prefab);
                            if (!instatiatePrefabFirstTime)
                            {
                                _canvas = GameObject.Instantiate(_parentPrefab);
                                instatiatePrefabFirstTime = false;
                            }


                            notifierObject.transform.SetParent(_canvas.transform);
                            entity.Components.Remove(component);
                            entity.Components.Add(new NotifierComponent("I exist",
                                notifierObject.GetComponent<TextMeshProUGUI>()));
                        }
                    }
                }
            }
        }
    }
}