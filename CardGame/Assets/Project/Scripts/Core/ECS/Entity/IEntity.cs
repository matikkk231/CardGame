using System;
using System.Collections.Generic;
using Project.Scripts.Core.ECS.Component;

namespace Project.Scripts.Core.ECS.Entity
{
    public interface IEntity
    {
        public IEnumerable<IComponent> Components { get; }
        public IComponent GetComponent(Type type);
        public void AddComponent(IComponent additionalComponent);
        public void RemoveComponent(Type componentType);
    }
}