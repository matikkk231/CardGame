using Project.Scripts.Area.Components.Logic;
using Project.Scripts.Area.Components.View.GameObjectComponent;
using Project.Scripts.Core.ECS.Entity;
using Project.Scripts.Core.ECS.System;
using Unity.Mathematics;
using UnityEngine;

namespace Project.Scripts.Area.Systems.Logic
{
    public class StartInitializerSystem : ISystem
    {
        private IEntityManager _entityManager;
        private bool _initializedFirst;
        private Sprite _playersCardSprite;

        public StartInitializerSystem(IEntityManager entityManager, Sprite playersCardSprite)
        {
            _entityManager = entityManager;
            _initializedFirst = true;
            _playersCardSprite = playersCardSprite;
        }

        public void Execute()
        {
            if (_initializedFirst)
            {
                var field = _entityManager.CreateEntity();
                field.AddComponent(new FieldComponent(1, 1, -1, -1));

                var fieldComponent = (FieldComponent)field.GetComponent(typeof(FieldComponent));

                var playerCard = _entityManager.CreateEntity();
                int2 centerOfField = new int2(1, 1);
                fieldComponent.PositionsWithCard[centerOfField.x, centerOfField.y] = playerCard;
                playerCard.AddComponent(new HealthComponent(10));
                playerCard.AddComponent(new PositionRelativeFieldCenterComponent(new int2(0, 0)));
                playerCard.AddComponent(new PlayerCardComponent());
                int2 cardPosition = new int2(0, 0);
                playerCard.AddComponent(new NeedInstantiatingCardPrefab(PrefabTypesId.PlayerCard, cardPosition, _playersCardSprite));

                _initializedFirst = false;
            }
        }
    }
}