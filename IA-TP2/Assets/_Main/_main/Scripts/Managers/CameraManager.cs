using System;
using _Main._main.Scripts.Datas;
using _Main._main.Scripts.Entities.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Main._main.Scripts.Managers
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private Transform pivot;

        private Camera m_camera;
        private PlayerModel m_playerModel;
        private PlayerData m_data;

        private Vector2 m_inputValue;
        private float m_XRotation;
        
        
        private void Start()
        {
            m_camera = Camera.main;
            m_playerModel = GameManager.Instance.GetLocalPlayer();
            m_data = m_playerModel.GetData();
            
            var l_input = InputManager.Instance;
            l_input.SubscribeInput(m_data.InputData.CameraMovementId, CameraMovementOnPerformed);


            if (m_camera != null)
            {
                var l_transform = m_camera.transform;
                l_transform.SetParent(pivot);

                l_transform.position = pivot.transform.position;
            }

            Cursor.lockState = CursorLockMode.Locked;
        }

        private void CameraMovementOnPerformed(InputAction.CallbackContext p_obj)
        {
            m_inputValue = p_obj.ReadValue<Vector2>();
            Debug.Log(m_inputValue);
        }


        private void LateUpdate()
        {
            RotateCamera();
        }
        
        
        private void RotateCamera()
        {
            var l_mouseX = m_inputValue.x * m_data.MouseSens * Time.deltaTime;
            var l_mouseY = m_inputValue.y * m_data.MouseSens * Time.deltaTime;

            m_XRotation -= l_mouseY;
            m_XRotation = Mathf.Clamp(m_XRotation, -90f, 90f);

            m_camera.transform.localRotation = Quaternion.Euler(m_XRotation,0,0);
            m_playerModel.transform.Rotate(Vector3.up * l_mouseX);
        }
    }
}