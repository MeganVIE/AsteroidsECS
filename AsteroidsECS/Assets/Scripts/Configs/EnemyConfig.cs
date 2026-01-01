using UnityEngine;

namespace Configs
{
    public abstract class EnemyConfig : ScriptableObject
    {
        public GameObject ViewPrefab;
        
        public float CollisionRadius = 0.32f;
        public float StartMoveSpeed = 0.002f;
        public int ScorePoints = 10;
    }
}