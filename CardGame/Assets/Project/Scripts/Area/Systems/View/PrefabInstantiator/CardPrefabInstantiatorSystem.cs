using System;
using System.Collections.Generic;
using Project.Scripts.Area.Components.Logic;
using Project.Scripts.Area.Components.View;
using Project.Scripts.Area.Components.View.GameObjectComponent;
using Project.Scripts.Core.ECS.Entity;
using Project.Scripts.Core.ECS.System;
using UnityEngine;

namespace Project.Scripts.Area.Systems.View.PrefabInstantiator
{
    public class CardPrefabInstantiatorSystem : ISystem
    {
        private readonly IEntityManager _entityManager;
        private readonly List<Type> _needfulGroup;
        private readonly List<PrefabType> _prefabTypes;

        public CardPrefabInstantiatorSystem(IEntityManager entityManager, List<PrefabType> prefabTypes)
        {
            _prefabTypes = prefabTypes;
            _entityManager = entityManager;
            _needfulGroup = new List<Type>();
            _needfulGroup.Add(typeof(NeedInstantiatingCardPrefab));
        }

        public void Execute()
        {
            var entities = _entityManager.GetEntitiesOfGroup(_needfulGroup);
            foreach (var entity in entities)
            {
                NeedInstantiatingCardPrefab needInstantiatingCardPrefabComponent =
                    (NeedInstantiatingCardPrefab)entity.GetComponent(typeof(NeedInstantiatingCardPrefab));
                foreach (var prefabType in _prefabTypes)
                {
                    if (prefabType.Id == needInstantiatingCardPrefabComponent.PrefabTypeId)
                    {
                        var gameObject = GameObject.Instantiate(prefabType.Prefab);
                        var contentImageRenderer = (ContentImageView)gameObject.GetComponent(typeof(ContentImageView));
                        contentImageRenderer.ContentImageRenderer.sprite =
                            needInstantiatingCardPrefabComponent.CardContent;
                        int newPositionOfObjectX =
                            needInstantiatingCardPrefabComponent.PositionWhereShouldBeInstantiated.x;
                        int newPositionOfObjectY =
                            needInstantiatingCardPrefabComponent.PositionWhereShouldBeInstantiated.y;
                        Vector3 newPositionOfObject =
                            new Vector3(newPositionOfObjectX, newPositionOfObjectY, 0);
                        gameObject.transform.position = newPositionOfObject;


                        entity.AddComponent(new GameObjectComponent(gameObject));
                        entity.RemoveComponent(needInstantiatingCardPrefabComponent.GetType());
                    }
                }
            }
        }
    }
}