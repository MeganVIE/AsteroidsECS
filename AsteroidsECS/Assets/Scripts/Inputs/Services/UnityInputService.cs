using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs.Services
{
    public class UnityInputService : MonoBehaviour, IInputService
    {
        #region Activation
        [SerializeField] private InputActionAsset m_actionAsset;    

        private void Awake()
        {
            if (m_actionAsset != null)
                m_actionAsset.Enable();
        }
        #endregion

        [Space]
        [SerializeField] private InputActionReference m_moveForward;
        [SerializeField] private InputActionReference m_rotate;
        [SerializeField] private InputActionReference m_gunUse;
        [SerializeField] private InputActionReference m_laserUse;

        private InputActionPhase _gunLastPhase;
        private InputActionPhase _laserLastPhase;

        public bool MovePressing => m_moveForward.action.phase == InputActionPhase.Performed;
        public float RotationValue => m_rotate.action.ReadValue<float>();
        public bool GunUsing { get; private set; }
        public bool LaserUsing { get; private set; }

        private void Update()
        {
            if (m_gunUse.action.phase == InputActionPhase.Performed)
            {
                GunUsing = _gunLastPhase != m_gunUse.action.phase;
            }
            else
            {
                GunUsing = false;
            }
            
            if (m_laserUse.action.phase == InputActionPhase.Performed)
            {
                LaserUsing = _laserLastPhase != m_laserUse.action.phase;
            }
            else
            {
                LaserUsing = false;
            }
            
            _gunLastPhase = m_gunUse.action.phase;
            _laserLastPhase = m_laserUse.action.phase;
        }
    }
}
