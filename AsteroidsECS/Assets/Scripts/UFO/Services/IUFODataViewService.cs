using Configs;
using UI.Services;

namespace UFO.Services
{
    public interface IUFODataViewService : IDestroyItemService, IViewPositionService
    {
        void CreateView(int id, UFOConfig config);
    }
}