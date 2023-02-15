using System;
using Project.Scripts.Area.Systems.Logic;
using Project.Scripts.Core.ECS.Entity;
using Project.Scripts.Core.ECS.System;
using UnityEngine;

namespace Project.Scripts
{
    public class Entry : MonoBehaviour
    {
        private IEntityManager _entityManager;
        private ISystem _startInitializerSystem;
        private ISystem _cardGeneratorSystem;

        private void Start()
        {
            _entityManager = new EntityManager();

            _startInitializerSystem = new StartInitializerSystem(_entityManager);
            _cardGeneratorSystem = new CardGeneratorSystem(_entityManager);
        }

        private void Update()
        {
            _startInitializerSystem.Execute();
            _cardGeneratorSystem.Execute();
        }
    }
}