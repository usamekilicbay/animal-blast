using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Projectile", menuName = "Configs/Projectile")]
    public class ProjectileConfig : ScriptableObject
    {
        public string Name;
        [Range(0f, 10000f)]
        public float ProjectileSpeed;
        [Range(1f, 1000f)]
        public float Damage;
    }
}
