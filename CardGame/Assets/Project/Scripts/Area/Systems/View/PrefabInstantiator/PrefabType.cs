using System;
using UnityEngine;

namespace Project.Scripts.Area.Systems.View.PrefabInstantiator
{
    [Serializable]
    public struct PrefabType
    {
        public GameObject Prefab;
        public PrefabTypeId Id;
    }
}