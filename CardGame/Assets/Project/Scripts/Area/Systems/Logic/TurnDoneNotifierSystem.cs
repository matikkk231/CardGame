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
        public List<Type> _fieldsWhereTurnDone;

        public TurnDoneNotifierSystem(IEntityManager entityManager)
        {
            _entityManager = entityManager;
            _cards = new List<Type>();
            _cards.Add(typeof(CardComponent));
            _fieldsWhereTurnDone = new List<Type>();
            _fieldsWhereTurnDone.Add(typeof(FieldComponent));
            _fieldsWhereTurnDone.Add(typeof(TurnDoneComponent));
        }


        public void Execute()
        {
            var fieldsWhereTurnDone = _entityManager.GetEntitiesOfGroup(_fieldsWhereTurnDone);
            foreach (var fieldWhereTurnDone in fieldsWhereTurnDone)
            {
                fieldWhereTurnDone.RemoveComponent(typeof(TurnDoneComponent));
            }

            var cards = _entityManager.GetEntitiesOfGroup(_cards);

            foreach (var card in cards)
            {
                card.AddComponent(new TurnDoneComponent());
            }
        }
    }
}