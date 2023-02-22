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
        private readonly List<CardPrefabType> _cardPrefabTypes;

        public CardPrefabInstantiatorSystem(IEntityManager entityManager, List<CardPrefabType> cardPrefabTypes)
        {
            _cardPrefabTypes = cardPrefabTypes;
            _entityManager = entityManager;
            _needfulGroup = new List<Type>();
            _needfulGroup.Add(typeof(NeedInstantiatingCardPrefabComponent));
        }

        public void Execute()
        {
            var entities = _entityManager.GetEntitiesOfGroup(_needfulGroup);
            foreach (var entity in entities)
            {
                NeedInstantiatingCardPrefabComponent needInstantiatingCardPrefabComponentComponent =
                    (NeedInstantiatingCardPrefabComponent)entity.GetComponent(typeof(NeedInstantiatingCardPrefabComponent));
                foreach (var prefabType in _cardPrefabTypes)
                {
                    if (prefabType.Id == needInstantiatingCardPrefabComponentComponent.CardPrefabTypeId)
                    {
                        var gameObject = GameObject.Instantiate(prefabType.Prefab);
                        var contentImageRenderer = (ContentImageView)gameObject.GetComponent(typeof(ContentImageView));
                        contentImageRenderer.ContentImageRenderer.sprite =
                            needInstantiatingCardPrefabComponentComponent.CardContent;
                        int newPositionOfObjectX =
                            needInstantiatingCardPrefabComponentComponent.PositionWhereShouldBeInstantiated.x;
                        int newPositionOfObjectY =
                            needInstantiatingCardPrefabComponentComponent.PositionWhereShouldBeInstantiated.y;
                        Vector3 newPositionOfObject =
                            new Vector3(newPositionOfObjectX, newPositionOfObjectY, 0);
                        gameObject.transform.position = newPositionOfObject;


                        entity.AddComponent(new GameObjectComponent(gameObject));
                        entity.RemoveComponent(needInstantiatingCardPrefabComponentComponent.GetType());
                    }
                }
            }
        }
    }
}