using Configs;
using UI.Services;

namespace Asteroids.Services
{
    public interface IAsteroidDataViewService : IDestroyItemService, IViewPositionService, IViewClearService
    {
        void CreateView(int id, AsteroidConfig config);
    }
}