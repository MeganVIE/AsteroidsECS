using Data;

namespace UI.Services
{
    public interface IViewPositionService : IViewClearService
    {
        void SetPosition(int id, Point newPosition);
    }
}