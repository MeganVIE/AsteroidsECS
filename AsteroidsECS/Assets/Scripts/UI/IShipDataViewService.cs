using Configs;

namespace UI
{
    public interface IShipDataViewService : IEntityDataViewService
    {
        void SetShipRotation(float angle);
    }
}