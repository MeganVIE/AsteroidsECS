using Configs;
using Data;
using UnityEngine;

namespace UI.Services
{
    public class ShipDataViewService : IShipDataViewService
    {
        private GameObject _ship;

        public void CreateView(ShipConfig config)
        {
            _ship = Object.Instantiate(config.ViewPrefab);
        }

        public void SetPosition(Point newPosition)
        {
            _ship.transform.position = new Vector3(newPosition.X, newPosition.Y);
        }

        public void SetShipRotation(float angle)
        {
            _ship.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}