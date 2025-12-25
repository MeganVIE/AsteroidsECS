using Configs;

namespace UI.Services
{
    public interface IUFODataViewService : IDestroyItemService, IViewPositionService
    {
        void CreateView(int id, UFOConfig config);
    }
}