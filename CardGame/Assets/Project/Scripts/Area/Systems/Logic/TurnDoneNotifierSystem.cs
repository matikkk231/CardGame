using System;
using System.Collections.Generic;
using Project.Scripts.Area.Components.Logic;
using Project.Scripts.Core.ECS.Entity;
using Project.Scripts.Core.ECS.System;

namespace Project.Scripts.Area.Systems.Logic
{
    public class TurnDoneNotifierSystem : ISystem
    {
        public IEntityManager _entityManager;
        public List<Type> _cards;
        public List<Type> _fields;

        public TurnDoneNotifierSystem(IEntityManager entityManager)
        {
            _entityManager = entityManager;
            _cards = new List<Type>();
            _cards.Add(typeof(CardComponent));
            _fields = new List<Type>();
            _fields.Add(typeof(TurnDoneComponent));
            _fields.Add(typeof(FieldComponent));
        }


        public void Execute()
        {
            var cards = _entityManager.GetEntitiesOfGroup(_cards);
            var fieldWhereTurnDone = _entityManager.GetEntitiesOfGroup(_fields);

            if (fieldWhereTurnDone.Count != 0)
            {
                foreach (var card in cards)
                {
                    card.AddComponent(new TurnDoneComponent());
                }
            }
        }
    }
}