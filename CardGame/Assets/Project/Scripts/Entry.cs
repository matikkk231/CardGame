using System.Collections.Generic;
using Project.Scripts.Area.Systems.Logic;
using Project.Scripts.Area.Systems.View.PrefabInstantiator;
using Project.Scripts.Core.ECS.Entity;
using Project.Scripts.Core.ECS.System;
using UnityEngine;

namespace Project.Scripts
{
    public class Entry : MonoBehaviour
    {
        [SerializeField] private List<PrefabType> _prefabTypes;

        private IEntityManager _entityManager;
        private ISystem _startInitializerSystem;
        private ISystem _cardGeneratorSystem;
        private ISystem _prefabInstantiatorSystem;

        private void Start()
        {
            _entityManager = new EntityManager();

            _startInitializerSystem = new StartInitializerSystem(_entityManager);
            _cardGeneratorSystem = new CardGeneratorSystem(_entityManager);
            _prefabInstantiatorSystem = new PrefabInstantiatorSystem(_entityManager, _prefabTypes);
        }

        private void Update()
        {
            _startInitializerSystem.Execute();
            _cardGeneratorSystem.Execute();
            _prefabInstantiatorSystem.Execute();
        }
    }
}