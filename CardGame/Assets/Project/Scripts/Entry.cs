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
        private List<ISystem> _systems;


        private void Start()
        {
            _entityManager = new EntityManager();
            _systems = new List<ISystem>();

            _systems.Add(new StartInitializerSystem(_entityManager, _playerImage));
            _systems.Add(new CardGeneratorSystem(_entityManager, _monsterConfigs, _potionConfigs));
            _systems.Add(new CardPrefabInstantiatorSystem(_entityManager, _cardPrefabTypes));
            _systems.Add(new PrefabInstantiatorSystem(_entityManager, _prefabTypes));
            _systems.Add(new FieldManagerSystem(_entityManager));
            _systems.Add(new CheckingAbilityToInteractSystem(_entityManager));

            _systems.Add(new CheckingInteractingTrySystem(_entityManager));

            _systems.Add(new InteractingWithEmptyCardsSystem(_entityManager));
            _systems.Add(new InteractingWithMonsterSystem(_entityManager));
            _systems.Add(new InteractingWithHealingPotionCardSystem(_entityManager));
            _systems.Add(new InteractingWithPoisonSystem(_entityManager));

            _systems.Add(new TurnDoneNotifierSystem(_entityManager));
            _systems.Add(new DestroyCardSystem(_entityManager));
            _systems.Add(new MovingCardsSystem(_entityManager));

            _systems.Add(new FieldManagerSystem(_entityManager));
            _systems.Add(new FallingCardsSystem(_entityManager));
            _systems.Add(new MovingCardsSystem(_entityManager));

            _systems.Add(new HealingStatusProcessingSystem(_entityManager));
            _systems.Add(new ToxicStatusProcessingSystem(_entityManager));
            _systems.Add(new TurnFinishedNotifierSystem(_entityManager));

            _systems.Add(new ShowingPotionImpactSystem(_entityManager));
            _systems.Add(new UpdatingScoreSystem(_entityManager));
            _systems.Add(new FieldManagerSystem(_entityManager));

            _systems.Add(new MovementViewSystem(_entityManager));
            _systems.Add(new HealthViewSystem(_entityManager));
            _systems.Add(new LoosingSystem(_entityManager));
        }

        private void Update()
        {
            foreach (var system in _systems)
            {
                system.Execute();
            }
        }
    }
}