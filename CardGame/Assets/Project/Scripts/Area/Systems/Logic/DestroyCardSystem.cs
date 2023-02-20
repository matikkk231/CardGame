using System;
using System.Collections.Generic;
using Project.Scripts.Area.Components.Logic;
using Project.Scripts.Area.Components.View.GameObjectComponent;
using Project.Scripts.Core.ECS.Entity;
using Project.Scripts.Core.ECS.System;
using Unity.Mathematics;
using UnityEngine;

namespace Project.Scripts.Area.Systems.Logic
{
    public class DestroyCardSystem : ISystem
    {
        private IEntityManager _entityManager;
        private List<Type> _fieldComponents;
        private List<Type> _destroyableCardComponents;

        public DestroyCardSystem(IEntityManager entityManager)
        {
            _entityManager = entityManager;
            _fieldComponents = new List<Type>();
            _fieldComponents.Add(typeof(FieldComponent));
            _destroyableCardComponents = new List<Type>();
            _destroyableCardComponents.Add(typeof(NeedToBeDestroyedComponent));
        }


        public void Execute()
        {
            var cards = _entityManager.GetEntitiesOfGroup(_destroyableCardComponents);
            foreach (var field in _entityManager.GetEntitiesOfGroup(_fieldComponents))
            {
                var fieldComponent = (FieldComponent)field.GetComponent(typeof(FieldComponent));

                foreach (var card in cards)
                {
                    var cardPosition =
                        (PositionRelativeFieldCenterComponent)card.GetComponent(
                            typeof(PositionRelativeFieldCenterComponent));
                    fieldComponent.PositionsWithCard[
                        cardPosition.CurrentPosition.x + Math.Abs(fieldComponent.MinRelativeCenterPositionX),
                        cardPosition.CurrentPosition.y + Math.Abs(fieldComponent.MinRelativeCenterPositionY)] = null;
                    var gameObjectComponent = (GameObjectComponent)card.GetComponent(typeof(GameObjectComponent));
                    GameObject.Destroy(gameObjectComponent.GameObject);
                    _entityManager.RemoveEntity(card);
                }
            }
        }
    }
}