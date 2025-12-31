using Configs;
using UI.Services;

namespace Asteroids.Services
{
    public interface IAsteroidPartDataViewService : IDestroyItemService, IViewPositionService
    {
        void CreateView(int id, AsteroidPartConfig config);
    }
}