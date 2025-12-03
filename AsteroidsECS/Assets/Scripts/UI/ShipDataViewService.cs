using UnityEngine;
using Utils;

namespace UI
{
    public class ShipDataViewService : IShipDataViewService
    {
        private GameObject _ship;

        public void CreateView(int _, EntityConfig config)
        {
            _ship = Object.Instantiate(config.ViewPrefab);
        }

        public void SetPosition(int _, Point newPosition)
        {
            _ship.transform.position = new Vector3(newPosition.X, newPosition.Y);
        }

        public void SetShipRotation(float angle)
        {
            _ship.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}