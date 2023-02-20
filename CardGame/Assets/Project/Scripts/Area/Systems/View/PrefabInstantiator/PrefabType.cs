using System;
using Project.Scripts.Area.Components.View.GameObjectComponent;
using UnityEngine;

namespace Project.Scripts.Area.Systems.View.PrefabInstantiator
{
    [Serializable]
    public struct PrefabType
    {
        public PrefabTypesId Id;
        public GameObject Prefab;
    }
}