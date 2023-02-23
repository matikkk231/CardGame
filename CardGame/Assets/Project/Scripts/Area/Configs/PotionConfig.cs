using UnityEngine;

namespace Project.Scripts.Area.Configs
{
    [CreateAssetMenu]
    public class PotionConfig : ScriptableObject
    {
        public string Id;
        
        public int ImpactForce;
        public int ImpactDuration;

        public Sprite PotionImage;
    }
}