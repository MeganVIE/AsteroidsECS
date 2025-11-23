using UnityEngine;
using UnityEngine.InputSystem;

namespace Inputs
{
    public class InputSystem : MonoBehaviour, IInputSystem
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

        public bool MovePressing => m_moveForward.action.phase == InputActionPhase.Performed;
        public float RotationValue => m_rotate.action.ReadValue<float>();
        public bool GunUsing => m_gunUse.action.phase == InputActionPhase.Performed;
        public bool LaserUsing => m_laserUse.action.phase == InputActionPhase.Performed;
    }
}
