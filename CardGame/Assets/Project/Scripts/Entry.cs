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
        [SerializeField] private List<CardPrefabType> _cardPrefabTypes;
        [SerializeField] private List<PrefabType> _prefabTypes;
        [SerializeField] private List<MonsterConfig> _monsterConfigs;
        [SerializeField] private Sprite _playerImage;
        [SerializeField] private List<PotionConfig> _potionConfigs;

        private IEntityManager _entityManager;

        private ISystem _startInitializerSystem;
        private ISystem _cardGeneratorSystem;
        private ISystem _cardPrefabInstantiatorSystem;
        private ISystem _prefabInstantiatorSystem;
        private ISystem _healthViewSystem;
        private ISystem _checkingAbilityToInteractSystem;
        private ISystem _checkingInteractingTrySystem;
        private ISystem _fieldManagerSystem;
        private ISystem _interactingWithEmptyCardsSystem;
        private ISystem _destroyingCardsSystem;
        private ISystem _movementSystem;
        private ISystem _interactingWithMonsterCardSystem;
        private ISystem _fallingSystem;
        private ISystem _interactingWithHealingPotionCardSystem;
        private ISystem _turnDoneNotifierSystem;
        private ISystem _turnFinishedNotifierSystem;
        private ISystem _healingStatusProcessingSystem;
        private ISystem _showingImpactSystem;
        private ISystem _updatingScoreSystem;
        private ISystem _movementViewSystem;
        private ISystem _loosingSystem;
        private ISystem _interactingWithPoisonCard;
        private ISystem _toxicStatusProcessingSystem;

        private void Start()
        {
            _entityManager = new EntityManager();

            _startInitializerSystem = new StartInitializerSystem(_entityManager, _playerImage);
            _cardGeneratorSystem = new CardGeneratorSystem(_entityManager, _monsterConfigs, _potionConfigs);
            _cardPrefabInstantiatorSystem = new CardPrefabInstantiatorSystem(_entityManager, _cardPrefabTypes);
            _healthViewSystem = new HealthViewSystem(_entityManager);
            _checkingAbilityToInteractSystem = new CheckingAbilityToInteractSystem(_entityManager);
            _checkingInteractingTrySystem = new CheckingInteractingTrySystem(_entityManager);
            _fieldManagerSystem = new FieldManagerSystem(_entityManager);
            _interactingWithEmptyCardsSystem = new InteractingWithEmptyCardsSystem(_entityManager);
            _destroyingCardsSystem = new DestroyCardSystem(_entityManager);
            _movementSystem = new MovingCardsSystem(_entityManager);
            _interactingWithMonsterCardSystem = new InteractingWithMonsterSystem(_entityManager);
            _fallingSystem = new FallingCardsSystem(_entityManager);
            _interactingWithHealingPotionCardSystem = new InteractingWithHealingPotionCardSystem(_entityManager);
            _turnDoneNotifierSystem = new TurnDoneNotifierSystem(_entityManager);
            _turnFinishedNotifierSystem = new TurnFinishedNotifierSystem(_entityManager);
            _healingStatusProcessingSystem = new HealingStatusProcessingSystem(_entityManager);
            _showingImpactSystem = new ShowingPotionImpactSystem(_entityManager);
            _prefabInstantiatorSystem = new PrefabInstantiatorSystem(_entityManager, _prefabTypes);
            _updatingScoreSystem = new UpdatingScoreSystem(_entityManager);
            _movementViewSystem = new MovementViewSystem(_entityManager);
            _loosingSystem = new LoosingSystem(_entityManager);
            _interactingWithPoisonCard = new InteractingWithPoisonSystem(_entityManager);
            _toxicStatusProcessingSystem = new ToxicStatusProcessingSystem(_entityManager);
        }

        private void Update()
        {
            _startInitializerSystem.Execute();
            _cardGeneratorSystem.Execute();
            _cardPrefabInstantiatorSystem.Execute();
            _prefabInstantiatorSystem.Execute();
            _fieldManagerSystem.Execute();
            _checkingAbilityToInteractSystem.Execute();

            _checkingInteractingTrySystem.Execute();

            _interactingWithEmptyCardsSystem.Execute();
            _interactingWithMonsterCardSystem.Execute();
            _interactingWithHealingPotionCardSystem.Execute();
            _interactingWithPoisonCard.Execute();

            _turnDoneNotifierSystem.Execute();

            _destroyingCardsSystem.Execute();
            _movementSystem.Execute();

            _fieldManagerSystem.Execute();
            _fallingSystem.Execute();
            _movementSystem.Execute();

            _healingStatusProcessingSystem.Execute();
            _toxicStatusProcessingSystem.Execute();
            _turnFinishedNotifierSystem.Execute();

            _showingImpactSystem.Execute();
            _updatingScoreSystem.Execute();
            _fieldManagerSystem.Execute();

            _movementViewSystem.Execute();
            _healthViewSystem.Execute();
            _loosingSystem.Execute();
        }
    }
}