using System;
using Project.Scripts.Area.Systems;
using Project.Scripts.Core.ECS.Entity;
using Project.Scripts.Core.ECS.System;
using UnityEngine;

namespace Project.Scripts
{
    public class Entry : MonoBehaviour
    {
        [SerializeField] private GameObject _notifierPrefab;
        [SerializeField] private GameObject _notifierParentPrefab;

        private IEntityManager _entityManager;
        private ISystem _notifierPrefabCreatorSystem;
        private ISystem _startInitializerSystem;
        private ISystem _notifierSystem;

        private void Awake()
        {
            _entityManager = new EntityManager();
            _notifierSystem = new NotifierSystem(_entityManager);

            _notifierPrefabCreatorSystem =
                new NotifierPrefabCreatorSystem(_entityManager, _notifierPrefab, _notifierParentPrefab);
            _startInitializerSystem = new StartInitializerSystem(_entityManager);
        }

        private void Update()
        {
            _startInitializerSystem.Execute();
            _notifierPrefabCreatorSystem.Execute();
            _notifierSystem.Execute();
        }
    }
}