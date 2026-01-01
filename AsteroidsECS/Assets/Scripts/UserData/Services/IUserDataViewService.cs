using Data;
using UnityUI;

namespace UserData.Services
{
    public interface IUserDataViewService
    {
        void SetPanel(IUserDataPanel panel);
        void SetHealth(int value);
        void SetPosition(Point value);
        void SetRotation(float value);
        void SetSpeed(float value);
        void SetLaserAmount(int value);
        void SetLaserRechargeTime(float value);
    }
}