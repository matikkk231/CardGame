using System;
using System.Collections.Generic;
using Project.Scripts.Core.ECS.Component;

namespace Project.Scripts.Core.ECS.Entity
{
    public class Entity : IEntity
    {
        private readonly List<IComponent> _components;

        public IEnumerable<IComponent> Components => _components;

        public Entity()
        {
            _components = new List<IComponent>();
        }


        public IComponent GetComponent(Type type)
        {
            IComponent requestedComponent = null;
            foreach (var component in _components)
            {
                if (component.GetType() == type)
                {
                    requestedComponent = component;
                    break;
                }
            }

            return requestedComponent;
        }

        public void AddComponent(IComponent additionalComponent)
        {
            bool entityHasComponent = false;
            foreach (var component in _components)
            {
                if (component.GetType() == additionalComponent.GetType())
                {
                    entityHasComponent = true;
                    break;
                }
            }

            if (!entityHasComponent)
            {
                _components.Add(additionalComponent);
            }
        }

        public void RemoveComponent(Type componentType)
        {
            foreach (var component in _components)
            {
                if (component.GetType() == componentType)
                {
                    _components.Remove(component);
                    break;
                }
            }
        }
    }
}