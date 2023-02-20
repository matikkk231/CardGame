using System;
using System.Collections.Generic;

namespace Project.Scripts.Core.ECS.Entity
{
    public class EntityManager : IEntityManager
    {
        public List<IEntity> Entities { get; }

        public EntityManager()
        {
            Entities = new List<IEntity>();
        }

        public List<IEntity> GetEntitiesOfGroup(List<Type> needfulGroup)
        {
            var needfulEntities = new List<IEntity>();

            var amountOfComponentMatches = 0;
            var amountOfComponentMatchesShouldBe = needfulGroup.Count;
            foreach (var entity in Entities)
            {
                foreach (var component in entity.Components)
                {
                    foreach (var needfulComponent in needfulGroup)
                    {
                        if (needfulComponent == component.GetType())
                        {
                            amountOfComponentMatches++;
                        }
                    }
                }

                if (amountOfComponentMatches == amountOfComponentMatchesShouldBe)
                {
                    needfulEntities.Add(entity);
                }

                amountOfComponentMatches = 0;
            }

            return needfulEntities;
        }

        public IEntity CreateEntity()
        {
            var entity = new Entity();
            Entities.Add(entity);
            return entity;
        }

        public void RemoveEntity(IEntity entity)
        {
            Entities.Remove(entity);
        }
    }
}