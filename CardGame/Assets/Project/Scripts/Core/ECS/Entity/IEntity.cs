using System.Collections.Generic;
using Project.Scripts.Core.ECS.Component;

namespace Project.Scripts.Core.ECS.Entity
{
    public interface IEntity
    {
        public List<IComponent> Components { get; }
    }
}