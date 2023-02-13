using Project.Scripts.Area.Components;
using Project.Scripts.Core.ECS.Entity;
using Project.Scripts.Core.ECS.System;

namespace Project.Scripts.Area.Systems
{
    public class StartInitializerSystem : ISystem
    {
        private readonly IEntityManager _entityManager;
        private bool _isFirstInitialized;

        public StartInitializerSystem(IEntityManager entityManager)
        {
            _entityManager = entityManager;
            _isFirstInitialized = true;
        }

        public void Execute()
        {
            if (_isFirstInitialized)
            {
                var entity = _entityManager.CreateEntity();
                entity.Components.Add(new NotifierPrefabComponent());
                _isFirstInitialized = false;
            }
        }
    }
}