using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "UFOConfig", menuName = "UFO Config", order = 11)]
    public class UFOConfig : ScriptableObject
    {
        public GameObject ViewPrefab;
        
        public float CollisionRadius = 0.32f;
        public float StartMoveSpeed = 0.002f;
        public float DelaySpawnTime = 5;

        public static UFOConfig LoadFromAssets() => Resources.Load<UFOConfig>("UFOConfig");
    }
}