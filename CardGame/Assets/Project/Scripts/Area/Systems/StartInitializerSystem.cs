using Project.Scripts.Area.Components.Logic;
using Project.Scripts.Core.ECS.Entity;
using Project.Scripts.Core.ECS.System;

namespace Project.Scripts.Area.Systems.Logic
{
    public class StartInitializerSystem : ISystem
    {
        private IEntityManager _entityManager;
        private bool _initializedFirst;

        public StartInitializerSystem(IEntityManager entityManager)
        {
            _entityManager = entityManager;
            _initializedFirst = true;
        }

        public void Execute()
        {
            if (_initializedFirst)
            {
                var field = _entityManager.CreateEntity();
                field.AddComponent(new FieldComponent(1, 1, -1, -1));

                _initializedFirst = false;
            }
        }
    }
}