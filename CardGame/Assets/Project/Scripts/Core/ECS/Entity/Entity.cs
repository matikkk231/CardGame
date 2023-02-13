using System.Collections.Generic;
using Project.Scripts.Core.ECS.Component;

namespace Project.Scripts.Core.ECS.Entity
{
    public class Entity : IEntity
    {
        public List<IComponent> Components { get; }

        public Entity()
        {
            Components = new List<IComponent>();
        }
    }
}