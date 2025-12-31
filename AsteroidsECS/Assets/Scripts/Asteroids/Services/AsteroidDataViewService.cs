using System.Collections.Generic;
using Configs;
using Data;
using UnityEngine;

namespace Asteroids.Services
{
    public class AsteroidDataViewService : IAsteroidDataViewService
    {
        private Dictionary<int, GameObject> _viewsByIds = new();

        public void CreateView(int id, AsteroidConfig config)
        {
            _viewsByIds[id] = Object.Instantiate(config.ViewPrefab);
        }

        public void SetPosition(int id, Point newPosition)
        {
            _viewsByIds[id].transform.position = new Vector3(newPosition.X, newPosition.Y);
        }

        public void Destroy(int id)
        {
            Object.Destroy(_viewsByIds[id]);
            _viewsByIds.Remove(id);
        }

        public void Clear()
        {
            foreach ((int _, GameObject view) in _viewsByIds)
            {
                Object.Destroy(view);
            }
            
            _viewsByIds.Clear();
        }
    }
}