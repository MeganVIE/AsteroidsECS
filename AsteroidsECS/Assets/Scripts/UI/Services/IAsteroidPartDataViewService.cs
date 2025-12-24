using Configs;

namespace UI.Services
{
    public interface IAsteroidPartDataViewService : IDestroyItemService, IViewPositionService
    {
        void CreateView(int id, AsteroidPartConfig config);
    }
}