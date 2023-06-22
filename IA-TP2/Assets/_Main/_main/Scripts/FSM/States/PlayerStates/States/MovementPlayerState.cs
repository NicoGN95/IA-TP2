using _Main._main.Scripts.Entities.Player;
using _Main._main.Scripts.Extensions;
using _Main._main.Scripts.Managers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Main._main.Scripts.FSM.States.PlayerStates.States
{
    [CreateAssetMenu(menuName = "_main/Player/States/MovementPlayerState")]
    public class MovementPlayerState : PlayerState
    {
        private Vector3 m_dir;
        private float m_speed;
        
        public override void EnterState(PlayerModel p_model)
        {
            base.EnterState(p_model);

            
            m_dir = Vector3.zero;


            var l_inputManager = InputManager.Instance;
            l_inputManager.SubscribeInput(Model.GetPlayerInputData().MovementId, MovementOnPerformed);
            l_inputManager.SubscribeInput(Model.GetPlayerInputData().ShootId, ShootOnPerformed);
            l_inputManager.SubscribeInput(Model.GetPlayerInputData().ReloadId, ReloadOnPerformed);

        }

        private void ReloadOnPerformed(InputAction.CallbackContext p_obj)
        {
            Model.ReloadGun();
        }

        private void ShootOnPerformed(InputAction.CallbackContext p_obj)
        {
            Model.Shoot();
        }

        private void MovementOnPerformed(InputAction.CallbackContext p_obj)
        {
            if (Model.CheckGround())
                m_dir = p_obj.ReadValue<Vector2>().X0Z();
        }
       
        
        public override void ExecuteState()
        {
            var l_transform = Model.transform;
            l_transform.rotation = Quaternion.Euler(Vector3.up * Model.GetCameraRotationY());
            
            var l_position = l_transform.position;
            var l_dir = (l_transform.right * m_dir.x + l_transform.forward * m_dir.z).normalized;
            l_position += l_dir * (Model.GetData().MovementSpeed * Time.deltaTime);

            l_transform.position = l_position;
            
            //TODO, hacer todo por RB m_speed = 1;
            Model.GetView().SetAnimSpeed(m_speed);
            
        }

        public override void ExitState()
        {
            var l_inputManager = InputManager.Instance;
            
            l_inputManager.UnsubscribeInput(Model.GetPlayerInputData().MovementId, MovementOnPerformed);
        }
    }
}