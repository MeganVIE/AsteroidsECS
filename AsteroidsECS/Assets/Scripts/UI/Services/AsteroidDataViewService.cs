using System.Collections.Generic;
using Configs;
using UnityEngine;
using Utils;

namespace UI.Services
{
    public interface IAsteroidDataViewService : IEntityDataViewService {}
    
    public class AsteroidDataViewService : IAsteroidDataViewService
    {
        private Dictionary<int, GameObject> _asteroids = new();

        public void CreateView(int id, EntityConfig config)
        {
            _asteroids[id] = Object.Instantiate(config.ViewPrefab);
        }

        public void SetPosition(int id, Point newPosition)
        {
            _asteroids[id].transform.position = new Vector3(newPosition.X, newPosition.Y);
        }
    }
}