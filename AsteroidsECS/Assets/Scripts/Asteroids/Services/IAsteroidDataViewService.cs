using UFO.Services;
using UI.Services;

namespace Asteroids.Services
{
    public interface IAsteroidDataViewService : IDestroyItemService, IDataViewService, IViewClearService { }
}