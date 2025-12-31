using Configs;
using UI.Services;

namespace Asteroids.Services
{
    public interface IAsteroidDataViewService : IDestroyItemService, IViewPositionService
    {
        void CreateView(int id, AsteroidConfig config);
    }
}