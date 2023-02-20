using System;
using System.Collections.Generic;
using Project.Scripts.Area.Systems.Logic;
using Project.Scripts.Area.Systems.Logic.View;
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
        private ISystem _healthViewSystem;
        private ISystem _checkingAbilityToInteractSystem;
        private ISystem _checkingInteractingTrySystem;
        private ISystem _fieldManagerSystem;
        private ISystem _interactingWithEmptyCardsSystem;
        private ISystem _destroyingCardsSystem;
        private ISystem _movementSystem;

        private void Start()
        {
            _entityManager = new EntityManager();
            
            _startInitializerSystem = new StartInitializerSystem(_entityManager);
            _cardGeneratorSystem = new CardGeneratorSystem(_entityManager);
            _prefabInstantiatorSystem = new PrefabInstantiatorSystem(_entityManager, _prefabTypes);
            _healthViewSystem = new HealthViewSystem(_entityManager);
            _checkingAbilityToInteractSystem = new CheckingAbilityToInteractSystem(_entityManager);
            _checkingInteractingTrySystem = new CheckingInteractingTrySystem(_entityManager);
            _fieldManagerSystem = new FieldManagerSystem(_entityManager);
            _interactingWithEmptyCardsSystem = new InteractingWithEmptyCardsSystem(_entityManager);
            _destroyingCardsSystem = new DestroyCardSystem(_entityManager);
            _movementSystem = new MovingCardsSystem(_entityManager);
        }

        private void Update()
        {
            _startInitializerSystem.Execute();
            _cardGeneratorSystem.Execute();
            _prefabInstantiatorSystem.Execute();
            _healthViewSystem.Execute();
            _fieldManagerSystem.Execute();
            _checkingAbilityToInteractSystem.Execute();

            _checkingInteractingTrySystem.Execute();
            _interactingWithEmptyCardsSystem.Execute();
            _destroyingCardsSystem.Execute();
            _movementSystem.Execute();
            _fieldManagerSystem.Execute();
        }
    }
}