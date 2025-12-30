using System.Collections.Generic;
using Configs;
using Data;
using UnityEngine;

namespace UFO.Services
{
    public class UFODataViewService : IUFODataViewService
    {
        private Dictionary<int, GameObject> _ufos = new();

        public void CreateView(int id, UFOConfig config)
        {
            _ufos[id] = Object.Instantiate(config.ViewPrefab);
        }

        public void SetPosition(int id, Point newPosition)
        {
            _ufos[id].transform.position = new Vector3(newPosition.X, newPosition.Y);
        }

        public void Destroy(int id)
        {
            Object.Destroy(_ufos[id]);
            _ufos.Remove(id);
        }
    }
}