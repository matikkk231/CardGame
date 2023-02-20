using Project.Scripts.Core.ECS.Component;
using UnityEngine;

namespace Project.Scripts.Area.Components.View.GameObjectComponent
{
    public class GameObjectComponent : IComponent
    {
        public GameObject GameObject;

        public GameObjectComponent(GameObject gameObject)
        {
            GameObject = gameObject;
        }
    }
}