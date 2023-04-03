using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Configs/Enemy")]
    public class EnemyConfig : ScriptableObject
    {
        public string Name;
        public Sprite Artwork;
        [Space(10)]
        public int Health;
        public int KillPoint;
    }
}
