using Data;
using TMPro;
using UnityEngine;

namespace UnityUI
{
    public class UserDataPanel : MonoBehaviour, IUserDataPanel
    {
        [SerializeField] private TextMeshProUGUI health;
        [SerializeField] private TextMeshProUGUI position;
        [SerializeField] private TextMeshProUGUI rotation;
        [SerializeField] private TextMeshProUGUI speed;
        [SerializeField] private TextMeshProUGUI laserAmount;
        [SerializeField] private TextMeshProUGUI laserRecharge;
        
        public void SetHealth(int value)
        {
            health.text = value.ToString();
        }

        public void SetPosition(Point value)
        {
            position.text = $"({value.X:0.00}; {value.Y:0.00})";
        }

        public void SetRotation(float value)
        {
            rotation.text = value.ToString("0.00");
        }

        public void SetSpeed(float value)
        {
            speed.text = value.ToString("0.00");
        }

        public void SetLaserAmount(int value)
        {
            laserAmount.text = value.ToString();
        }

        public void SetLaserRechargeTime(float value)
        {
            laserRecharge.text = value.ToString("0.00");
        }
    }
}