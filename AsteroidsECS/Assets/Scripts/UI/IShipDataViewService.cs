using Configs;

namespace UI
{
    public interface IShipDataViewService
    {
        void CreateShipView(ShipConfig config);
        void SetShipPosition(Point newPosition);
        void SetShipRotation(float angle);
    }
}