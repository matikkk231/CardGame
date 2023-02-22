using System;
using System.Collections.Generic;
using Project.Scripts.Area.Components.View;
using Project.Scripts.Area.Components.View.GameObjectComponent;
using Project.Scripts.Core.ECS.Entity;
using Project.Scripts.Core.ECS.System;
using UnityEngine;

namespace Project.Scripts.Area.Systems.View.PrefabInstantiator
{
    public class PrefabInstantiatorSystem : ISystem
    {
        private readonly IEntityManager _entityManager;
        private readonly List<Type> _needFulGroup;
        private readonly List<PrefabType> _prefabTypes;

        public PrefabInstantiatorSystem(IEntityManager entityManager, List<PrefabType> prefabTypes)
        {
            _entityManager = entityManager;
            _needFulGroup = new List<Type>();
            _needFulGroup.Add(typeof(NeedInstantiatingPrefabComponent));
            _prefabTypes = prefabTypes;
        }

        public void Execute()
        {
            var needFulGroup = _entityManager.GetEntitiesOfGroup(_needFulGroup);
            foreach (var entity in needFulGroup)
            {
                var needInstantiatingPrefabComponent = (NeedInstantiatingPrefabComponent)entity.GetComponent(typeof(NeedInstantiatingPrefabComponent));
                foreach (var prefabType in _prefabTypes)
                {
                    if (prefabType.Id == needInstantiatingPrefabComponent.PrefabTypeId)
                    {
                        var gameObject = GameObject.Instantiate(prefabType.Prefab);
                        gameObject.transform.position = needInstantiatingPrefabComponent.PositionWhereInstantiated;
                        entity.RemoveComponent(typeof(NeedInstantiatingPrefabComponent));
                        entity.AddComponent(new GameObjectComponent(gameObject));
                    }
                }
            }
        }
    }
}