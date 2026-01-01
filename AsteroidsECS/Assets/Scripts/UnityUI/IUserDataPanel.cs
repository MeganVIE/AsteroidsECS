using Data;

namespace UnityUI
{
    public interface IUserDataPanel
    {
        void SetHealth(int value);
        void SetPosition(Point value);
        void SetRotation(float value);
        void SetSpeed(float value);
        void SetLaserAmount(int value);
        void SetLaserRechargeTime(float value);

    }
}