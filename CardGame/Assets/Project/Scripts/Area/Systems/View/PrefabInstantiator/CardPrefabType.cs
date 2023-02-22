using System;
using Project.Scripts.Area.Components.View.GameObjectComponent;
using UnityEngine;

namespace Project.Scripts.Area.Systems.View.PrefabInstantiator
{
    [Serializable]
    public struct CardPrefabType
    {
        public CardPrefabTypesId Id;
        public GameObject Prefab;
    }
}