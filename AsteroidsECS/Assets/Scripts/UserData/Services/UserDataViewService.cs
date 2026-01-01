using Data;
using UnityUI;

namespace UserData.Services
{
    public class UserDataViewService : IUserDataViewService
    {
        private IUserDataPanel _panel;
        
        public void SetPanel(IUserDataPanel panel)
        {
            _panel = panel;
        }

        public void SetHealth(int value)
        {
            _panel.SetHealth(value);
        }

        public void SetPosition(Point value)
        {
            _panel.SetPosition(value);
        }

        public void SetRotation(float value)
        {
            _panel.SetRotation(value);
        }

        public void SetSpeed(float value)
        {
            _panel.SetSpeed(value);
        }

        public void SetLaserAmount(int value)
        {
            _panel.SetLaserAmount(value);
        }

        public void SetLaserRechargeTime(float value)
        {
            _panel.SetLaserRechargeTime(value);
        }
    }
}