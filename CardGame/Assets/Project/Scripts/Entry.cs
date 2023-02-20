using System.Collections.Generic;
using Project.Scripts.Area.Configs;
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
        [SerializeField] private List<MonsterConfig> _monsterConfigs;
        [SerializeField] private Sprite _playerImage;

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
        private ISystem _interactingWithMonsterCardSystem;

        private void Start()
        {
            _entityManager = new EntityManager();

            _startInitializerSystem = new StartInitializerSystem(_entityManager, _playerImage);
            _cardGeneratorSystem = new CardGeneratorSystem(_entityManager, _monsterConfigs);
            _prefabInstantiatorSystem = new CardPrefabInstantiatorSystem(_entityManager, _prefabTypes);
            _healthViewSystem = new HealthViewSystem(_entityManager);
            _checkingAbilityToInteractSystem = new CheckingAbilityToInteractSystem(_entityManager);
            _checkingInteractingTrySystem = new CheckingInteractingTrySystem(_entityManager);
            _fieldManagerSystem = new FieldManagerSystem(_entityManager);
            _interactingWithEmptyCardsSystem = new InteractingWithEmptyCardsSystem(_entityManager);
            _destroyingCardsSystem = new DestroyCardSystem(_entityManager);
            _movementSystem = new MovingCardsSystem(_entityManager);
            _interactingWithMonsterCardSystem = new InteractingWithMonsterSystem(_entityManager);
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
            _interactingWithMonsterCardSystem.Execute();
            
            _destroyingCardsSystem.Execute();
            _movementSystem.Execute();
            _fieldManagerSystem.Execute();
        }
    }
}