using Configs;
using UI.Services;

namespace UFO.Services
{
    public interface IUFODataViewService : IDestroyItemService, IViewPositionService, IViewClearService
    {
        void CreateView(int id, UFOConfig config);
    }
}