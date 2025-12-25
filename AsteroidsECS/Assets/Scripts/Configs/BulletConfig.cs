using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "BulletConfig", menuName = "Bullet Config", order = 12)]
    public class BulletConfig : ScriptableObject
    {
        public GameObject ViewPrefab;
        public float CollisionRadius = .05f;
        public float StartMoveSpeed = .4f;
        public float DelaySpawnTime = 1;

        public static BulletConfig LoadFromAssets() => Resources.Load<BulletConfig>("BulletConfig");
    }
}
