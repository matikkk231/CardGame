using System;
using System.Collections.Generic;
using Project.Scripts.Area.Components.Logic;
using Project.Scripts.Core.ECS.Entity;
using Project.Scripts.Core.ECS.System;

namespace Project.Scripts.Area.Systems.Logic
{
    public class TurnFinishedNotifierSystem : ISystem
    {
        public IEntityManager _entityManager;
        public List<Type> _cards;

        public TurnFinishedNotifierSystem(IEntityManager entityManager)
        {
            _entityManager = entityManager;
            _cards = new List<Type>();
            _cards.Add(typeof(TurnDoneComponent));
        }

        public void Execute()
        {
            var cards = _entityManager.GetEntitiesOfGroup(_cards);
            foreach (var card in cards)
            {
                card.RemoveComponent(typeof(TurnDoneComponent));
            }
        }
    }
}