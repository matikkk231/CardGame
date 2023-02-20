using System;
using System.Collections.Generic;
using Project.Scripts.Area.Components.Logic;
using Project.Scripts.Area.Components.View.GameObjectComponent;
using Project.Scripts.Core.ECS.Entity;
using Project.Scripts.Core.ECS.System;
using UnityEngine;

namespace Project.Scripts.Area.Systems.View.PrefabInstantiator
{
    public class PrefabInstantiatorSystem : ISystem
    {
        private readonly IEntityManager _entityManager;
        private readonly List<Type> _needfulGroup;
        private readonly List<PrefabType> _prefabTypes;

        public PrefabInstantiatorSystem(IEntityManager entityManager, List<PrefabType> prefabTypes )
        {
            _prefabTypes = prefabTypes;
            _entityManager = entityManager;
            _needfulGroup = new List<Type>();
            _needfulGroup.Add(typeof(NeedInstantiatingPrefab));
        }

        public void Execute()
        {
            var entities = _entityManager.GetEntitiesOfGroup(_needfulGroup);
            foreach (var entity in entities)
            {
                NeedInstantiatingPrefab needInstantiatingPrefabComponent =
                    (NeedInstantiatingPrefab)entity.GetComponent(typeof(NeedInstantiatingPrefab));
                foreach (var prefabType in _prefabTypes)
                {
                    if (prefabType.Id == needInstantiatingPrefabComponent.PrefabTypeId)
                    {
                        var gameObject = GameObject.Instantiate(prefabType.Prefab);
                        int newPositionOfObjectX = needInstantiatingPrefabComponent.PositionWhereShouldBeInstantiated.x;
                        int newPositionOfObjectY = needInstantiatingPrefabComponent.PositionWhereShouldBeInstantiated.y;
                        Vector3 newPositionOfObject =
                            new Vector3(newPositionOfObjectX, newPositionOfObjectY, 0);
                        gameObject.transform.position = newPositionOfObject;


                        entity.AddComponent(new GameObjectComponent(gameObject));
                        entity.RemoveComponent(needInstantiatingPrefabComponent.GetType());
                    }
                }
            }
        }
    }
}