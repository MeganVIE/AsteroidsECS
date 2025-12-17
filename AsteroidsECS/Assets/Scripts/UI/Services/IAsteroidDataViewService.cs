using Configs;
using Data;

namespace UI.Services
{
    public interface IAsteroidDataViewService
    {
        void CreateView(int id, AsteroidConfig config);
        void SetPosition(int id, Point newPosition);
        void Destroy(int id);
    }
}