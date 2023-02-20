using UnityEngine;

namespace Project.Scripts.Area.Configs
{
    [CreateAssetMenu]
    public class MonsterConfig : ScriptableObject
    {
        public Sprite MonsterImage;

        public int MinHp;
        public int MaxHp;
    }
}