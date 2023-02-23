using System;
using System.Collections.Generic;
using Project.Scripts.Area.Components.Logic;
using Project.Scripts.Area.Components.View.GameObjectComponent;
using Project.Scripts.Core.ECS.Entity;
using Project.Scripts.Core.ECS.System;

namespace Project.Scripts.Area.Systems.Logic
{
    public class LoosingSystem : ISystem
    {
        private readonly IEntityManager _entityManager;
        private readonly List<Type> _fieldWhereLost;

        public LoosingSystem(IEntityManager entityManager)
        {
            _entityManager = entityManager;
            _fieldWhereLost = new List<Type>();
            _fieldWhereLost.Add(typeof(LoseComponent));
        }

        public void Execute()
        {
            var fieldsWhereLost = _entityManager.GetEntitiesOfGroup(_fieldWhereLost);

            foreach (var fieldWhereLost in fieldsWhereLost)
            {
                var lostComponent = fieldWhereLost.GetComponent(typeof(LoseComponent));
                if (lostComponent != null)
                {
                    var fieldComponent = (FieldComponent)fieldWhereLost.GetComponent(typeof(FieldComponent));
                    for (int x = 0; x <= fieldComponent.MaxPositionX; x++)
                    {
                        for (int y = 0; y <= fieldComponent.MaxPositionY; y++)
                        {
                            var player = (PlayerCardComponent)fieldComponent.PositionsWithCard[x, y].GetComponent(typeof(PlayerCardComponent));
                            if (player == null)
                            {
                                var gameObjectComponent = (GameObjectComponent)fieldComponent.PositionsWithCard[x, y].GetComponent(typeof(GameObjectComponent));
                                gameObjectComponent.GameObject.SetActive(false);
                            }
                        }
                    }
                }
            }
        }
    }
}