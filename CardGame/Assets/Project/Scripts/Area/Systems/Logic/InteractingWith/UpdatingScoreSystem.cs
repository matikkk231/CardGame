using System;
using System.Collections.Generic;
using Project.Scripts.Area.Components.Logic;
using Project.Scripts.Area.Components.View.GameObjectComponent;
using Project.Scripts.Core.ECS.Entity;
using Project.Scripts.Core.ECS.System;

namespace Project.Scripts.Area.Systems.Logic
{
    public class UpdatingScoreSystem : ISystem
    {
        private readonly IEntityManager _entityManager;
        private readonly List<Type> _scores;

        public UpdatingScoreSystem(IEntityManager entityManager)
        {
            _entityManager = entityManager;
            _scores = new List<Type>();
            _scores.Add(typeof(ScoreComponent));
        }

        public void Execute()
        {
            var scores = _entityManager.GetEntitiesOfGroup(_scores);
            foreach (var score in scores)
            {
                var gameObjectComponent = (GameObjectComponent)score.GetComponent(typeof(GameObjectComponent));
                var scoreComponent = (ScoreComponent)score.GetComponent(typeof(ScoreComponent));

                var counter = (CounterView)gameObjectComponent.GameObject.GetComponent(typeof(CounterView));

                counter.ImpactText.text = scoreComponent.ScoreValue.ToString();
            }
        }
    }
}