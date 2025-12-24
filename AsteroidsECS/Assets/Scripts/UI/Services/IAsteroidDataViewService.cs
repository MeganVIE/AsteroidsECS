using Configs;

namespace UI.Services
{
    public interface IAsteroidDataViewService : IDestroyItemService, IViewPositionService
    {
        void CreateView(int id, AsteroidConfig config);
    }
}