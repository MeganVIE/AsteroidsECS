using UnityEngine;

namespace Utils
{
    public class DeltaTimeService : IDeltaTimeService
    {
        public float DeltaTime => Time.deltaTime;
    }
}