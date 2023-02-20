using System;
using System.Collections.Generic;
using Project.Scripts.Area.Components.Logic;
using Project.Scripts.Core.ECS.Entity;
using Project.Scripts.Core.ECS.System;
using Unity.Mathematics;

namespace Project.Scripts.Area.Systems.Logic
{
    public class CheckingAbilityToInteractSystem : ISystem
    {
        private readonly IEntityManager _entityManager;
        private readonly List<Type> _players;
        private readonly List<Type> _fields;


        public CheckingAbilityToInteractSystem(IEntityManager entityManager)
        {
            _entityManager = entityManager;

            _players = new List<Type>();
            _players.Add(typeof(PlayerCardComponent));

            _fields = new List<Type>();
            _fields.Add(typeof(FieldComponent));
        }

        public void Execute()
        {
            AddAbilityToInteractWithCards();
        }

        private void AddAbilityToInteractWithCards()
        {
            var players = _entityManager.GetEntitiesOfGroup(_players);
            var fields = _entityManager.GetEntitiesOfGroup(_fields);

            var field = fields[0];
            var player = players[0];

            var fieldComponent = (FieldComponent)field.GetComponent(typeof(FieldComponent));
            var playersPositionRelativeCenterComponent = (PositionRelativeFieldCenterComponent)player.GetComponent(
                typeof(PositionRelativeFieldCenterComponent));

            int x = playersPositionRelativeCenterComponent.CurrentPosition.x;
            int y = playersPositionRelativeCenterComponent.CurrentPosition.y;

            int2 currentPlayerPosition = new int2(
                x + Math.Abs(fieldComponent.MinRelativeCenterPositionX),
                y + Math.Abs(fieldComponent.MinRelativeCenterPositionY));

            if (currentPlayerPosition.x < fieldComponent.MaxPositionX)
            {
                var interactableComponent = fieldComponent
                    .PositionsWithCard[currentPlayerPosition.x + 1, currentPlayerPosition.y]
                    .GetComponent(typeof(InteractableComponent));
                if (interactableComponent == null)
                {
                    var card = fieldComponent.PositionsWithCard[currentPlayerPosition.x + 1, currentPlayerPosition.y];
                    card.AddComponent(new InteractableComponent());
                }
            }

            int minPositionX = 0;
            if (currentPlayerPosition.x > minPositionX)
            {
                var interactableComponent = fieldComponent
                    .PositionsWithCard[currentPlayerPosition.x - 1, currentPlayerPosition.y]
                    .GetComponent(typeof(InteractableComponent));
                if (interactableComponent == null)
                {
                    var card = fieldComponent.PositionsWithCard[currentPlayerPosition.x - 1, currentPlayerPosition.y];
                    card.AddComponent(new InteractableComponent());
                }
            }

            if (currentPlayerPosition.y < fieldComponent.MaxPositionY)
            {
                var interactableComponent = fieldComponent
                    .PositionsWithCard[currentPlayerPosition.x, currentPlayerPosition.y + 1]
                    .GetComponent(typeof(InteractableComponent));
                if (interactableComponent == null)
                {
                    var card = fieldComponent.PositionsWithCard[currentPlayerPosition.x, currentPlayerPosition.y + 1];
                    card.AddComponent(new InteractableComponent());
                }
            }

            int minPositionY = 0;
            if (currentPlayerPosition.y > minPositionY)
            {
                var interactableComponent = fieldComponent
                    .PositionsWithCard[currentPlayerPosition.x, currentPlayerPosition.y - 1]
                    .GetComponent(typeof(InteractableComponent));
                if (interactableComponent == null)
                {
                    var card = fieldComponent.PositionsWithCard[currentPlayerPosition.x, currentPlayerPosition.y - 1];
                    card.AddComponent(new InteractableComponent());
                }
            }
        }
    }
}