using Configs;
using UnityEngine;

namespace UI
{
    public class ShipDataViewService : IShipDataViewService
    {
        private GameObject _ship;

        public void CreateShipView(ShipConfig config)
        {
            _ship = Object.Instantiate(config.ViewPrefab, config.StartPosition, Quaternion.Euler(0, 0, config.StartRotation));
        }

        public void SetShipPosition(Point newPosition)
        {
            _ship.transform.position = new Vector3(newPosition.X, newPosition.Y);
        }

        public void SetShipRotation(float angle)
        {
            _ship.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}