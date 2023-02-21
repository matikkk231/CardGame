using System;
using System.Collections.Generic;
using Project.Scripts.Area.Components.Logic;
using Project.Scripts.Area.Components.View.GameObjectComponent;
using Project.Scripts.Area.Configs;
using Project.Scripts.Core.ECS.Entity;
using Project.Scripts.Core.ECS.System;
using Unity.Mathematics;
using Random = Unity.Mathematics.Random;

namespace Project.Scripts.Area.Systems.Logic
{
    public class CardGeneratorSystem : ISystem
    {
        private readonly IEntityManager _entityManager;
        private readonly List<Type> _fieldComponentType;

        private readonly List<MonsterConfig> _monsterConfigs;
        private readonly List<PotionConfig> _potionConfigs;

        public CardGeneratorSystem(IEntityManager entityManager, List<MonsterConfig> monsterConfigs, List<PotionConfig> potionConfigs)
        {
            _entityManager = entityManager;

            _fieldComponentType = new List<Type>();
            _fieldComponentType.Add(typeof(FieldComponent));
            _monsterConfigs = monsterConfigs;
            _potionConfigs = potionConfigs;
        }

        public void Execute()
        {
            var fields = _entityManager.GetEntitiesOfGroup(_fieldComponentType);
            IEntity entityWithFieldComponent = null;
            foreach (var element in fields)
            {
                entityWithFieldComponent = element;
            }

            FieldComponent fieldComponent =
                (FieldComponent)entityWithFieldComponent.GetComponent(typeof(FieldComponent));
            for (int y = 0; y <= fieldComponent.MaxPositionY; y++)
            {
                for (int x = 0; x <= fieldComponent.MaxPositionX; x++)
                {
                    if (fieldComponent.PositionsWithCard[x, y] == null)
                    {
                        var randomNumber = UnityEngine.Random.Range(1, 4);
                        switch (randomNumber)
                        {
                            case 1:
                                CreateEmptyCard(fieldComponent, x, y);
                                break;
                            case 2:
                                CreateMonsterCard(fieldComponent, x, y);
                                break;
                            case 3:
                                CreateFastHealingPotionCard(fieldComponent, x, y);
                                break;
                            default: throw new Exception("card with this number doesn't exist");
                        }
                    }
                }
            }
        }

        private void CreateEmptyCard(FieldComponent fieldComponent, int x, int y)
        {
            var card = _entityManager.CreateEntity();
            card.AddComponent(new CardComponent());
            card.AddComponent(new EmptyCardComponent());
            int2 currentPositionRelativeFieldCenter = new int2(
                x - Math.Abs(fieldComponent.MinRelativeCenterPositionX),
                y - Math.Abs(fieldComponent.MinRelativeCenterPositionY));
            card.AddComponent(new PositionRelativeFieldCenterComponent(currentPositionRelativeFieldCenter));
            int2 positionWhereCardShouldBeInstantiated = new int2(currentPositionRelativeFieldCenter.x * 35,
                currentPositionRelativeFieldCenter.y * 52);
            card.AddComponent(new NeedInstantiatingCardPrefab(PrefabTypesId.Card,
                positionWhereCardShouldBeInstantiated, null));
            card.AddComponent(new FallingComponent());
        }

        private void CreateMonsterCard(FieldComponent fieldComponent, int x, int y)
        {
            var monsterNumber = UnityEngine.Random.Range(0, _monsterConfigs.Count);

            var monsterHp = UnityEngine.Random.Range(_monsterConfigs[monsterNumber].MinHp,
                _monsterConfigs[monsterNumber].MaxHp);

            var monsterSprite = _monsterConfigs[monsterNumber].MonsterImage;


            var card = _entityManager.CreateEntity();
            card.AddComponent(new CardComponent());
            card.AddComponent(new MonsterCardComponent());
            int2 currentPositionRelativeFieldCenter = new int2(
                x - Math.Abs(fieldComponent.MinRelativeCenterPositionX),
                y - Math.Abs(fieldComponent.MinRelativeCenterPositionY));
            card.AddComponent(new PositionRelativeFieldCenterComponent(currentPositionRelativeFieldCenter));
            int2 positionWhereCardShouldBeInstantiated = new int2(currentPositionRelativeFieldCenter.x * 35,
                currentPositionRelativeFieldCenter.y * 52);
            card.AddComponent(new NeedInstantiatingCardPrefab(PrefabTypesId.MonsterCard,
                positionWhereCardShouldBeInstantiated, monsterSprite));
            card.AddComponent(new HealthComponent(monsterHp));
            card.AddComponent(new FallingComponent());
        }

        private void CreateFastHealingPotionCard(FieldComponent fieldComponent, int x, int y)
        {
            var card = _entityManager.CreateEntity();
            card.AddComponent(new CardComponent());

            PotionConfig fastHealingPotionConfig = null;
            foreach (var potionConfig in _potionConfigs)
            {
                if (potionConfig.Id == "fastHealingPotion")
                {
                    fastHealingPotionConfig = potionConfig;
                    break;
                }
            }

            card.AddComponent(new PotionCardComponent(fastHealingPotionConfig.ImpactDuration, fastHealingPotionConfig.ImpactForce, fastHealingPotionConfig.Id));

            int2 currentPositionRelativeFieldCenter = new int2(
                x - Math.Abs(fieldComponent.MinRelativeCenterPositionX),
                y - Math.Abs(fieldComponent.MinRelativeCenterPositionY));

            card.AddComponent(new PositionRelativeFieldCenterComponent(currentPositionRelativeFieldCenter));

            int2 positionWhereCardShouldBeInstantiated = new int2(currentPositionRelativeFieldCenter.x * 35,
                currentPositionRelativeFieldCenter.y * 52);
            card.AddComponent(new NeedInstantiatingCardPrefab(PrefabTypesId.Potion, positionWhereCardShouldBeInstantiated, fastHealingPotionConfig.PotionImage));
            card.AddComponent(new FallingComponent());
        }
    }
}