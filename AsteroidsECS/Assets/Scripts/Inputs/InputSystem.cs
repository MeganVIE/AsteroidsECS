using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
    public class InputSystem : MonoBehaviour
    {
        #region Activation
        [SerializeField] private InputActionAsset m_actionAsset;    

        private void Awake()
        {
            if (m_actionAsset != null)
                m_actionAsset.Enable();

            Init();
        }
        #endregion

        [Space]
        [SerializeField] private InputActionReference m_moveForward;
        [SerializeField] private InputActionReference m_rotate;
        [SerializeField] private InputActionReference m_gunUse;
        [SerializeField] private InputActionReference m_laserUse;

        public float RotationValue => m_rotate.action.ReadValue<float>();
        public InputActionPhase MoveForwardPhase => m_moveForward.action.phase;

        public Action onGunUse { get; set; }
        public Action onLaserUse { get; set; }

        private void Init()
        {
            m_gunUse.action.performed += GunUsePerformed;
            m_laserUse.action.performed += LaserUsePerformed;
        }

        private void OnDestroy()
        {
            m_gunUse.action.performed -= GunUsePerformed;
            m_laserUse.action.performed -= LaserUsePerformed;
        }

        private void LaserUsePerformed(InputAction.CallbackContext obj)
        {
            onLaserUse?.Invoke();
        }

        private void GunUsePerformed(InputAction.CallbackContext obj)
        {
            onGunUse?.Invoke();
        }
    }
}
